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
    public class ProjectWorkflowService
    {
        public WrapperProjectWorkflow GetProjectWFOneEmailFormated(ProjectWorkflowENT Param)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();
            
          //new ProjectWorkflowCRUD().GetProjectWFOneEmailFormated(Param);
                 
            return WrapperProjectWorkflow;
        }
        

        public RequestResponse ProjectWFOne(ProjectWorkflowENT Param)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {

                    //new ProjectWorkflowCRUD().ProjectWFOneDone(Param);
                    RequestResponse.Message = "Project status updated";
                    RequestResponse.Success = true;
                }
                else if(Param.IsTransfer == true)
                {

                }
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
