using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using TokenAPITest.Entities;
using TokenAPITest.Models;

namespace TokenAPITest.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        [HttpPost("api/Photos/Upload")]
        public IActionResult Upload([FromBody] PhotoUploadModel model)
        {
            if (model == null || model.PhotoData == null || model.PhotoData.Length == 0)
            {
                return BadRequest("Geçersiz fotoğraf verisi");
            }

            string fileName = Guid.NewGuid().ToString() + ".png";

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "/Users/omersahin/Desktop/WithToken/API/TokenAPITest/Images", fileName);

            System.IO.File.WriteAllBytes(filePath, model.PhotoData);


            return Ok("Fotoğraf kayıt edildi.");
        }
    }

}

