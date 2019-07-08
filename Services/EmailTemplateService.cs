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
    public class EmailTemplateService
    {
        public WrapperEmailTemplate GetSingleEmailTemplate(int id)
        {
            WrapperEmailTemplate data = new WrapperEmailTemplate();
            data.EmailTemplate = new EmailTemplateCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiEmailTemplates GetAllEmailTemplate()
        {
            WrapperMultiEmailTemplates data = new WrapperMultiEmailTemplates();
            data.MultiEmailTemplates = new EmailTemplateCRUD().GetAll();
            return data;
        }

        public WrapperEmailTemplate UpdateSingleEmailTemplate(EmailTemplateENT UserProfile)
        {
            WrapperEmailTemplate data = new WrapperEmailTemplate();
            data.EmailTemplate = new EmailTemplateCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperEmailTemplate CreateSingleEmailTemplate(EmailTemplateENT UserProfile)
        {
            WrapperEmailTemplate data = new WrapperEmailTemplate();
            data.EmailTemplate = new EmailTemplateCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSingleEmailTemplate(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new EmailTemplateCRUD().DeleteSingle(id);
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
