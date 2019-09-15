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
    #region WorkflowCategory
    public class WrapperWorkflowCategory
    {
        public WorkflowCategoryENT WorkflowCategory { get; set; }
    }

    public class WrapperMultiWorkflowCategory
    {
        public List<WorkflowCategoryENT> MultiWorkflowCategory { get; set; }
    }
    public class WorkflowCategoryENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool? IsDefault { get; set; }
    }
    public class ProjectAsociatedWithWorkflowCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class ResponseWorkflowCategory
    {
        public RequestResponse RequestResponse { get; set; }
        public List<ProjectAsociatedWithWorkflowCategory> ProjectAsociatedWithWorkflowCategory { get; set; }
    }
    #endregion

    #region WorkflowCategorySteps
    public class WrapperWorkflowCategoryStep
    {
        public WorkflowCategoryStepENT WorkflowCategoryStep { get; set; }
    }

    public class WrapperMultiWorkflowCategorySteps
    {
        public List<WorkflowCategoryStepENT> MultiWorkflowCategorySteps { get; set; }
    }
    public class WorkflowCategoryStepENT
    {
               
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int WorkflowCategoryId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string StepName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int StepSequence { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool IsTransferable { get; set; }
    }

    public class ResponseWorkflowCategorySteps
    {
        public RequestResponse RequestResponse { get; set; }
        
    }
    #endregion
}
