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
using NBKProject.Models.CRUD;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NBKProject.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MiscellaneousController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPostCodes()
        {
            #region Validate Token
            HttpRequest request = Request;
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            WrapperPostCode data = new Services.MiscellaneousService().GetAllPostCode();
            return Ok(data);
        }

    }
}