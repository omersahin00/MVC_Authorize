using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TokenAppTest.Managers;
using TokenAppTest.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace TokenAppTest.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpRequestManager _requestFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _requestFactory = new HttpRequestManager(httpClientFactory);
        }


        [HttpGet]
        public IActionResult Index()
        {
            string? tokenClaim = HttpContext.User.FindFirst("Token")?.Value;
            TempData["Message"] = tokenClaim;
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            string apiUrl = "https://localhost:7052/api/Login";
            string jsonProduct = JsonConvert.SerializeObject(user);
            var result = await _requestFactory.SendHttpPostRequest(apiUrl, jsonProduct);

            Token? Token = JsonConvert.DeserializeObject<Token>(result);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim("Token", Token.token)
            };
            var userIdentity = new ClaimsIdentity(claims, " ");
            var authProporties = new AuthenticationProperties
            {
                IsPersistent = false,
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
            };
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal, authProporties);

            return RedirectToAction("Index", "Login");
        }
    }
}

