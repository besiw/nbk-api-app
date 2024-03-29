﻿using System;
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

        #region ProjectWorkflowSteps Managment for Dashboard

        public WrapperMultiProjectWorkflow GetProjectWorkflowStep(int ProjectID, int WorkflowID, int WorkflowStepID)
        {
            WrapperMultiProjectWorkflow WrapperMultiProjectWorkflow = new WrapperMultiProjectWorkflow();

            WrapperMultiProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWorkflowStep(ProjectID, WorkflowID, WorkflowStepID);

            return WrapperMultiProjectWorkflow;
        }
        public WrapperMultiProjectWorkflow GetProjectWorkflowCompletedTransferedSteps(int ProjectID, int WorkflowID)
        {
            WrapperMultiProjectWorkflow WrapperMultiProjectWorkflow = new WrapperMultiProjectWorkflow();

            WrapperMultiProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWorkflowCompletedTransferedSteps(ProjectID, WorkflowID);

            return WrapperMultiProjectWorkflow;
        }
        
        #endregion
        #region Workflow # 1 

        #region Step # 1  [Send e-post: Takk for bestillingen Sendt (Send e-mail: Thank you for your order Sent)]
        public WrapperProjectWorkflow GetProjectWFOneEmailFormated(ProjectWorkflowENT Param)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();
            Param.EmailTempId = 1;
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
            Param.EmailTempId = 2;
            WrapperProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWFEmailFormated(Param);
            //Param = new Helpers.PDFCreator().PdfGenerate(Param, _hostingEnvironment);
            Param.AttachmentURL = "https:\\nbk-api-dev.azurewebsites.net";
            return WrapperProjectWorkflow;
        }


        public RequestResponse ProjectWFTwo(ProjectWorkflowENT Param, IFormFile FileInRequest, IHostingEnvironment _hostingEnvironment)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {
                    //Param = new Helpers.PDFCreator().PdfSave(Param, _hostingEnvironment, FileInRequest);
                    //temporary code
                    Param.FileName = "";
                    Param.RootURL = "test";
                    //ends
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

        #region Step # 3 [Opprett Sjekklister for Prosjektet (Create Checklists for the Project)]

        public RequestResponse ProjectWFThree(ProjectWorkflowENT Param, IFormFileCollection FilesInRequest, IHostingEnvironment _hostingEnvironment)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {
                    
                    //This code will be updated after file storage
                    Param.FileName = "";
                    Param.RootURL = "";
                    foreach (var item in FilesInRequest)
                    {
                        Param.FileNames.Add(item.FileName);
                    }
                    //end

                    Param = new ProjectWorkflowCRUD().ProjectWFThreeDone(Param);
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, Document uploaded successfully!";
                    RequestResponse.Success = true;
                }
                else if (Param.IsTransfer == true)
                {
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated.";
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

        #region Step # 4  [Send e-post: Gratulerer med Godkjent byggesøknad. (Send email: Congratulations on Approved building application.)]
        public WrapperProjectWorkflow GetProjectWFFourEmailFormated(ProjectWorkflowENT Param)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();
            Param.EmailTempId = 3;
            WrapperProjectWorkflow = new ProjectWorkflowCRUD().GetProjectWFEmailFormated(Param);

            return WrapperProjectWorkflow;
        }


        public RequestResponse ProjectWFFour(ProjectWorkflowENT Param)
        {
            RequestResponse RequestResponse = new RequestResponse();
            try
            {
                if (Param.IsTransfer == false)
                {

                    Param = new ProjectWorkflowCRUD().ProjectWFFourDone(Param);
                    Param = new ProjectWorkflowCRUD().WorkflowProjectStepStatusAdd(Param);
                    RequestResponse.Message = "Project status updated, email Sent successfully!";
                    RequestResponse.Success = true;
                }
                else if (Param.IsTransfer == true)
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

        #endregion
    }
}
