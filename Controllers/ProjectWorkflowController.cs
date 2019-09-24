﻿using System;
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
    public class ProjectWorkflowController : ControllerBase
    {
        #region Workflow # 1

        [HttpGet]
        public IActionResult GetProjectWFOneEmailFormated([FromBody]WrapperProjectWorkflow Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            WrapperProjectWorkflow data = new Services.ProjectWorkflowService().GetProjectWFOneEmailFormated(Param.ProjectWorkflow);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult ProjectWFOne([FromBody]WrapperProjectWorkflow Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse data = new Services.ProjectWorkflowService().ProjectWFOne(Param.ProjectWorkflow);

            return Ok(data);
        }

        #endregion
    }
}