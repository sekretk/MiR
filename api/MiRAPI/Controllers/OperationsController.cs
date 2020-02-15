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
        public JsonResult List([FromQuery] Page page)
        {
            using (var db = new IR2016DB())
            {
                return Json(new PageResult<Operation>
                {
                    Items = db.Operations.Skip(page.Skip).Take(page.Size),
                    Skiped = page.Skip,
                    TotalAmount = db.Operations.Count()
                });
            }
        }
    }
}
