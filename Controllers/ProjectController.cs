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
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProjectList(int EntriesFrom, int EntriesTill)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all Projects
            //1 means 1-10 , 2 means 20-30
            WrapperMultiProject data = new Services.ProjectUDService().GetAllProjectList(EntriesFrom, EntriesTill);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllProjectListNotArchivedOrDeleted(int EntriesFrom, int EntriesTill)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all Projects
            //1 means 1-10 , 2 means 20-30
            WrapperMultiProject data = new Services.ProjectUDService().GetAllProjectListNotArchivedOrDeleted(EntriesFrom, EntriesTill);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllArchivedProjectList(int EntriesFrom, int EntriesTill)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all Projects
            //1 means 1-10 , 2 means 20-30
            WrapperMultiProject data = new Services.ProjectUDService().GetAllArchivedProjectList(EntriesFrom, EntriesTill);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllDeletedProjectList(int EntriesFrom, int EntriesTill)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            //ZERO (0) page number means all Projects
            //1 means 1-10 , 2 means 20-30
            WrapperMultiProject data = new Services.ProjectUDService().GetAllDeletedProjectList(EntriesFrom, EntriesTill);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetProject(int ProjectID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperProject data = new Services.ProjectUDService().GetSingleProject(ProjectID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateProject([FromBody]WrapperProject Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperProject data = new Services.ProjectUDService().UpdateSingleProject(Param.Project);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteProject(int ProjectID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.ProjectUDService().DeleteSingleProject(ProjectID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatProject([FromBody]WrapperProject Param)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperProject data = new Services.ProjectUDService().CreateSingleProject(Param.Project);

            return Ok(data);
        }

       
    }
}