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
    public class EmailTemplateController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmailTemplate(int EmailTemplateID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperEmailTemplate data = new Services.EmailTemplateService().GetSingleEmailTemplate(EmailTemplateID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateEmailTemplate([FromBody]WrapperEmailTemplate userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperEmailTemplate data = new Services.EmailTemplateService().UpdateSingleEmailTemplate(userParam.EmailTemplate);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteEmailTemplate(int EmailTemplateID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.EmailTemplateService().DeleteSingleEmailTemplate(EmailTemplateID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatEmailTemplate([FromBody]WrapperEmailTemplate userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperEmailTemplate data = new Services.EmailTemplateService().CreateSingleEmailTemplate(userParam.EmailTemplate);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllEmailTemplate()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperMultiEmailTemplates data = new Services.EmailTemplateService().GetAllEmailTemplate();
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllEmailHashtags()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion
            
            return Ok(EmailHashTags.EmailHashTagsList);
        }
    }
}