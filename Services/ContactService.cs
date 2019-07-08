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
    public class ContactService
    {
        public WrapperContact GetSingleContact(int id)
        {
            WrapperContact data = new WrapperContact();
            data.Contact = new ContactCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiContact GetAllContact(int PageNo, string SearchByName, string SearchByEmail, string SearchByCompany, string SearchByNumber)
        {
            WrapperMultiContact data = new WrapperMultiContact();
            data.MultiContact = new ContactCRUD().GetAll(PageNo, SearchByName, SearchByEmail, SearchByCompany, SearchByNumber);
            return data;
        }

        public WrapperContact UpdateSingleContact(ContactENT Obj)
        {
            WrapperContact data = new WrapperContact();
            data.Contact = new ContactCRUD().UpdateSelectSingle(Obj);
            return data;
        }

        public WrapperContact CreateSingleContact(ContactENT Obj)
        {
            WrapperContact data = new WrapperContact();
            data.Contact = new ContactCRUD().CreateSingle(Obj);
            return data;
        }

        public RequestResponse DeleteSingleContact(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                //First check if its used in project already
                bool ifExist = new ContactCRUD().CheckIfContactAsocWithProject(id);
                if(ifExist == true)
                {
                    RequestResponse.Message = "Not deleted, Record is used in Project.";
                    RequestResponse.Success = false;
                    return RequestResponse;
                }

                new ContactCRUD().DeleteSingle(id);
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
