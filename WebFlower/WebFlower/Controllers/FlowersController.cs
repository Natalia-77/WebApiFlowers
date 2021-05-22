using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebFlower.Entities;
using WebFlower.Entities.Domain;
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

        #region Get

        [HttpGet]
        [Route("Search")]
        public IActionResult GetResults()
        {

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
            var res = _context.Flowers.FirstOrDefault(y=>y.Name==name);
           
            if (res == null)
            {
                //return NotFound($"Flower with { name} not found");
                return NotFound(new {message="There is no name!" });
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
        #endregion

        #region Post

        [HttpPost]
        [Route("add")]
        public IActionResult AddFlower([FromBody] FlowerView flower)
        {
            //_context.Flowers.Add(flower);
            //_context.SaveChanges();
            return Ok();
        }

        #endregion


        #region Put
        [HttpPut("{id}")]          
        public IActionResult Update(int id,[FromBody] Flower flower)
        {

            if (flower == null )
            {
                return BadRequest();              
            }

            var res = _context.Flowers.FirstOrDefault(x => x.Id == id);

            res.Name = flower.Name;
            res.Family = flower.Family;
            res.Weight = flower.Weight;
            res.Image = flower.Image;

            if (res == null)
            {
                return NotFound();
            }
                       
            _context.SaveChanges();

            return NoContent();
        }

        #endregion


        #region Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var del_item = _context.Flowers.Find(id);

            if (del_item == null)
            {
                return NotFound();
            }

            _context.Flowers.Remove(del_item);
            _context.SaveChanges();

            return NoContent();
        }

        #endregion
    }
}
