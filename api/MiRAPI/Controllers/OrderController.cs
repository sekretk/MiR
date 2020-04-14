using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using MiRAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
               
                return Json(null);
            }
        }
    }
}
