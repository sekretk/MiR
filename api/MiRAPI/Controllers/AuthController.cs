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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<object> Login([FromBody] AuthModel authModel)
        {
            using (var db = new IR2016DB())
            {
                var pwd = Crypto.EncryptText(authModel.password);

                var user = db.Users.FirstOrDefault(u => u.Name == authModel.user && u.Password == pwd);

                if (user == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
                else
                {
                    return new { token = AppState.AddToken(user) };
                }
            }
        }

        [HttpDelete]
        [Route("signout")]
        public ActionResult SignOut()
        {
            AppState.RemoveAuth(HttpContext.Request.Headers[MiRConsts.Authorization]);

            return Ok();
        }
    }
}
