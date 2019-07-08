using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NBKProject.Entities;
using NBKProject.Helpers;
using NBKProject.Models.NbkEF;
using NBKProject.Models.CRUD;
using System.ComponentModel.DataAnnotations;

namespace NBKProject.Services
{
    public class UserProfileService
    {
        public WrapperUserProfile GetSingleUserProfile(int id)
        {
            WrapperUserProfile data = new WrapperUserProfile();
            data.UserProfile = new UserProfileCRUD().SelectSingle(id);
            return data;
        }



        public WrapperMultiUserProfile GetAllUserProfile()
        {
            WrapperMultiUserProfile data = new WrapperMultiUserProfile();
            data.MultiUserProfiles = new UserProfileCRUD().GetAll();
            return data;
        }

        public WrapperUserProfile UpdateSingleUserProfile(UserProfileENT UserProfile)
        {
            WrapperUserProfile data = new WrapperUserProfile();
            data.UserProfile = new UserProfileCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperUserProfile CreateSingleUserProfile(UserProfileENT UserProfile)
        {
            WrapperUserProfile data = new WrapperUserProfile();
            data.UserProfile = new UserProfileCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSingleUserProfile(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new UserProfileCRUD().DeleteSingle(id);
                RequestResponse.Message = "Record deleted";
                RequestResponse.Success = true;
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }

        public RequestResponse ForgotPassword(string email)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                UserProfileENT UserProfile = new UserProfileCRUD().SearchUserByEmail(email);

                #region Send Username & Password to Email in Param
                string username = UserProfile.UserName;
                string password = UserProfile.Password;
                #endregion

                RequestResponse.Message = "Credentials sent to the user's email address";
                RequestResponse.Success = true;
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }
        public RequestResponse ValidateRequestParams(int UserProfileIdInRequest, int userProfileIdInToken, bool isAdminUser, bool AdminRequiredForThisRequest)
        {
            
            RequestResponse ObjReq = new RequestResponse();
            ObjReq.UserProfileID = userProfileIdInToken;
            if (AdminRequiredForThisRequest == false)
            {
                if (UserProfileIdInRequest == userProfileIdInToken)
                {
                    ObjReq.Message = "User Matched";
                    ObjReq.Success = true;
                }
                else if (isAdminUser == true)
                {
                    ObjReq.Message = "Admin User";
                    ObjReq.Success = true;
                }
                else
                {
                    ObjReq.Message = "User not authorized";
                    ObjReq.Success = false;
                }
            }
            else
            {
                if (isAdminUser == true)
                {
                    ObjReq.Message = "Admin User";
                    ObjReq.Success = true;
                }
                else
                {
                    ObjReq.Message = "User not authorized";
                    ObjReq.Success = false;
                }
            }
            return ObjReq;
        }

    }
}
