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
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetService(int ServiceID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperService data = new Services.ServiceService().GetSingleService(ServiceID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateService([FromBody]WrapperService Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperService data = new Services.ServiceService().UpdateSingleService(Param.Service);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteService(int ServiceID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.ServiceService().DeleteSingleService(ServiceID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatService([FromBody]WrapperService Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperService data = new Services.ServiceService().CreateSingleService(Param.Service);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllService(int PageNo, string SearchByName, string SearchByDescription)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all Services
            //1 means 1-10 , 2 means 20-30
            WrapperMultiService data = new Services.ServiceService().GetAllService(PageNo, SearchByName, SearchByDescription);
            return Ok(data);
        }
    }
}