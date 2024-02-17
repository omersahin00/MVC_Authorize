using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokenAPITest.Builder;
using TokenAPITest.Entities;
using TokenAPITest.Concrete;

namespace TokenAPITest.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly SqlDbContext _context;

        public UserController (SqlDbContext context)
        {
            _context = context;
        }


        [HttpPost("api/Login")]
        public IActionResult Login(User user)
        {
            string username = user.UserName;
            string password = user.Password;

            if (IsValidUser(username, password))
            {
                int userId = GetUserId(username);
                string token = TokenBuilder.GenerateToken(userId.ToString(), username);

                Token? oldToken = _context.Tokens.FirstOrDefault(x => x.UserID == userId);
                if (oldToken != null && token != null)
                {
                    oldToken.UserToken = token;
                    _context.Tokens.Update(oldToken);
                    _context.SaveChanges();
                }
                else
                {
                    if (token != null)
                    {
                        Token newToken = new Token()
                        {
                            UserID = userId,
                            UserToken = token
                        };
                        _context.Tokens.Add(newToken);
                        _context.SaveChanges();
                    }
                }
                
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }


        private bool IsValidUser(string username, string password)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (user != null)
                return true;
            else
                return false;
        }

        private int GetUserId(string username)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if (user != null)
                return user.ID;
            else
                return 0;
        }
    }
}

