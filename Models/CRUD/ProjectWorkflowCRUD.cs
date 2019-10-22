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

namespace NBKProject.Models.CRUD
{
    public class ProjectWorkflowCRUD
    {
        #region ProjectWorkflowSteps Managment for Dashboard

        public WrapperMultiProjectWorkflow GetProjectWorkflowStep(int ProjectID, int WorkflowID, int WorkflowStepID)
        {
            WrapperMultiProjectWorkflow WrapperMultiProjectWorkflow = new WrapperMultiProjectWorkflow();
            NbkDbEntities dbcontext = new NbkDbEntities();

            List<ProjectWorkflowSteps> Obj = dbcontext.ProjectWorkflowSteps.Where(x => x.ProjectId == ProjectID && x.WorkflowId == WorkflowID &&
            x.WorkflowStepId == WorkflowStepID).ToList();
            List<ProjectWorkflowENT> MultiProjectWorkflow = new List<ProjectWorkflowENT>();

            if (Obj != null)
            {
                if (Obj.Count > 0)
                {
                    MultiProjectWorkflow.AddRange(Obj.Select(i => new ProjectWorkflowENT
                    {
                        Id = i.Id,
                        ProjectId = i.ProjectId,
                        WorkflowId = i.WorkflowId,
                        WorkflowStepId = i.WorkflowStepId,
                        IsTransfer = i.IsTransfer,
                        EmailHistoryId = i.TaskId,
                        InsertDate = i.InsertDate,
                        InsertedBy = i.InsertedBy
                    }));
                }                
            }
            WrapperMultiProjectWorkflow.MultiProjectWorkflow = MultiProjectWorkflow;
            return WrapperMultiProjectWorkflow;
        }

        public WrapperMultiProjectWorkflow GetProjectWorkflowCompletedTransferedSteps(int ProjectID, int WorkflowID)
        {
            WrapperMultiProjectWorkflow WrapperMultiProjectWorkflow = new WrapperMultiProjectWorkflow();
            NbkDbEntities dbcontext = new NbkDbEntities();

            List<ProjectWorkflowSteps> Obj = dbcontext.ProjectWorkflowSteps.Where(x => x.ProjectId == ProjectID && 
            x.WorkflowId == WorkflowID).OrderBy(x=>x.WorkflowStepId).ToList();
            List<ProjectWorkflowENT> MultiProjectWorkflow = new List<ProjectWorkflowENT>();

            if (Obj != null)
            {
                if (Obj.Count > 0)
                {
                    MultiProjectWorkflow.AddRange(Obj.Select(i => new ProjectWorkflowENT
                    {
                        Id = i.Id,
                        ProjectId = i.ProjectId,
                        WorkflowId = i.WorkflowId,
                        WorkflowStepId = i.WorkflowStepId,
                        IsTransfer = i.IsTransfer,
                        EmailHistoryId = i.TaskId,
                        InsertDate = i.InsertDate,
                        InsertedBy = i.InsertedBy
                    }));
                }
            }
            WrapperMultiProjectWorkflow.MultiProjectWorkflow = MultiProjectWorkflow;
            return WrapperMultiProjectWorkflow;
        }

        
        #endregion


        #region Workflow # 1

        public WrapperProjectWorkflow GetProjectWFEmailFormated(ProjectWorkflowENT Param)
        {
            WrapperProjectWorkflow WrapperProjectWorkflow = new WrapperProjectWorkflow();
            EmailWorkflow Content = new ProjectWorkflowCRUD().EmailContent(Param);
            Param.EmailContent = Content.Content;
            Param.EmailSubject = Content.Title;
            Param.EmailTo = Content.EmailTo;
            Param.EmailFrom = Content.EmailFrom;
            WrapperProjectWorkflow.ProjectWorkflow = Param;
            return WrapperProjectWorkflow;
        }
        public EmailWorkflow EmailContent(ProjectWorkflowENT Param)
        {
            
            NbkDbEntities dbcontext = new NbkDbEntities();
            Users user = dbcontext.Users.Where(x => x.Id == Param.InsertedBy).FirstOrDefault();

            dbcontext = new NbkDbEntities();
            var emailDetails = new EmailWorkflow();
            EmailTemplateENT template = new EmailTemplateCRUD().SelectSingle(Param.EmailTempId);
            ProjectENT projectDetail = new ProjectCRUD().SelectSingle(Param.ProjectId);
            ContactENT customer = new ContactCRUD().SelectSingle(Convert.ToInt32(projectDetail.CustomerId));
            ContactENT contactPerson = new ContactCRUD().SelectSingle(Convert.ToInt32(projectDetail.ContactPersonId));
            BuildingSupplierENT buildingSupplier = new BuildingSupplierCRUD().SelectSingle(Convert.ToInt32(projectDetail.BuildingSupplierId));
            CompanyProfileENT companyProfile = new CompanyCRUD().SelectAll();

            emailDetails.EmailFrom = companyProfile.senderEmailAddress;
            emailDetails.EmailTo = customer.Email;
            if (Param.EmailTempId == 1)
            {
                
                int price = 0;
                float priceWithGst = 0f;
                var ProjectServices = new ProjectCRUD().ListOfProjectServices(Param.ProjectId);
                if (ProjectServices != null)
                {
                    foreach (var item in ProjectServices)
                    {
                        price = price + Convert.ToInt32(item.Price);
                    }
                }

                //template.Template = template.Template.Replace("#ansvarlig#", projectDetail.Contact.Name);
                template.Template = template.Template.Replace("#priceWithoutGst#", price.ToString());
                if (price != 0)
                {
                    float remainder = price * 0.25f;
                    priceWithGst = price + remainder;
                }
                template.Template = template.Template.Replace("#priceWithGst#", priceWithGst.ToString());
            }
            dbcontext = new NbkDbEntities();
            string InspectorName = "";
            if (Param.EmailTempId == 7 || Param.EmailTempId == 8)
            {
                if (projectDetail.InspectorId != null)
                {
                    InspectorName = dbcontext.Users.Where(x => x.Id == projectDetail.InspectorId).Select(x => x.FullName).FirstOrDefault();
                }
                template.Template = template.Template.Replace("#InspectorName#", InspectorName);
            }
            if (projectDetail.CustomerId != null)
            {
                
                template.Template = template.Template.Replace("#anvarligSokerCompany#", contactPerson.CompanyName);
                template.Template = template.Template.Replace("#ansvarlig#", contactPerson.Name);
            }
            else
            {
                template.Template = template.Template.Replace("#anvarligSokerCompany#", "");
                template.Template = template.Template.Replace("#ansvarlig#", "");
            }
            template.Template = template.Template.Replace("#PhoneNumber#", user.ContactNo);
            template.Template = template.Template.Replace("#Email#", user.Email);
            template.Template = template.Template.Replace("#Name#", user.FullName);
            template.Template = template.Template.Replace("#Designation#", user.Designation);
            template.Template = template.Template.Replace("#Address#", projectDetail.Address);
            template.Template = template.Template.Replace("#Description#", projectDetail.Description);
            template.Template = template.Template.Replace("#ProjectTitle#", projectDetail.Title);
            template.Template = template.Template.Replace("#CustomerName#", customer.Name);
            template.Template = template.Template.Replace("#CustomerPhone#", customer.ContactNo);
            template.Template = template.Template.Replace("#BuildingSupplier#", buildingSupplier.Title);

            emailDetails.Content = template.Template;
            emailDetails.Title = EmailSubjectReplacements(template, projectDetail, customer, contactPerson, buildingSupplier,user);
            return emailDetails;

           

        }        
        public string EmailSubjectReplacements(EmailTemplateENT template, ProjectENT projectDetail, ContactENT customer, ContactENT contactPerson, BuildingSupplierENT buildingSupplier, Users user)
        {
            string content = "";
            
            template.Title = template.Title.Replace("#CustomerName#", customer.Name);
            template.Title = template.Title.Replace("#Description#", projectDetail.Description);
            template.Title = template.Title.Replace("#Name#", user.FullName);
            template.Title = template.Title.Replace("#PhoneNumber#", user.ContactNo);
            template.Title = template.Title.Replace("#Email#", user.Email);
            template.Title = template.Title.Replace("#Designation#", user.Designation);
            if (contactPerson != null)
            {
                template.Title = template.Title.Replace("#ansvarlig#", contactPerson.Name);
            }
            else
            {
                template.Title = template.Title.Replace("#ansvarlig#", "");
            }
            template.Title = template.Title.Replace("#Address#", projectDetail.Address);
            template.Title = template.Title.Replace("#ProjectTitle#", projectDetail.Title);
            template.Title = template.Title.Replace("#CustomerPhone#", customer.ContactNo);
            template.Title = template.Title.Replace("#BuildingSupplier#", buildingSupplier.Title);
            content = template.Title;
            
            return content;
        }
        public ProjectWorkflowENT ProjectWFOneDone(ProjectWorkflowENT Param)
        {

            string emailsend = new Helpers.Emailing().EmailHostDetail(Param.EmailTo, Param.EmailFrom, Param.EmailSubject, Param.EmailContent, Param.FileName, Param.RootURL);
            Param.InsertDate = DateTime.Now;
            Param =  InsertRecordInEmailHistory(Param);
            return Param;
        }
        public ProjectWorkflowENT InsertRecordInEmailHistory(ProjectWorkflowENT Param)
        {
            if(Param.FileName == null)
            {
                Param.FileName = "";
            }
            NbkDbEntities dbcontext = new NbkDbEntities();
            EmailHistory Data = new EmailHistory
            {
                ProjectId = Param.ProjectId,
                WorkflowId = Param.WorkflowId,
                WorkflowStepId = Param.WorkflowStepId,
                //PartyId = 
                Subject = Param.EmailSubject,
                ToEmail = Param.EmailTo,
                FromEmail = Param.EmailFrom,
                Message = Param.EmailContent,
                FileName = Param.FileName.ToString(),
                Date = DateTime.Now
                //PartyTypeId =
                //IsEmail =

            };

            dbcontext.EmailHistory.Add(Data);
            dbcontext.SaveChanges();
            Param.EmailHistoryId = Data.Id;
            return Param;
        }
        public ProjectWorkflowENT WorkflowProjectStepStatusAdd(ProjectWorkflowENT Param)
        {

            NbkDbEntities dbcontext = new NbkDbEntities();
            
            ProjectWorkflowSteps Data = new ProjectWorkflowSteps()
            {
                ProjectId = Param.ProjectId,
                WorkflowId = Param.WorkflowId,
                WorkflowStepId = Param.WorkflowStepId,
                IsTransfer = Param.IsTransfer,
                TaskId = Param.EmailHistoryId,
                InsertDate = Param.InsertDate,
                InsertedBy = Param.InsertedBy
            };


            //dbcontext.ProjectWorkflowSteps.Attach(Data);
            //var update = dbcontext.Entry(Data);
            //update.Property(x => x.ProjectId).IsModified = true;
            //update.Property(x => x.WorkflowId).IsModified = true;
            //update.Property(x => x.WorkflowStepId).IsModified = true;
            //update.Property(x => x.IsTransfer).IsModified = true;
            //update.Property(x => x.TaskId).IsModified = true;
            //update.Property(x => x.InsertDate).IsModified = true;
            //update.Property(x => x.InsertedBy).IsModified = true;
            dbcontext.ProjectWorkflowSteps.Add(Data);
            dbcontext.SaveChanges();
            Param.Id = Data.Id;

            return Param;
        }


        public ProjectWorkflowENT ProjectWFTwoDone(ProjectWorkflowENT Param)
        {

            string emailsend = new Helpers.Emailing().EmailHostDetail(Param.EmailTo, Param.EmailFrom, Param.EmailSubject, Param.EmailContent, Param.FileName, Param.RootURL);
            Param.InsertDate = DateTime.Now;
            Param = InsertRecordInEmailHistory(Param);
            return Param;
        }

        public ProjectWorkflowENT ProjectWFThreeDone(ProjectWorkflowENT Param)
        {

            Param.InsertDate = DateTime.Now;
            foreach (var item in Param.FileNames)
            {
                Param.FileName = item;
                Param = InsertRecordInDoc(Param, 3, null, null);
            }
            return Param;
        }

        public ProjectWorkflowENT InsertRecordInDoc(ProjectWorkflowENT Param, int ? otherDoc, int ? PartyTypeId, int ? PartyDocTypeID)
        {
            if (Param.FileName == null)
            {
                Param.FileName = "";
            }
            NbkDbEntities dbcontext = new NbkDbEntities();
            Doc Data = new Doc
            {
                ProjectId = Param.ProjectId,
                WorkflowId = Param.WorkflowId,
                WorkflowStepId = Param.WorkflowStepId,
                Date = Param.InsertDate,
                PartyId =  null,
                OtherDocs = otherDoc,
                FileName = Param.FileName.ToString(),
                PartyTypeId = PartyTypeId,
                PartyDocTypeId = PartyDocTypeID
            };

            dbcontext.Doc.Add(Data);
            dbcontext.SaveChanges();
            Param.EmailHistoryId = Data.Id;
            return Param;
        }


        public ProjectWorkflowENT ProjectWFFourDone(ProjectWorkflowENT Param)
        {

            string emailsend = new Helpers.Emailing().EmailHostDetail(Param.EmailTo, Param.EmailFrom, Param.EmailSubject, Param.EmailContent, Param.FileName, Param.RootURL);
            Param.InsertDate = DateTime.Now;
            Param = InsertRecordInEmailHistory(Param);
            return Param;
        }
        #endregion
    }
}
