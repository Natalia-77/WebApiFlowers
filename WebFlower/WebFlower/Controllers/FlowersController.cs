using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebFlower.ModelFlowers;

namespace WebFlower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        //надає відомості про середовище веб-розміщення ,в якому виконується додаток.
        public static IWebHostEnvironment _webHostEnvironment;
        public string _url = "https://nat77.ga/img/";
        public FlowersController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        [Route("Search")]
        public IActionResult GetResults()
        {

            var list = new List<FlowerVM>()
            {
                new FlowerVM
                {
                    Name="Rose",
                    Family="Rosaceae",
                    Weight=55,
                    Image=_url+"3.jpg"

                },
                new FlowerVM
                {
                    Name="Rosa primula",
                    Family="Rosaceae",
                    Weight=85,
                    Image=_url+"4.jpg"

                },
                new FlowerVM
                {
                    Name="Rosa carolina",
                    Family="Rosaceae",
                    Weight=100,                   
                    Image=_url+"3.jpg"
                },

            };
            return Ok(list);
        }

        //Окремо отримувати фото.
        [HttpGet("{fileName}")]      

        public async Task<IActionResult>Get(string fileName)
        {
            string path = _webHostEnvironment.ContentRootPath + "\\Photos\\";
            var filepath = path + fileName + ".jpg";
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);

            //повертає масив байт і контент-тайп
            return File(bytes, "image/jpg");
        }

       
    }
}
