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
    public class BuildingSupplierController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBuildingSupplier(int BuildingSupplierID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion


            WrapperBuildingSupplier data = new Services.BuildingSupplierService().GetSingleBuildingSupplier(BuildingSupplierID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateBuildingSupplier([FromBody]WrapperBuildingSupplier userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperBuildingSupplier data = new Services.BuildingSupplierService().UpdateSingleBuildingSupplier(userParam.BuildingSupplier);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteBuildingSupplier(int BuildingSupplierID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            

            RequestResponse RequestResponse = new Services.BuildingSupplierService().DeleteSingleBuildingSupplier(BuildingSupplierID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);
            
            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatBuildingSupplier([FromBody]WrapperBuildingSupplier userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperBuildingSupplier data = new Services.BuildingSupplierService().CreateSingleBuildingSupplier(userParam.BuildingSupplier);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllBuildingSupplier()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion



            WrapperMultiBuildingSuppliers data = new Services.BuildingSupplierService().GetAllBuildingSupplier();
            return Ok(data);
        }

    }
}