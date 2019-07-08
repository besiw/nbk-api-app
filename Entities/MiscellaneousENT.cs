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
    public class MiscellaneousENT
    {
    }

    public class WrapperPostCode
    {
        public List<PostCodeENT> postCodes { get; set; }
    }
    public class PostCodeENT
    {
        public int Id { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Kommunenummer { get; set; }
        public string Kommunenavn { get; set; }
        public string Kategori { get; set; }

    }
}
