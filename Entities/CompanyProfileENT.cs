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

namespace NBKProject.Entities
{
    public class WrapperCompanyProfile
    {
        public CompanyProfileENT CompanyProfile { get; set; }
    }
    public class CompanyProfileENT
    {        
        public int Id { get; set; }        
        [Required (ErrorMessage = "CompanyName is required")]
        public string companyName { get; set; }
        public string organizationalNumber { get; set; }
        public string address { get; set; }
        public string ownerName { get; set; }
        public int? postCode { get; set; }        
        //public string EmailAddress { get; set; }
        [StringLength(8, ErrorMessage ="Number must be of 8 digits", MinimumLength = 8)]
        public string telephone { get; set; }
        [StringLength(8, ErrorMessage = "Number must be of 8 digits", MinimumLength = 8)]
        public string mobile { get; set; }
        public string nameOnEmailAddress { get; set; }
        [Required(ErrorMessage = "SenderEmailAddress is required")]
        public string senderEmailAddress { get; set; }
    }
}
