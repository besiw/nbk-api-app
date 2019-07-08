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
    public class DocTypeService
    {
        public WrapperDocType GetSingleDocType(int id)
        {
            WrapperDocType data = new WrapperDocType();
            data.DocType = new DocTypeCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiDocTypes GetAllDocType()
        {
            WrapperMultiDocTypes data = new WrapperMultiDocTypes();
            data.MultiDocTypes = new DocTypeCRUD().GetAll();
            return data;
        }

        public WrapperDocType UpdateSingleDocType(DocTypeENT UserProfile)
        {
            WrapperDocType data = new WrapperDocType();
            data.DocType = new DocTypeCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperDocType CreateSingleDocType(DocTypeENT UserProfile)
        {
            WrapperDocType data = new WrapperDocType();
            data.DocType = new DocTypeCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSingleDocType(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new DocTypeCRUD().DeleteSingle(id);
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
