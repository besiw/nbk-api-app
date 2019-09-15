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


namespace NBKProject.Services
{
    public class WorkflowCategoryService
    {
        #region WorkflowCategory
        public WrapperWorkflowCategory GetSingleWorkflowCategory(int id)
        {
            WrapperWorkflowCategory data = new WrapperWorkflowCategory();
            data.WorkflowCategory = new WorkflowCategoryCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiWorkflowCategory GetAllWorkflowCategory()
        {
            WrapperMultiWorkflowCategory data = new WrapperMultiWorkflowCategory();
            data.MultiWorkflowCategory = new WorkflowCategoryCRUD().GetAll();
            return data;
        }

        public WrapperWorkflowCategory UpdateSingleWorkflowCategory(WorkflowCategoryENT UserProfile)
        {
            WrapperWorkflowCategory data = new WrapperWorkflowCategory();
            data.WorkflowCategory = new WorkflowCategoryCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperWorkflowCategory CreateSingleWorkflowCategory(WorkflowCategoryENT UserProfile)
        {
            WrapperWorkflowCategory data = new WrapperWorkflowCategory();
            data.WorkflowCategory = new WorkflowCategoryCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSingleWorkflowCategory(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new WorkflowCategoryCRUD().DeleteSingle(id);
                RequestResponse.Message = "Record deleted";
                RequestResponse.Success = true;
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }
        #endregion

        #region WorkflowCategorySteps
        public WrapperMultiWorkflowCategorySteps GetSingleWorkflowCategoryStepsForOneWorkflow(int id)
        {
            WrapperMultiWorkflowCategorySteps data = new WrapperMultiWorkflowCategorySteps();
            data.MultiWorkflowCategorySteps =  new WorkflowCategoryCRUD().SelectSingleWorkflowCategoryStepsForOneWorkflow(id);
            return data;
        }

        public WrapperWorkflowCategoryStep GetSingleWorkflowCategoryStep(int id)
        {
            WrapperWorkflowCategoryStep data = new WrapperWorkflowCategoryStep();
            data.WorkflowCategoryStep = new WorkflowCategoryCRUD().SelectSingleWorkflowCategoryStep(id);
            return data;
        }
        public WrapperWorkflowCategoryStep CreateSingleWorkflowCategoryStep(WorkflowCategoryStepENT UserProfile)
        {
            WrapperWorkflowCategoryStep data = new WrapperWorkflowCategoryStep();
            data.WorkflowCategoryStep = new WorkflowCategoryCRUD().CreateSingleWorkflowCategoryStep(UserProfile);
            return data;
        }

       
        public WrapperWorkflowCategoryStep UpdateSingleWorkflowCategoryStep(WorkflowCategoryStepENT UserProfile)
        {
            WrapperWorkflowCategoryStep data = new WrapperWorkflowCategoryStep();
            data.WorkflowCategoryStep = new WorkflowCategoryCRUD().UpdateSingleWorkflowCategoryStep(UserProfile);
            return data;
        }

        

        public RequestResponse DeleteSingleWorkflowCategorySteps(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new WorkflowCategoryCRUD().DeleteSingleWorkflowCategoryStep(id);
                RequestResponse.Message = "Record deleted";
                RequestResponse.Success = true;
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }
        #endregion
    }
}
