﻿using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiRAPI.DTO;
using MiRAPI.Extentions;
using MiRAPI.utils;
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

    }
}
