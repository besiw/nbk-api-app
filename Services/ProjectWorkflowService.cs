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
using Microsoft.AspNetCore.Hosting;

namespace NBKProject.Services
{
    public class ProjectWorkflowService
    {
        #region Workflow # 1 

        #region Step # 1  [Send e-post: Takk for bestillingen Sendt (Send e-mail: Thank you for your order Sent)]
        public WrapperProjectWorkflow GetProjectWFOneEmailFormated(ProjectWorkflowENT Param)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();            
            WrapperProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWFEmailFormated(Param);

            return WrapperProjectWorkflow;
        }
        

        public RequestResponse ProjectWFOne(ProjectWorkflowENT Param)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {

                    Param = new ProjectWorkflowCRUD().ProjectWFOneDone(Param);
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, email Sent successfully!";
                    RequestResponse.Success = true;
                }
                else if(Param.IsTransfer == true)
                {
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, Transfer successfully!";
                    RequestResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }
        #endregion

        #region Step # 2 [Opprett og send: Erklæring om ansvarsrett (Create and send: Statement of Liability)]

        public WrapperProjectWorkflow GetProjectWFTwoEmailFormatedWithPDF(ProjectWorkflowENT Param , IHostingEnvironment _hostingEnvironment)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();
            WrapperProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWFEmailFormated(Param);
            Param = new Helpers.PDFCreator().PdfGenerate(Param, _hostingEnvironment);
            return WrapperProjectWorkflow;
        }


        public RequestResponse ProjectWFTwo(ProjectWorkflowENT Param, IFormFile FileInRequest, IHostingEnvironment _hostingEnvironment)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {
                    Param = new Helpers.PDFCreator().PdfSave(Param, _hostingEnvironment, FileInRequest); 
                    Param = new ProjectWorkflowCRUD().ProjectWFTwoDone(Param);
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, email Sent successfully!";
                    RequestResponse.Success = true;
                }
                else if (Param.IsTransfer == true)
                {
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, email Sent successfully!";
                    RequestResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                RequestResponse.Message = ex.Message;
                RequestResponse.Success = false;
            }

            return RequestResponse;
        }

        #endregion

        #endregion
    }
}
