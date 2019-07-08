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
    public class ProjectUDService
    {
        public WrapperProject GetSingleProject(int id)
        {
            WrapperProject data = new WrapperProject();
            data.Project = new ProjectCRUD().SelectSingle(id);
            return data;
        }

        public WrapperMultiProject GetAllProjectList(int PageNo)
        {
            WrapperMultiProject data = new WrapperMultiProject();
            data.MultiProject = new ProjectCRUD().GetAllProjectList(PageNo);
            return data;
        }

        public WrapperMultiProject GetAllProjectListNotArchivedOrDeleted(int PageNo)
        {
            WrapperMultiProject data = new WrapperMultiProject();
            data.MultiProject = new ProjectCRUD().GetAllProjectListNotArchivedOrDeleted(PageNo);
            return data;
        }

        public WrapperMultiProject GetAllArchivedProjectList(int PageNo)
        {
            WrapperMultiProject data = new WrapperMultiProject();
            data.MultiProject = new ProjectCRUD().GetAllArchivedProjectList(PageNo);
            return data;
        }

        public WrapperMultiProject GetAllDeletedProjectList(int PageNo)
        {
            WrapperMultiProject data = new WrapperMultiProject();
            data.MultiProject = new ProjectCRUD().GetAllDeletedProjectList(PageNo);
            return data;
        }

        public WrapperProject UpdateSingleProject(ProjectENT Obj)
        {
            WrapperProject data = new WrapperProject();
            data.Project = new ProjectCRUD().UpdateSelectSingle(Obj);
            return data;
        }

        public WrapperProject CreateSingleProject(ProjectENT Obj)
        {
            WrapperProject data = new WrapperProject();
            data.Project = new ProjectCRUD().CreateSingle(Obj);
            return data;
        }

        public RequestResponse DeleteSingleProject(int id)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {


                new ProjectCRUD().DeleteSingle(id);
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
