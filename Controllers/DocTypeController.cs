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
    public class DocTypeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDocType(int DocTypeID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperDocType data = new Services.DocTypeService().GetSingleDocType(DocTypeID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateDocType([FromBody]WrapperDocType userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperDocType data = new Services.DocTypeService().UpdateSingleDocType(userParam.DocType);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteDocType(int DocTypeID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.DocTypeService().DeleteSingleDocType(DocTypeID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatDocType([FromBody]WrapperDocType userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperDocType data = new Services.DocTypeService().CreateSingleDocType(userParam.DocType);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllDocType()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperMultiDocTypes data = new Services.DocTypeService().GetAllDocType();
            return Ok(data);
        }
    }
}