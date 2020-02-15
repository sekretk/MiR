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
    public class GoodsController : Controller
    {
        private readonly IConfiguration _configuration;

        public GoodsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("list")]
        public JsonResult List(int? groupid)
        {
            using (var db = new IR2016DB())
            {
                if (groupid.HasValue)
                {
                    var parentGroup = db.GoodsGroups.FirstOrDefault(gg => gg.ID == groupid);

                    var groupResults = db.GoodsGroups.Where(gg => gg.Code.StartsWith(parentGroup.Code)).Select(gg => new GoodResult(gg)).ToArray();

                    var result = db.Goods
                        .Where(g => g.GroupID == groupid)
                        .Select(g => new GoodResult(g))
                        .Union(groupResults)
                        .OrderBy(r => !r.IsGroup)
                        .ThenBy(r => r.Name)
                        .ToArray();

                    return Json(result);
                }

                return Json(
                    db.GoodsGroups
                        .Where(gg => gg.Code.Length == 3)
                        .Select(gg => new GoodResult(gg))
                        .Union(
                            db.Goods
                                .Where(g => g.GroupID == 1 /*служебная группа*/)
                                .Select(g => new GoodResult(g))
                            )
                        .ToArray());

            }
        }
    }
}
