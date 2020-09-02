using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase;

namespace WebML.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string[] Get()
        {
            return Usage.GetCountersFromDB().ToArray();
        }

        [HttpOptions]
        public string Options()
        {
            return "Allow: GET";
        }
    }
}