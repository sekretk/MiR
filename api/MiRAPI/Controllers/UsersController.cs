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
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("me")]
        public JsonResult Me()
        {
            using (var db = new MiRDB())
            {

                var user = (User)HttpContext.Items[MiRConsts.USER_BAG];                

            return Json(new { id = user.ID, name = user.Name, role = db.UsersGroups.FirstOrDefault(ug => ug.ID == user.GroupID)?.Name });
            }
        }
    }
}
