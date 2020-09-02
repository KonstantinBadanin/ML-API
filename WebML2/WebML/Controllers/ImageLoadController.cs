using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase;

namespace WebML.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageLoadController : Controller
    {
        [HttpPost]
        public string Post(In b)//единицы числа b это класс айтема, остальные разряды номер позиции
        {
            byte[] blob = Usage.GetBlobFromDBByNumAndClass((b.Name%10).ToString(), b.Name/10);
            if (blob != null)
                return Convert.ToBase64String(blob);
            else
                return "Error";
        }

        [HttpOptions]
        public string Options()
        {
            return "Allow: POST";
        }
    }
}