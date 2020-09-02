using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebML.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CleaningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpDelete]
        public void Delete()
        {
            Usage.Clean();
        }
    }
}