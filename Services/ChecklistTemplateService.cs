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
    public class ChecklistTemplateService
    {
        public WrapperChecklistTemplate GetSingleChecklistTemplate(int id)
        {
            WrapperChecklistTemplate data = new WrapperChecklistTemplate();
            data.ChecklistTemplate = new ChecklistTemplateCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiChecklistTemplate GetAllChecklistTemplate(int PageNo, string SearchByName)
        {
            WrapperMultiChecklistTemplate data = new WrapperMultiChecklistTemplate();
            data.MultiChecklistTemplate = new ChecklistTemplateCRUD().GetAll(PageNo, SearchByName);
            return data;
        }

        public WrapperChecklistTemplate UpdateSingleChecklistTemplate(ChecklistTemplateENT Obj)
        {
            WrapperChecklistTemplate data = new WrapperChecklistTemplate();
            data.ChecklistTemplate = new ChecklistTemplateCRUD().UpdateSelectSingle(Obj);
            return data;
        }

        public WrapperChecklistTemplate CreateSingleChecklistTemplate(ChecklistTemplateENT Obj)
        {
            WrapperChecklistTemplate data = new WrapperChecklistTemplate();
            data.ChecklistTemplate = new ChecklistTemplateCRUD().CreateSingle(Obj);
            return data;
        }

        public RequestResponse DeleteSingleChecklistTemplate(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                

                new ChecklistTemplateCRUD().DeleteSingle(id);
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
    }
}
