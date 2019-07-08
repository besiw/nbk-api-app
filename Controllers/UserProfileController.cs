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
    public class UserProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserProfile(int UserProfileID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            #region Validate Request Params
            bool AdminRequiredForThisRequest = false;
            RequestResponse isValidRequest = new Services.UserProfileService().ValidateRequestParams(UserProfileID, isAuthorized.UserProfileID, isAuthorized.isAdminUser, AdminRequiredForThisRequest);
            if (isValidRequest.Success == false) return BadRequest(isValidRequest);
            #endregion

            WrapperUserProfile data = new Services.UserProfileService().GetSingleUserProfile(UserProfileID);
            return Ok(data);
        }

        [HttpPut]
        public IActionResult UpdateUserProfile([FromBody]WrapperUserProfile userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            #region Validate Request Params
            bool AdminRequiredForThisRequest = false;
            RequestResponse isValidRequest =  new Services.UserProfileService().ValidateRequestParams(userParam.UserProfile.Id, isAuthorized.UserProfileID, isAuthorized.isAdminUser, AdminRequiredForThisRequest);
            if (isValidRequest.Success == false) return BadRequest(isValidRequest);
            #endregion

            WrapperUserProfile data = new Services.UserProfileService().UpdateSingleUserProfile(userParam.UserProfile);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeleteUserProfile(int UserProfileID)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            #region Validate Request Params
            bool AdminRequiredForThisRequest = true;
            RequestResponse isValidRequest = new Services.UserProfileService().ValidateRequestParams(UserProfileID, isAuthorized.UserProfileID, isAuthorized.isAdminUser, AdminRequiredForThisRequest);
            if (isValidRequest.Success == false) return BadRequest(isValidRequest);
            #endregion


            RequestResponse RequestResponse = new Services.UserProfileService().DeleteSingleUserProfile(UserProfileID);
            if (RequestResponse.Success == false) return BadRequest(RequestResponse);

            RequestResponse.isAdminUser = isAuthorized.isAdminUser;
            RequestResponse.UserProfileID = UserProfileID;
            return Ok(RequestResponse);
        }


        [HttpPost]
        public IActionResult CreatUserProfile([FromBody]WrapperUserProfile userParam)
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            #region Validate Request Params
            bool AdminRequiredForThisRequest = true;
            RequestResponse isValidRequest = new Services.UserProfileService().ValidateRequestParams(0, isAuthorized.UserProfileID, isAuthorized.isAdminUser, AdminRequiredForThisRequest);
            if (isValidRequest.Success == false) return BadRequest(isValidRequest);
            #endregion

            WrapperUserProfile data = new Services.UserProfileService().CreateSingleUserProfile(userParam.UserProfile);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAllUserProfile()
        {
            #region Validate Token
            RequestResponse isAuthorized = new Authorize().RequestTokenAuth(Request);
            if (isAuthorized.Success == false) return BadRequest(isAuthorized);
            #endregion

            #region Validate Request Params
            bool AdminRequiredForThisRequest = true;
            RequestResponse isValidRequest = new Services.UserProfileService().ValidateRequestParams(0, isAuthorized.UserProfileID, isAuthorized.isAdminUser, AdminRequiredForThisRequest);
            if (isValidRequest.Success == false) return BadRequest(isValidRequest);
            #endregion

            WrapperMultiUserProfile data = new Services.UserProfileService().GetAllUserProfile();
            return Ok(data);
        }


        [HttpGet]
        public IActionResult ForgotPassword(string Email)
        {
            RequestResponse RequestResponse = new Services.UserProfileService().ForgotPassword(Email);
            return Ok(RequestResponse);
        }
    }
}