using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TokenAppTest.Entities;
using TokenAppTest.Managers;
using TokenAppTest.Models;

namespace TokenAppTest.Controllers
{
    public class PhotoController : Controller
    {
        private readonly HttpClient _apiClient;

        public PhotoController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient();
            _apiClient.BaseAddress = new Uri("https://localhost:7052/");
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewBag.ErrorMessage = "Foto seçilmedi";
                return View();
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await photo.CopyToAsync(stream);
                    var photoData = stream.ToArray();

                    // API'ye fotoğrafı gönder
                    var model = new PhotoUploadModel { PhotoData = photoData };
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await _apiClient.PostAsync("api/Photos/Upload", jsonContent);

                    ViewBag.Message = response.StatusCode;
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Exception error";
                return View();
            }
        }


    }
}

