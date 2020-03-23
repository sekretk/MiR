using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationsController : Controller
    {
        private readonly IConfiguration _configuration;

        public OperationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("list")]
        public JsonResult List([FromQuery] OperationsPage page)
        {
            using (var db = new MiRDB())
            {
                Func<Operation, bool> opFilter = 
                    o => o.OperType == 2 
                            && o.ObjectID == page.ObjectId 
                            && o.Date == page.Date.Date;

                var payments =
                    from o in db.Operations.Where(opFilter).ToArray()
                    join p in db.Payments.Where(_ => _.Mode == 1)
                    on new { o.Acct, o.ObjectID, o.OperType } equals new { p.Acct, p.ObjectID, p.OperType } 
                    select new { p.Type, p.Mode, p.Qtty };


                var cash = payments.Where(_ => _.Type == 1/*cash*/).Sum(_ => _.Qtty);
                var card = payments.Where(_ => _.Type == 3/*card*/).Sum(_ => _.Qtty);

                var operationsCount = db.Operations.Where(opFilter).GroupBy(g => g.Acct).ToArray().Count();

                var averageWeight = operationsCount == 0 ? 0 :
                    db.Operations.Where(opFilter).ToArray().Sum(_ => _.Qtty * _.PriceOut) / operationsCount;

                return Json(new OperationsResult
                {
                    Items = db.Operations
                                .Where(opFilter)
                                .OrderBy(g => g.Acct)
                                .GroupBy(g => g.Acct)
                                .Select(_ => new DataModel.OperationAggregation()
                                    {
                                        Acct = _.Key,
                                        Date = _.Max(gg => gg.Date),
                                        PositionsCount = _.Count()
                                    })
                                .Skip(page.Skip)
                                .Take(page.Size)
                                .ToArray(),
                    Skiped = page.Skip,
                    Date = page.Date.Date,
                    TotalAmount = operationsCount,
                    Cash = (decimal)cash,
                    Card = (decimal)cash,
                    Average = (decimal)averageWeight
                });
            }
        }

        [HttpGet()]
        [Route("positions")]
        public JsonResult Positions(int operationAcct)
        {
            using (var db = new MiRDB())
            {
                return Json(db.Operations
                    .Where(o => o.OperType == 2)
                    .Where(o => o.Acct == operationAcct)
                    .Join(db.Goods, _ => _.GoodID, _ => _.ID, (o, g) => new { g.ID, g.Name, o.Qtty }).ToArray());
            }
        }
    }
}
