using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
       //global variable to access env variable from the constructor and use it across other methods
       //readonly is similar to constant, but the value needs to be assigned in the constructor and the valu and meory location are fixed
        private readonly IWebHostEnvironment _env;
        //IwebHostEnvironment is the API location on Host Environment
        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult GetImage(int id)
        {
            var webroot = _env.WebRootPath;
            var path = Path.Combine($"{webroot}/Images/", $"Image{id}.jpg");
            var buffer =  System.IO.File.ReadAllBytes(path);
            return File(buffer, "image/jpeg");
            
        }
    }
}