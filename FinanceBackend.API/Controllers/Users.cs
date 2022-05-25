using FinanceBackend.Context;
using FinanceBackend.Context.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;

namespace FinanceBackend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Users : Controller
    {
        protected readonly ApplicationContext _context = new ApplicationContext();

        [HttpGet("Login/{login}/{password}")]
        public IActionResult LoginUser(string login, string password)
        {
            var users_list = _context.Users.ToList();

            if (users_list.All(x => x.Username == login) ||
                users_list.Any(x => (x.Username == login) && (x.Password != password)))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("Register/{login}/{password}")]
        public IActionResult RegisterUser(string login, string password)
        {
            var users_list = _context.Users.ToList();

            if (users_list.All(x => x.Username != login))
            {
                User newUser = new User()
                {
                    Username = login,
                    Password = password
                };

                try
                {
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                
            }

            return BadRequest();
        }
    }
}
