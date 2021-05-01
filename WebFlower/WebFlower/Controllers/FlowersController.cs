using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFlower.ModelFlowers;

namespace WebFlower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        [HttpGet]
        [Route("Search")]
        public IActionResult GetResults()
        {
            //return Ok(new { Name = "Rose", Family = "Rosaceae", Weight = 55 });

            var list = new List<FlowerVM>()
            {
                new FlowerVM
                {
                    Name="Rose",
                    Family="Rosaceae",
                    Weight=55,
                    Image="2.png"

                },
                new FlowerVM
                {
                    Name="Rosa primula",
                    Family="Rosaceae",
                    Weight=85,
                    Image="3.jpg"

                },
                new FlowerVM
                {
                    Name="Rosa carolina",
                    Family="Rosaceae",
                    Weight=100,
                    Image="4.jpg"

                },

            };
            return Ok(list);
        }
    }
}
