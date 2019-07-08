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
    public class BuildingSupplierService
    {
        public WrapperBuildingSupplier GetSingleBuildingSupplier(int id)
        {
            WrapperBuildingSupplier data = new WrapperBuildingSupplier();
            data.BuildingSupplier = new BuildingSupplierCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiBuildingSuppliers GetAllBuildingSupplier()
        {
            WrapperMultiBuildingSuppliers data = new WrapperMultiBuildingSuppliers();
            data.MultiBuildingSuppliers = new BuildingSupplierCRUD().GetAll();
            return data;
        }

        public WrapperBuildingSupplier UpdateSingleBuildingSupplier(BuildingSupplierENT UserProfile)
        {
            WrapperBuildingSupplier data = new WrapperBuildingSupplier();
            data.BuildingSupplier = new BuildingSupplierCRUD().UpdateSelectSingle(UserProfile);
            return data;
        }

        public WrapperBuildingSupplier CreateSingleBuildingSupplier(BuildingSupplierENT UserProfile)
        {
            WrapperBuildingSupplier data = new WrapperBuildingSupplier();
            data.BuildingSupplier = new BuildingSupplierCRUD().CreateSingle(UserProfile);
            return data;
        }

        public RequestResponse DeleteSingleBuildingSupplier(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                new BuildingSupplierCRUD().DeleteSingle(id);
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
