using BlazorProject.Server.Helpers;
using BlazorProject.Server.Models;
using BlazorProject.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public ActionResult Authenticate(AuthenticationRequest model)
        {
            var response = _userService.Authenticate(model);
            if (response == null)
                return BadRequest(new { message = "Username atau Password tidak benar" });
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
