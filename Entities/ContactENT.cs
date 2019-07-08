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
using System.ComponentModel.DataAnnotations;

namespace NBKProject.Entities
{

    public class WrapperContact
    {
        public ContactENT Contact { get; set; }
    }

    public class WrapperMultiContact
    {
        public List<ContactENT> MultiContact { get; set; }
    }
    public class ContactENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [StringLength(8, ErrorMessage = "Number must be of 8 digits", MinimumLength = 8)]
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }

    }

}
