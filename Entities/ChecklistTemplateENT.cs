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
    public class WrapperChecklistTemplate
    {
        public ChecklistTemplateENT ChecklistTemplate { get; set; }
    }

    public class WrapperMultiChecklistTemplate
    {
        public List<ChecklistTemplateENT> MultiChecklistTemplate { get; set; }
    }
    public class ChecklistTemplateENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Title { get; set; }
        public bool? IsDefault { get; set; }
        public int ServiceSelectedID { get; set; }

        //[Required(ErrorMessage = "{0} is required")]
        public List<ChecklistItemTemplateENT> ChecklistItemTemplateList { get; set; }
        public ServiceItemTemplateENT CheckListAttchedWithService { get; set; }

    }

    public class ChecklistItemTemplateENT
    {
        public int Id { get; set; }
        public int? ChecklistId { get; set; }
        public string Title { get; set; }
    }

    public class ServiceItemTemplateENT
    {
        
        public int Id { get; set; }
        public int? ServiceTypeId { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public int? ServiceChargedAs { get; set; }
        public int? ChecklistTempId { get; set; }
        
    }
}
