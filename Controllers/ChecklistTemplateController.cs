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
    public class ChecklistTemplateController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetChecklistTemplate(int ChecklistTemplateID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperChecklistTemplate data = new Services.ChecklistTemplateService().GetSingleChecklistTemplate(ChecklistTemplateID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateChecklistTemplate([FromBody]WrapperChecklistTemplate Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperChecklistTemplate data = new Services.ChecklistTemplateService().UpdateSingleChecklistTemplate(Param.ChecklistTemplate);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteChecklistTemplate(int ChecklistTemplateID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.ChecklistTemplateService().DeleteSingleChecklistTemplate(ChecklistTemplateID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatChecklistTemplateWithItems([FromBody]WrapperChecklistTemplate Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperChecklistTemplate data = new Services.ChecklistTemplateService().CreateSingleChecklistTemplate(Param.ChecklistTemplate);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllChecklistTemplate(int PageNo, string SearchByName)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all ChecklistTemplates
            //1 means 1-10 , 2 means 20-30
            WrapperMultiChecklistTemplate data = new Services.ChecklistTemplateService().GetAllChecklistTemplate(PageNo, SearchByName);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreatChecklistItemTempByChecklistTempId([FromBody]WrapperChecklistItemTemplate Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperChecklistItemTemplate data = new Services.ChecklistTemplateService().CreateSingleChecklistItemTempByChecklistTempId(Param.ChecklistItemTemplate);

            return Ok(data);
        }
    }
}