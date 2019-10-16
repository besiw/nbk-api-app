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
using Microsoft.AspNetCore.Hosting;

namespace NBKProject.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectWorkflowController : ControllerBase
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public ProjectWorkflowController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        

        #region Workflow # 1

        #region Step # 1
        [HttpPost]
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


            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            RequestResponse data = new Services.ProjectWorkflowService().ProjectWFOne(Param.ProjectWorkflow);

            return Ok(data);
        }
        #endregion

        #region Step # 2
        [HttpPost]
        public IActionResult GetProjectWFTwoEmailFormated([FromBody]WrapperProjectWorkflow Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion
            

            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            WrapperProjectWorkflow data = new Services.ProjectWorkflowService().GetProjectWFTwoEmailFormatedWithPDF(Param.ProjectWorkflow, _hostingEnvironment);

            return Ok(data);
        }

        
        [HttpPost]
        public IActionResult ProjectWFTwo([FromForm]string request)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            
            WrapperProjectWorkflow Param = JsonConvert.DeserializeObject<WrapperProjectWorkflow>(request);
             
            IFormFile FileInRequest = Request.Form.Files[0];  
            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            RequestResponse data = new Services.ProjectWorkflowService().ProjectWFTwo(Param.ProjectWorkflow, FileInRequest, _hostingEnvironment);

            return Ok(data);
        }
        #endregion


        #region Step # 3
        [HttpPost]
        public IActionResult ProjectWFThree([FromForm]string request)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperProjectWorkflow Param = JsonConvert.DeserializeObject<WrapperProjectWorkflow>(request);
            IFormFileCollection FilesInRequest = Request.Form.Files;
            
            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            RequestResponse data = new Services.ProjectWorkflowService().ProjectWFThree(Param.ProjectWorkflow, FilesInRequest, _hostingEnvironment);

            return Ok(data);
        }

        #endregion


        #region Step # 4

        [HttpPost]
        public IActionResult GetProjectWFFourEmailFormated([FromBody]WrapperProjectWorkflow Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            WrapperProjectWorkflow data = new Services.ProjectWorkflowService().GetProjectWFFourEmailFormated(Param.ProjectWorkflow);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult ProjectWFFour([FromBody]WrapperProjectWorkflow Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            Param.ProjectWorkflow.InsertedBy = isAuthorized.UserProfileID;
            RequestResponse data = new Services.ProjectWorkflowService().ProjectWFFour(Param.ProjectWorkflow);

            return Ok(data);
        }

        #endregion

        #endregion
    }
}