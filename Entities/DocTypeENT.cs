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

    public class WrapperDocType
    {
        public DocTypeENT DocType { get; set; }
    }

    public class WrapperMultiDocTypes
    {
        public List<DocTypeENT> MultiDocTypes { get; set; }
    }
    public class DocTypeENT
    {
        public int Id { get; set; }
        public int? PartyTypeId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string DocName { get; set; }
        public bool? isRequired { get; set; }
    }

}
