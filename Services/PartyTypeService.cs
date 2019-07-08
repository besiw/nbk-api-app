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
    public class PartyTypeService
    {
        public WrapperPartyType GetSinglePartyType(int id)
        {
            WrapperPartyType data = new WrapperPartyType();
            data.PartyType = new PartyTypeCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiPartyTypes GetAllPartyType()
        {
            WrapperMultiPartyTypes data = new WrapperMultiPartyTypes();
            data.MultiPartyTypes = new PartyTypeCRUD().GetAll();
            return data;
        }

        public WrapperPartyType UpdateSinglePartyType(PartyTypeENT UserProfile)
        {
            WrapperPartyType data = new WrapperPartyType();
            data.PartyType = new PartyTypeCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperPartyType CreateSinglePartyType(PartyTypeENT UserProfile)
        {
            WrapperPartyType data = new WrapperPartyType();
            data.PartyType = new PartyTypeCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSinglePartyType(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new PartyTypeCRUD().DeleteSingle(id);
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
