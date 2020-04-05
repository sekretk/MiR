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

                IEnumerable<GoodResult> groupResults;
                IEnumerable<Good> rawFilteredGoods;
                int? parentGroupId = null;

                if (filter.GroupId.HasValue)
                {
                    var group = db.GoodsGroups.FirstOrDefault(gg => gg.ID == filter.GroupId);

                    groupResults = db.GoodsGroups
                                            .Where(gg => gg.Code.StartsWith(group.Code) && gg.ID != group.ID)
                                            .Select(gg => new GoodResult(gg.Name, gg.ID, null, true));

                    rawFilteredGoods = db.Goods.Where(g => g.GroupID == filter.GroupId);

                    string parentGroupCode = group.Code.Substring(0, group.Code.Length-3);

                    parentGroupId = db.GoodsGroups.FirstOrDefault(gg => gg.Code == parentGroupCode)?.ID;
                }
                else
                {
                    rawFilteredGoods = db.Goods.Where(g => g.GroupID == -1 || g.GroupID == 1 /*служебная группа*/);


                    groupResults = db.GoodsGroups
                                           .Where(gg => gg.Code.Length == 3)
                                           .Select(gg => new GoodResult(gg.Name, gg.ID, null, true));
                }

                IEnumerable<GoodResult> storeGoods =
                        from good in db.Goods.Where(g => g.GroupID == filter.GroupId)
                        join stores in db.Stores on good.ID equals stores.GoodID into goodsStore
                        from gs in goodsStore
                        where gs.ObjectID == filter.ObjectId
                        select new GoodResult(good.Name, good.ID, gs.Qtty, false);

                filterResult = storeGoods.ToArray().Union(groupResults.ToArray());

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
                    GroupId = filter.GroupId,
                    ParentGroupId = parentGroupId,
                });
            }
        }
    }
}
