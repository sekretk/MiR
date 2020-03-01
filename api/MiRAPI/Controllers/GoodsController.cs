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
        public JsonResult List([FromQuery] GoodsFilter filter)
        {
            using (var db = new MiRDB())
            {
                IEnumerable<GoodResult> filterResult;

                if (filter.GroupId.HasValue)
                {
                    var parentGroup = db.GoodsGroups.FirstOrDefault(gg => gg.ID == filter.GroupId);

                    var groupResults = db.GoodsGroups
                                            .Where(gg => gg.Code.StartsWith(parentGroup.Code) && gg.ID != parentGroup.ID)
                                            .Select(gg => new GoodResult(gg))
                                            .ToArray();

                    filterResult = db.Goods
                        .Where(g => g.GroupID == filter.GroupId)
                        .Select(g => new GoodResult(g))
                        .ToArray()
                        .Union(groupResults);
                }
                else
                    filterResult =
                        db.GoodsGroups
                            .Where(gg => gg.Code.Length == 3)
                            .Select(gg => new GoodResult(gg))
                            .ToArray()
                            .Union(
                                db.Goods
                                    .Where(g => g.GroupID == -1 || g.GroupID == 1 /*служебная группа*/)
                                    .Select(g => new GoodResult(g))
                                    .ToArray()
                                );

                return Json(new GoodsPageResult
                {
                    Items = filterResult
                            .OrderBy(g => !g.IsGroup)
                            .ThenBy(g => g.ID)
                            .Skip(filter.Skip)
                            .Take(filter.Size)
                            .ToArray(),
                    Skiped = filter.Skip,
                    TotalAmount = filterResult.Count(),
                    GroupId = filter.GroupId
                });
            }
        }
    }
}
