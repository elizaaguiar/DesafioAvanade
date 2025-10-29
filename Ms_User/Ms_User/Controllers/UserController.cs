using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms_User.DTOs;
using Ms_User.Services;

namespace Ms_User.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet("need-auth")]
        [Authorize]
        public IActionResult NeedAuth()
        {
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Create(LoginDTO loginDTO)
        {
            var (success, errors) = await _userService.CreateUser(loginDTO);
            if (success)
            {
                return Ok();
            }
            return BadRequest(errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody]LoginDTO loginDTO)
        {
            var login = await _userService.UserLogin(loginDTO);
            return Ok(login);
        }
    }
}