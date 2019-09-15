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
    public class WorkflowCategoryController : ControllerBase
    {
        #region WorkflowCategory

        [HttpGet]
        public IActionResult GetWorkflowCategory(int WorkflowCategoryID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperWorkflowCategory data = new Services.WorkflowCategoryService().GetSingleWorkflowCategory(WorkflowCategoryID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateWorkflowCategory([FromBody]WrapperWorkflowCategory userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperWorkflowCategory data = new Services.WorkflowCategoryService().UpdateSingleWorkflowCategory(userParam.WorkflowCategory);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteWorkflowCategory(int WorkflowCategoryID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.WorkflowCategoryService().DeleteSingleWorkflowCategory(WorkflowCategoryID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);
            
            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatWorkflowCategory([FromBody]WrapperWorkflowCategory userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperWorkflowCategory data = new Services.WorkflowCategoryService().CreateSingleWorkflowCategory(userParam.WorkflowCategory);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllWorkflowCategory()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperMultiWorkflowCategory data = new Services.WorkflowCategoryService().GetAllWorkflowCategory();
            return Ok(data);
        }

        #endregion

        #region WorkflowCategorySteps

        [HttpGet]
        public IActionResult GetWorkflowCategoryStepsForOneWorkflow(int WorkflowCategoryID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperMultiWorkflowCategorySteps data = new Services.WorkflowCategoryService().GetSingleWorkflowCategoryStepsForOneWorkflow(WorkflowCategoryID);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreatWorkflowCategoryStep([FromBody]WrapperWorkflowCategoryStep userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            WrapperWorkflowCategoryStep data = new Services.WorkflowCategoryService().CreateSingleWorkflowCategoryStep(userParam.WorkflowCategoryStep);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetSingleWorkflowCategoryStep(int WorkflowCategoryStepID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperWorkflowCategoryStep data = new Services.WorkflowCategoryService().GetSingleWorkflowCategoryStep(WorkflowCategoryStepID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateSingleWorkflowCategoryStep([FromBody]WrapperWorkflowCategoryStep userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion
                       
            WrapperWorkflowCategoryStep data = new Services.WorkflowCategoryService().UpdateSingleWorkflowCategoryStep(userParam.WorkflowCategoryStep);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteWorkflowCategorySteps(int WorkflowCategoryID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion
                       
            RequestResponse RequestResponse = new Services.WorkflowCategoryService().DeleteSingleWorkflowCategory(WorkflowCategoryID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);
            return Ok(RequestResponse);
        }


      

        #endregion
    }
}