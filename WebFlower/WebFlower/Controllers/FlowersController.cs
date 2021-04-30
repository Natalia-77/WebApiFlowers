using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFlower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResults()
        {
            return Ok(new { Name = "Rose", Family = "Rosaceae", Weight = 55 });
        }
    }
}
