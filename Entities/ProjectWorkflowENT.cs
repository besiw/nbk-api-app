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
   
    public class WrapperProjectWorkflow
    {
        public ProjectWorkflowENT ProjectWorkflow { get; set; }

    }

    public class WrapperMultiProjectWorkflow
    {
        public List<ProjectWorkflowENT> MultiProjectWorkflow { get; set; }
    }
    public class ProjectWorkflowENT
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int WorkflowId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int WorkflowStepId { get; set; }
        public bool IsTransfer { get; set; }
        public int? TaskId { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? InsertedBy { get; set; }
        public string EmailContent { get; set; }
        public string EmailSubject { get; set; }
    }

    
}
