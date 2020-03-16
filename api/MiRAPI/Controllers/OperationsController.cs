﻿using DataModels;
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
                return Json(new PageResult<DataModel.OperationAggregation>
                {
                    Items = db.Operations
                                .Where(o => o.OperType == 2 && o.ObjectID == page.ObjectId)
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
                    TotalAmount = db.Operations.Where(o => o.OperType == 2 && o.ObjectID == page.ObjectId).GroupBy(g => g.Acct).Count()
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
