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
    public class CompanyProfileService
    {
        public WrapperCompanyProfile GetProfile()
        {
            WrapperCompanyProfile data = new WrapperCompanyProfile();
            data.CompanyProfile = new CompanyCRUD().SelectAll();
            return data;
        }

        public CompanyProfileENT Update(CompanyProfileENT Obj)
        {
            CompanyProfileENT data = new CompanyCRUD().Update(Obj);
            return data;
        }
    }
}
