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

        [Required(ErrorMessage = "{0} is required")]
        public List<ChecklistItemTemplate> ChecklistItemTemplateList { get; set; }
        public Service CheckListAttchedWithService { get; set; }

    }

    public partial class ChecklistItemTemplateENT
    {
        public int Id { get; set; }
        public int? ChecklistId { get; set; }
        public string Title { get; set; }
    }
}
