﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // POST: api/users/authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] RegisterRequest model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Username = user.Username });
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            try
            {
                // Create User
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Password = model.Password,
                    Role = model.Role
                };
                var createdUser = _userService.Register(user);

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
