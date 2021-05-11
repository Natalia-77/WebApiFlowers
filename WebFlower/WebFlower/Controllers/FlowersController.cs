using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebFlower.Entities;
using WebFlower.ModelFlowers;

namespace WebFlower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        //надає відомості про середовище веб-розміщення ,в якому виконується додаток.
        private IWebHostEnvironment _webHostEnvironment;
        private EFContext _context;
        public string _url = "https://nat77.ga/img/";
        public FlowersController(EFContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        [Route("Search")]
        public IActionResult GetResults()
        {

            //var list = new List<FlowerVM>()
            //{
            //    new FlowerVM
            //    {
            //        Name="Rose",
            //        Family="Rosaceae",
            //        Weight=55,
            //        Image=_url+"3.jpg"

            //    },
            //    new FlowerVM
            //    {
            //        Name="Rosa primula",
            //        Family="Rosaceae",
            //        Weight=85,
            //        Image=_url+"4.jpg"

            //    },
            //    new FlowerVM
            //    {
            //        Name="Rosa carolina",
            //        Family="Rosaceae",
            //        Weight=100,
            //        Image=_url+"3.jpg"
            //    },

            //};
            var list = _context.Flowers.Select(
                x => new
                {x.Id,
                x.Name,
                x.Family,
                x.Weight,
                x.Image
                });              
                       
            return Ok(list);
        }

        //Отримати дані по окремо введеному імені.
        [HttpGet("Name")]
        public IActionResult Search(string name)
        {
            //var list = new List<FlowerVM>()
            //{
            //    new FlowerVM
            //    {
            //        Name = "Rose",
            //        Family = "Rosaceae",
            //        Weight = 55,
            //        Image = _url + "3.jpg"

            //    },
            //    new FlowerVM
            //    {
            //        Name = "Rosa primula",
            //        Family = "Rosaceae",
            //        Weight = 85,
            //        Image = _url + "4.jpg"

            //    },
            //    new FlowerVM
            //    {
            //        Name = "Rosa carolina",
            //        Family = "Rosaceae",
            //        Weight = 100,
            //        Image = _url + "3.jpg"
            //    },

            //};
            var res = _context.Flowers.FirstOrDefault(y=>y.Name==name);
           
            if (res == null)
            {
                return NotFound(name);
            }
            return Ok(res);
        }


        //Окремо отримувати фото.
        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            string path = _webHostEnvironment.ContentRootPath + "\\Photos\\";
            var filepath = path + fileName + ".jpg";

            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filepath);
                return File(bytes, "image/jpg");
            }

            return NotFound(fileName + " Not found this image!");



        }

        //[HttpPost]
        //public ActionResult<FlowerVM> Post([FromBody] FlowerVM flower)
        //{
        //    if (flower == null)
        //    {
        //        return BadRequest();
        //    }

        //    var res = new List<FlowerVM>();
        //    res.Add(flower);
        //    return Ok(flower);
        //}

        //public IActionResult Post([FromBody] FlowerVM flower)
        //{
        //    var res = new FlowerVM()
        //    {
        //        Name = flower.Name,
        //        Family = flower.Family,
        //        Weight = flower.Weight,
        //        Image = flower.Image

        //    };
        //    return CreatedAtAction(
        //        nameof(GetResults),
        //        new
        //        {
        //            Name = res.Name,
        //            Family = res.Family,
        //            Weight = res.Weight,
        //            Image = res.Image
        //        }, flower);
        //}

    }
}
