using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NBKProject.Services;
using NBKProject.Entities;
using NBKProject.Models.NbkEF;

namespace NBKProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       
            private IUserService _userService;

        

        public UsersController(IUserService userService)
            {
                _userService = userService;
            }

        //private readonly NbkDbEntities _context;

        //public UsersController(NbkDbEntities context)
        //{
        //    _context = context;
        //}


            [AllowAnonymous]
            [HttpPost("authenticate")]
            public IActionResult Authenticate([FromBody]User userParam)
            {
                var user = _userService.Authenticate(userParam.UserName, userParam.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                return Ok(users);
            }

        

    }
}