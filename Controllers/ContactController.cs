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
    public class ContactController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetContact(int ContactID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperContact data = new Services.ContactService().GetSingleContact(ContactID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateContact([FromBody]WrapperContact Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperContact data = new Services.ContactService().UpdateSingleContact(Param.Contact);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteContact(int ContactID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.ContactService().DeleteSingleContact(ContactID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatContact([FromBody]WrapperContact Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperContact data = new Services.ContactService().CreateSingleContact(Param.Contact);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllContact(int PageNo, string SearchByName, string SearchByEmail, string SearchByCompany, string SearchByNumber)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all contacts
            //1 means 1-10 , 2 means 20-30
            WrapperMultiContact data = new Services.ContactService().GetAllContact(PageNo, SearchByName, SearchByEmail, SearchByCompany, SearchByNumber);
            return Ok(data);
        }
    }
}