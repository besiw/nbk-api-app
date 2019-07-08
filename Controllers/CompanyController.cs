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
    public class CompanyController : ControllerBase
    {
        //private IAuthService _authService;
        //public CompanyController(IAuthService authService)
        //{
        //    _authService = authService;
        //}
        //[AllowAnonymous]

        [HttpGet]
        public IActionResult GetProfile()
        {
            #region Validate Token
            HttpRequest request = Request;
            RequestResponse isAuthorized =  new Authorize().RequestTokenAuth(request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            WrapperCompanyProfile data = new Services.CompanyProfileService().GetProfile();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateProfile([FromBody]WrapperCompanyProfile userParam)
        {
            #region Validate Token
            HttpRequest request = Request;
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion
            
            CompanyProfileENT data = new Services.CompanyProfileService().Update(userParam.CompanyProfile);
            return Ok(data);
        }
    }
}