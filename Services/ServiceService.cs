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
    public class ServiceService
    {
        public WrapperService GetSingleService(int id)
        {
            WrapperService data = new WrapperService();
            data.Service = new ServiceCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiService GetAllService(int PageNo, string SearchByName, string SearchByDescription)
        {
            WrapperMultiService data = new WrapperMultiService();
            data.MultiService = new ServiceCRUD().GetAll(PageNo, SearchByName, SearchByDescription);
            return data;
        }

        public WrapperService UpdateSingleService(ServiceENT Obj)
        {
            WrapperService data = new WrapperService();
            data.Service = new ServiceCRUD().UpdateSelectSingle(Obj);
            return data;
        }

        public WrapperService CreateSingleService(ServiceENT Obj)
        {
            WrapperService data = new WrapperService();
            data.Service = new ServiceCRUD().CreateSingle(Obj);
            return data;
        }

        public RequestResponse DeleteSingleService(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                //First check if its used in project already
                bool ifExist = new ServiceCRUD().CheckIfServiceAsocWithProject(id);
                if (ifExist == true)
                {
                    RequestResponse.Message = "Not deleted, Record is used in Project.";
                    RequestResponse.Success = false;
                    return RequestResponse;
                }

                new ServiceCRUD().DeleteSingle(id);
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
