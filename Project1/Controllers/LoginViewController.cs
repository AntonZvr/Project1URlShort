using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.ViewModels;
using WebApplication1.DAL.Models;
using WebApplication1.ServiceInterfaces;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginViewModel model)
        {
   
            var user = _userService.Authenticate(model.LoginUsername, model.LoginPassword);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Username = user.Username });
        }

        // POST
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            try
            {
                var createdUser = _userService.Register(model);

                return Ok(new { Id = createdUser.Id, FirstName = createdUser.FirstName, LastName = createdUser.LastName, Username = createdUser.Username, Role = createdUser.Role });
            }
            catch (Exception ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
