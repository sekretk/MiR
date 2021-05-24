using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiRAPI.DataModels;
using MiRAPI.DTO;

namespace MiRAPI.Controllers
{
    public class CardController : Controller
    {
        [HttpGet()]
        [Route("card")]
        public JsonResult List([FromQuery] int objectId)
        {
            using (var db = new MiRDB())
            {
                Func<Operation, bool> opFilter =
                    o => o.OperType == 2
                            && o.ObjectID == objectId
                            && o.Note == "draft";

                var draftOperation = db.Operations.Where(opFilter).OrderByDescending(o => o.Date).Select(o => o.Acct).FirstOrDefault();

                return Json(

                    from goodInOperation in db.Operations.Where(o => o.Acct == draftOperation.GetValueOrDefault())
                    join goods in db.Goods on goodInOperation.ID equals goods.ID into goodsForOperation
                    from go in goodsForOperation
                    select new GoodResult(go.Name, go.ID, goodInOperation.Qtty, false));
            }
        }
    }
}