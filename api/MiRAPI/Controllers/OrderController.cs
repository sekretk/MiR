using ClosedXML.Excel;
using LinqToDB;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using MiRAPI.utils;
using System;
using System.Collections.Generic;
using io = System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MiRAPI.DataModels;

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
        public JsonResult Create([FromBody] OrderDTO order)
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

                using (var workbook = new XLWorkbook(io.Path.Combine(PathExtentions.GetApplicationRoot, "templates", "kolos.xlsx")))
                {
                    var worksheet = workbook.Worksheets.First();

                    var rowIdx = 7;

                    var total = 0d;

                    foreach (var item in order.Items)
                    {
                        var good = db.Goods.First(g => g.ID == item.GoodId);

                        worksheet.Cell(rowIdx, 2).Value = rowIdx - 6;
                        worksheet.Cell(rowIdx, 3).Value = good.Code;
                        worksheet.Cell(rowIdx, 4).Value = good.Name;
                        worksheet.Cell(rowIdx, 7).Value = good.BarCode1;
                        //worksheet.Cell(rowIndex, columnIndex).Style.NumberFormat.Format = "$0.00";
                        worksheet.Cell(rowIdx, 7).DataType = XLDataType.Text;
                        worksheet.Cell(rowIdx, 8).Value = "штука";
                        worksheet.Cell(rowIdx, 10).Value = item.Qtty;
                        worksheet.Cell(rowIdx, 11).Value = good.PriceIn;
                        worksheet.Cell(rowIdx, 13).Value = good.PriceIn * item.Qtty;

                        total += (good.PriceIn??0) * item.Qtty;

                        rowIdx++;
                    }

                    worksheet.Cell(rowIdx, 13).Value = total;

                    workbook.SaveAs(io.Path.Combine(PathExtentions.GetApplicationRoot, "temp", name));

                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("zkolosvolgodonsk",
                    "zkolosvolgodonsk@mail.ru");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("User",
                    "boykokv@yandex.ru");
                    message.To.Add(to);
                    message.To.Add(new MailboxAddress("sambuk", "sambuk@ivtex.net"));                    

                    message.Subject = "Тестовый заказ от МИР";

                    var body = new TextPart("plain")
                    {
                        Text = @"Заказ во вложении"
                    };

                    // create an image attachment for the file located at path
                    var attachment = new MimePart("image", "gif")
                    {
                        Content = new MimeContent(io.File.OpenRead(io.Path.Combine(PathExtentions.GetApplicationRoot, "temp", name))),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = name
                    };

                    // now create the multipart/mixed container to hold the message text and the
                    // image attachment
                    var multipart = new Multipart("mixed");
                    multipart.Add(body);
                    multipart.Add(attachment);

                    // now set the multipart/mixed as the message body
                    message.Body = multipart;

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.mail.ru", 465, true);
                    client.Authenticate("zkolosvolgodonsk@mail.ru", "Aa061280");

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }

                return Json(null);
            }
        }

        [HttpGet()]
        [Route("list")]
        public JsonResult List([FromQuery] Page filter)
        {
            using (var db = new MiRDB())
            {
                var operations = db.Operations.Where(o => o.OperType == 13).GroupBy(o => o.Acct);;

                return Json(new PageResult<OrdersResult>
                {
                    Items = operations
                            .OrderBy(o => o.Key)
                            .Skip(filter.Skip)
                            .Take(filter.Size)
                            .Select(go => 
                                new OrdersResult(go.Key.GetValueOrDefault(), go.First().Date.GetValueOrDefault()))
                            .ToArray(),
                    Skiped = filter.Skip,
                    TotalAmount = operations.Count(),
                });
            }
        }
    }
}
