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

namespace NBKProject.Services
{
    //public interface IAuthService
    //{
    //    bool RequestTokenAuth(HttpRequest request);
        
    //}

    public class Authorize //: IAuthService
    {

        public RequestResponse RequestTokenAuth(HttpRequest request)
        {
            IEnumerable<string> headerValues = request.Headers.GetCommaSeparatedValues("Authorization");
            string SplitToken = headerValues.FirstOrDefault().Split(' ').Last();
            var id = headerValues.FirstOrDefault();
            string Token = SplitToken;
            //GetName(Token);
            var stream = SplitToken;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(Token) as JwtSecurityToken;
            var ID = tokenS.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            var Expiry = tokenS.Claims.FirstOrDefault(claim => claim.Type == "exp").Value;
            var Secret = tokenS.Claims.FirstOrDefault(claim => claim.Type == "iat").Value;
            int UserId = Convert.ToInt32(ID);
            string ValidateStatus = new UserCRUD().ValidateToken(UserId, Token);
            RequestResponse ObjReq = new RequestResponse();
            ObjReq.Message = ValidateStatus;
            ObjReq.UserProfileID = UserId;
            if (ValidateStatus == "TokenExpired" || ValidateStatus == "IncorrectToken")
            {                   
                    ObjReq.Success = false;                
            }
            else
            {
                bool? isAdmin = new UserCRUD().IsAdminUser(UserId);
                if (isAdmin != null)
                {
                    if (isAdmin == true)
                    {
                        ObjReq.isAdminUser = true;
                    }
                    else
                    {
                        ObjReq.isAdminUser = false;
                    }
                }
                else
                {
                    ObjReq.isAdminUser = false;
                }

                ObjReq.Success = true;
            }
            return ObjReq;
        }
    }
}
