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
    public class PartyTypeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPartyType(int PartyTypeID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperPartyType data = new Services.PartyTypeService().GetSinglePartyType(PartyTypeID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdatePartyType([FromBody]WrapperPartyType userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperPartyType data = new Services.PartyTypeService().UpdateSinglePartyType(userParam.PartyType);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeletePartyType(int PartyTypeID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            RequestResponse RequestResponse = new Services.PartyTypeService().DeleteSinglePartyType(PartyTypeID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatPartyType([FromBody]WrapperPartyType userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperPartyType data = new Services.PartyTypeService().CreateSinglePartyType(userParam.PartyType);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllPartyType()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperMultiPartyTypes data = new Services.PartyTypeService().GetAllPartyType();
            return Ok(data);
        }
    }
}