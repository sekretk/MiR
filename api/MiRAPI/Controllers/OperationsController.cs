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
        public JsonResult List()
        {
            using (var db = new IR2016DB())
            {

                var user = (User)HttpContext.Items[MiRConsts.USER_BAG];                

            return Json(new { id = user.ID, name = user.Name, role = db.UsersGroups.FirstOrDefault(ug => ug.ID == user.GroupID)?.Name });
            }
        }
    }
}
