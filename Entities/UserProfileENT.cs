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
    public class WrapperUserProfile
    {
        public UserProfileENT UserProfile { get; set; }
    }

    public class WrapperMultiUserProfile
    {
        public List<UserProfileENT> MultiUserProfiles { get; set; }
    }
    public class UserProfileENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }
        public string Designation { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int? UserTypeId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool? IsActive { get; set; }
        public string Picture { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int? ContactId { get; set; }
        //public string FullName { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public string ContactNo { get; set; }
    }
}
