using ClosedXML.Excel;
using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using MiRAPI.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MiRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost()]
        [Route("create")]
        public JsonResult Create([FromBody] Order order)
        {
            var user = (User)HttpContext.Items[MiRConsts.USER_BAG];

            using (var db = new MiRDB())
            {
                var nexAcct = (db.Operations.Where(o => o.OperType == 13).FirstOrDefault()?.ID)??0;

                foreach (var item in order.Items)
                {
                    db.Operations
                        .Value(o => o.OperType, 13)
                        .Value(o => o.GoodID, item.GoodId)
                        .Value(o => o.PartnerID, 1)
                        .Value(o => o.OperatorID, user.ID)
                        .Value(o => o.Sign, 0)
                        .Value(o => o.PriceIn, 0)
                        .Value(o => o.PriceOut, 0)
                        .Value(o => o.VATIn, 0)
                        .Value(o => o.VATOut, 0)
                        .Value(o => o.Discount, 0)
                        .Value(o => o.CurrencyID, 0)
                        .Value(o => o.CurrencyRate, 0)
                        .Value(o => o.Lot, String.Empty)
                        .Value(o => o.Note, String.Empty)
                        .Value(o => o.UserID, user.ID)
                        .Value(o => o.Qtty, item.Qtty)
                        .Value(o => o.Date, DateTime.Now)
                        .Value(o => o.UserRealTime, DateTime.Now)
                        .Value(o => o.ObjectID, order.ObjectId)
                        .Value(o => o.Acct, nexAcct++)
                        .InsertWithInt32Identity();
                }

                var name = "order_" + RandomName.RandomString(5) + ".xlsx";

                using (var workbook = new XLWorkbook(Path.Combine(PathExtentions.GetApplicationRoot, "templates", "kolos.xlsx")))
                {
                    var worksheet = workbook.Worksheets.First();

                    var rowIdx = 7;

                    foreach (var item in order.Items)
                    {
                        var good = db.Goods.First(g => g.ID == item.GoodId);

                        worksheet.Cell(rowIdx, 2).Value = rowIdx - 6;
                        worksheet.Cell(rowIdx, 3).Value = good.Code;
                        worksheet.Cell(rowIdx, 4).Value = good.Name;
                        worksheet.Cell(rowIdx, 6).Value = good.BarCode1;
                        //worksheet.Cell(rowIndex, columnIndex).Style.NumberFormat.Format = "$0.00";
                        worksheet.Cell(rowIdx, 6).DataType = XLDataType.Text;
                        worksheet.Cell(rowIdx, 8).Value = "штука";
                        worksheet.Cell(rowIdx, 10).Value = item.Qtty;
                        worksheet.Cell(rowIdx, 11).Value = good.PriceIn;
                        worksheet.Cell(rowIdx, 13).Value = good.PriceIn * item.Qtty;

                        rowIdx++;
                    }

                    workbook.SaveAs(Path.Combine(PathExtentions.GetApplicationRoot, "temp", name));

                    SmtpClient client = new SmtpClient("mysmtpserver");
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("zkolosvolgodonsk@mail.ru", "Aa061280");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("zkolosvolgodonsk@mail.ru");
                    mailMessage.To.Add("boykokv@yandex.ru");
                    mailMessage.Body = "body";
                    mailMessage.Subject = "Тестовый заказ от МИР";
                    mailMessage.Attachments.Add(new Attachment(Path.Combine(PathExtentions.GetApplicationRoot, "temp", name)));
                    client.Send(mailMessage);
                }

                return Json(null);
            }
        }
    }
}
