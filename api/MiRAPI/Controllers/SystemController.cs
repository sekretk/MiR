using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiRAPI.DataModels;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using MiRAPI.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiRAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController : Controller
    {
        private readonly IConfiguration _configuration;

        public SystemController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("version")]
        public JsonResult Version()
        {
            string[] changes = SystemVersion.Changes();

            return Json(new { version = MiRAPI.Version.BUILD_VERSION, date = MiRAPI.Version.BUILD_DATE, changes });
        }

        [HttpGet()]
        [Route("objects")]
        public JsonResult Objects()
        {
            using (var db = new MiRDB())
            {
                return Json(db.Objects.ToArray());
            }
        }

        [HttpGet()]
        [Route("ping")]
        public ActionResult Ping()
        {
            return Ok();
        }

        [HttpPost()]
        [Route("configuration")]
        public ActionResult Configuration([FromBody] MiRConfiguration config)
        {
            using (var db = new MiRDB())
            {
                var eSet = db.Configurations.FirstOrDefault(c => c.Key == Consts.EMainSettings);

                if (eSet != null)
                {
                    db.Configurations
                        .Where(c => c.Key == Consts.EMainSettings)
                        .Set(c => c.Value, JsonConvert.SerializeObject(config))
                        .Update();
                } else
                {
                    db.Configurations
                        .Value(c => c.Value, JsonConvert.SerializeObject(config))
                        .Insert();
                }

                return Ok();
            }
        }

        [HttpGet()]
        [Route("configuration")]
        public JsonResult Configuration()
        {
            using (var db = new MiRDB())
            {
                return Json(1);
            }
        }
    }
}
