using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recognition;
using DataBase;
using System.Data.Entity;

namespace WebML.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InferenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public List<byte[]> Post(string Cyfer)
        {
            //Do inference.
            List<byte[]> lst = new List<byte[]>();
            using (Context db = new Context())
            {
                foreach (var blob in db.Items.Where(x => x.Cyfer == Cyfer).Select(x => x.Blob.Blob))
                {
                    lst.Add(blob);
                }
            }
            return lst;
        }
    }
}