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
    public class ProjectCRUD
    {

        #region Project List APIs
        public List<ProjectENT> GetAllProjectList(int EntriesFrom, int EntriesTill)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            EntriesFrom = EntriesFrom - 1;
            int EntriesRequired = EntriesTill - EntriesFrom;            
            Obj = dbcontext.Project.OrderByDescending(x => x.Id).Skip(EntriesFrom).Take(EntriesRequired).ToList();
            List<ProjectENT> Data = new List<ProjectENT>();
            Data.AddRange(Obj.Select(i => new ProjectENT
            {
                Id = i.Id,
                Title = i.Title,
                Dated = i.Dated,
                CustomerId = i.CustomerId,
                ContactPersonId = i.ContactPersonId,
                BuildingSupplierId = i.BuildingSupplierId,
                Description = i.Description,
                IsDeleted = i.IsDeleted,
                IsArchived = i.IsArchived
            }));
            return Data;
        }

        public List<ProjectENT> GetAllProjectListNotArchivedOrDeleted(int EntriesFrom, int EntriesTill)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            EntriesFrom = EntriesFrom - 1;
            int EntriesRequired = EntriesTill - EntriesFrom;
            Obj = dbcontext.Project.Where(x => x.IsDeleted == null && x.IsArchived == null).OrderByDescending(x => x.Id).Skip(EntriesFrom).Take(EntriesRequired).ToList();
            
            
            List<ProjectENT> Data = new List<ProjectENT>();
            Data.AddRange(Obj.Select(i => new ProjectENT
            {
                Id = i.Id,
                Title = i.Title,
                Dated = i.Dated,
                CustomerId = i.CustomerId,
                ContactPersonId = i.ContactPersonId,
                BuildingSupplierId = i.BuildingSupplierId,
                Description = i.Description,
                IsDeleted = i.IsDeleted,
                IsArchived = i.IsArchived
            }));
            return Data;
        }

        public List<ProjectENT> GetAllArchivedProjectList(int EntriesFrom, int EntriesTill)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            EntriesFrom = EntriesFrom - 1;
            int EntriesRequired = EntriesTill - EntriesFrom;
            Obj = dbcontext.Project.Where(x => x.IsArchived == true).OrderByDescending(x => x.Id).Skip(EntriesFrom).Take(EntriesRequired).ToList();
                        
            List<ProjectENT> Data = new List<ProjectENT>();
            Data.AddRange(Obj.Select(i => new ProjectENT
            {
                Id = i.Id,
                Title = i.Title,
                Dated = i.Dated,
                CustomerId = i.CustomerId,
                ContactPersonId = i.ContactPersonId,
                BuildingSupplierId = i.BuildingSupplierId,
                Description = i.Description,
                IsDeleted = i.IsDeleted,
                IsArchived = i.IsArchived
            }));
            return Data;
        }
        
        public List<ProjectENT> GetAllDeletedProjectList(int EntriesFrom, int EntriesTill)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            EntriesFrom = EntriesFrom - 1;
            int EntriesRequired = EntriesTill - EntriesFrom;
            Obj = dbcontext.Project.Where(x => x.IsDeleted == true).OrderByDescending(x => x.Id).Skip(EntriesFrom).Take(EntriesRequired).ToList();
                                 
            List<ProjectENT> Data = new List<ProjectENT>();
            Data.AddRange(Obj.Select(i => new ProjectENT
            {
                Id = i.Id,
                Title = i.Title,
                Dated = i.Dated,
                CustomerId = i.CustomerId,
                ContactPersonId = i.ContactPersonId,
                BuildingSupplierId = i.BuildingSupplierId,
                Description = i.Description,
                IsDeleted = i.IsDeleted,
                IsArchived = i.IsArchived
            }));
            return Data;
        }

        #endregion
        public ProjectENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Project Obj = dbcontext.Project.Where(x => x.Id == Id).FirstOrDefault();
            
            ProjectENT Data = new ProjectENT()
            {
                Id = Obj.Id,
                Title = Obj.Title,
                Address = Obj.Address,
                Dated = Obj.Dated,
                CustomerId = Obj.CustomerId,
                ContactPersonId = Obj.ContactPersonId,
                BuildingSupplierId = Obj.BuildingSupplierId,
                GardsNo = Obj.GardsNo,
                Bruksnmmer = Obj.Bruksnmmer,
                PostNo = Obj.PostNo,
                Poststed = Obj.Poststed,
                Kommune = Obj.Kommune,
                Description = Obj.Description,
            };
            return Data;


        }
        


        public void DeleteSingle(int ProjectID, bool isDelete)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            var Data = dbcontext.Project.Where(x => x.Id == ProjectID).FirstOrDefault();
            Data.IsArchived = isDelete;
            dbcontext.Project.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.IsDeleted).IsModified = true;
            dbcontext.SaveChanges();
            //NbkDbEntities dbcontext = new NbkDbEntities();
            //Project Obj = dbcontext.Project.Where(x => x.Id == Id).FirstOrDefault();
            //dbcontext.Project.Remove(Obj);
            //dbcontext.SaveChanges();
        }

        public ProjectENT UpdateSelectSingle(ProjectENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();

                 
            Project Data = new Project()
            {
                Id = Obj.Id,
                Title = Obj.Id + " - " + Obj.Address + " - " + Obj.GardsNo + "/" + Obj.Bruksnmmer,
                Address = Obj.Address,
                CustomerId = Obj.CustomerId,
                ContactPersonId = Obj.ContactPersonId,
                BuildingSupplierId = Obj.BuildingSupplierId,
                GardsNo = Obj.GardsNo,
                Bruksnmmer = Obj.Bruksnmmer,
                PostNo = Obj.PostNo,
                Poststed = Obj.Poststed,
                Kommune = Obj.Kommune,
                Description = Obj.Description
            };

            dbcontext.Project.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Title).IsModified = true;
            update.Property(x => x.CustomerId).IsModified = true;
            update.Property(x => x.ContactPersonId).IsModified = true;
            update.Property(x => x.BuildingSupplierId).IsModified = true;
            update.Property(x => x.GardsNo).IsModified = true;
            update.Property(x => x.PostNo).IsModified = true;
            update.Property(x => x.Poststed).IsModified = true;
            update.Property(x => x.Kommune).IsModified = true;
            update.Property(x => x.Bruksnmmer).IsModified = true;
            update.Property(x => x.Description).IsModified = true;
            update.Property(x => x.Address).IsModified = true;
            dbcontext.SaveChanges();


            //Services add
            Obj.ProjectService = InsertProjectServicesList(Obj.ProjectService, Obj.Id);

            //Adding Default partytype into projectparty table
            List<ServiceWorkflowCategory> DataServiceWorkflowCatagory = GetServiceWorkflowCategoryByServiceID(Obj.ProjectService);


            List<PartyType> DefaultPartyTypes = DefaultPartyTypesList(DataServiceWorkflowCatagory);
            ContactBook Dummy = GetDummyContact();
            foreach (var item in DefaultPartyTypes)
            {
                AddProjectParty(Obj.Id, Dummy.Id, item.Id);
            }

            //Project checklist creation 
            ProjectChecklistsOnUpdateCreate(Obj.Id, Obj.ProjectService);

            return Obj;
        }

        public void ProjectChecklistsOnUpdateCreate(int? ProjectId, List<ProjectServiceENT> ServicesList)
        {
            NbkDbEntities db = new NbkDbEntities();
            List<int> ProjectServiceIDs = ServicesList.Select(X => X.Id).ToList();
            //var list = (from r in db.ProjectChecklist where r.ProjectId == ProjectId select r).ToList();
            if (ProjectId != null)
            {
                var listServices = ListOfProjectServicesForUpdate(Convert.ToInt32(ProjectId), ProjectServiceIDs);
                foreach (var item in listServices)
                {
                    ChecklistTemplate ObjChecklistTemp = ChecklistTempByServiceID(item.ServiceId);
                    Service ObjService = ServiceByServiceID(item.ServiceId);
                    int totalServices = Convert.ToInt32(item.Quantity);
                    //int a = i + 1;
                    if (totalServices > 0)
                    {
                        for (int i = 0; i < totalServices; i++)
                        {
                            //Checklist for ServiceNameHere
                            ProjectChecklist obj = new ProjectChecklist();

                            obj.ProjectId = item.ProjectId;
                            if (item.Service.ChecklistTempId != null)
                            {

                                obj.ChecklistName = ObjChecklistTemp.Title;
                            }
                            else
                            {
                                obj.ChecklistName = "Checklist for " + ObjService.Name;
                            }
                            db.ProjectChecklist.Add(obj);
                            db.SaveChanges();
                            if (ObjService.ChecklistTempId != null)
                            {
                                int ProjectChecklistID = obj.Id;
                                int ChecklistTempId = Convert.ToInt32(ObjService.ChecklistTempId);
                                SaveChecklistTemp(ChecklistTempId, ProjectChecklistID);
                            }
                        }
                    }
                    else
                    {
                        ProjectChecklist obj = new ProjectChecklist();
                        obj.ProjectId = item.ProjectId;
                        obj.SortOrder = item.Id;
                        obj.ChecklistName = item.Service.Name + " Checklist";
                        db.ProjectChecklist.Add(obj);
                        db.SaveChanges();
                    }
                    //Service status change
                    var ProService = db.ProjectService.Where(x => x.Id == item.Id).FirstOrDefault();
                    ProService.IsNewAdded = false;
                    db.SaveChanges();
                }
            }

        }


        public ProjectENT CreateSingle(ProjectENT Obj)
        {

            NbkDbEntities dbcontext = new NbkDbEntities();

            //changes for adding id manually
            if (Obj.Id == 0)
            {
               
                int maxId = Convert.ToInt32(ProjectsMaxId());
                Obj.Id = maxId + 1;                
            }

            Project Data = new Project()
            {
                Id = Obj.Id,
                Title = Obj.Id + " - " + Obj.Address + " - " + Obj.GardsNo + "/" + Obj.Bruksnmmer,
                Address = Obj.Address,               
                Dated= DateTime.Today,
                CustomerId= Obj.CustomerId,
                ContactPersonId= Obj.ContactPersonId,
                BuildingSupplierId= Obj.BuildingSupplierId,
                GardsNo= Obj.GardsNo,
                Bruksnmmer= Obj.Bruksnmmer,
                PostNo= Obj.PostNo,
                Poststed= Obj.Poststed,
                Kommune= Obj.Kommune,
                Description= Obj.Description
                //CreateChecklistCdate= Obj.CreateChecklistCdate,
                //AddPartiesCdate= Obj.AddPartiesCdate
            };
            dbcontext.Project.Add(Data);
            dbcontext.SaveChanges();

            //Services add
            InsertProjectServicesList(Obj.ProjectService, Obj.Id);

            //Adding Default partytype into projectparty table
            List<ServiceWorkflowCategory> DataServiceWorkflowCatagory = GetServiceWorkflowCategoryByServiceID(Obj.ProjectService);
            Obj.ProjectServiceWorkflowList = GetProjectServiceWorkflowList(Obj.Id);

            List<PartyType> DefaultPartyTypes = DefaultPartyTypesList(DataServiceWorkflowCatagory);
            ContactBook Dummy = GetDummyContact();
            foreach (var item in DefaultPartyTypes)
            {
                AddProjectParty(Obj.Id, Dummy.Id, item.Id);
            }

            //Project checklist creation 
            ProjectChecklistsCreate(Obj.Id);

            
            Obj.Title = Obj.Id + " - " + Obj.Address + " - " + Obj.GardsNo + "/" + Obj.Bruksnmmer;
             return Obj;
        }

        public List<ServiceWorkflowCategoryENT> GetProjectServiceWorkflowList(int ProjectID)
        {
            List<ServiceWorkflowCategoryENT> ServiceWorkflowCategoryList = new List<ServiceWorkflowCategoryENT>();

            NbkDbEntities db = new NbkDbEntities();
            List<ProjectService> ProjectServiceList =  ListOfProjectServices(ProjectID);

            List<ServiceWorkflowCategory> ServiceWorkflowCategory = db.ServiceWorkflowCategory.Where(x => ProjectServiceList.Select(y => y.ServiceId).Contains(x.ServiceId)).ToList();
            if (ServiceWorkflowCategory != null)
            {
                if (ServiceWorkflowCategory.Count > 0)
                {
                    ServiceWorkflowCategoryList.AddRange(ServiceWorkflowCategory.Select(x => new ServiceWorkflowCategoryENT
                    { Id = x.Id, WorkflowCategoryId = x.WorkflowCategoryId, ServiceId = x.ServiceId }).ToList());
                }
            }
            return ServiceWorkflowCategoryList;
        }


        public void ProjectChecklistsCreate(int? ProjectId)
        {

            NbkDbEntities db = new NbkDbEntities();
            //var list = (from r in db.ProjectChecklist where r.ProjectId == ProjectId select r).ToList();

            if (ProjectId != null)
            {
                var listServices = ListOfProjectServices(Convert.ToInt32(ProjectId));
                foreach (var item in listServices)
                {
                    ChecklistTemplate ObjChecklistTemp = ChecklistTempByServiceID(item.ServiceId);
                    Service ObjService = ServiceByServiceID(item.ServiceId);
                    int totalServices = Convert.ToInt32(item.Quantity);
                    //int a = i + 1;
                    if (totalServices > 0)
                    {
                        for (int i = 0; i < totalServices; i++)
                        {
                            //Checklist for ServiceNameHere
                            ProjectChecklist obj = new ProjectChecklist();
                            
                            obj.ProjectId = item.ProjectId;                            
                            if (ObjChecklistTemp != null && !string.IsNullOrEmpty(ObjChecklistTemp.Title))
                            {
                                
                                obj.ChecklistName = ObjChecklistTemp.Title;
                            }
                            else
                            {
                                obj.ChecklistName = "Checklist for " + ObjService.Name;
                            }
                            db.ProjectChecklist.Add(obj);
                            db.SaveChanges();
                            if (ObjService.ChecklistTempId != null)
                            {
                                if (ObjService.ChecklistTempId > 0)
                                {
                                    int ProjectChecklistID = obj.Id;
                                    if (ObjChecklistTemp != null)
                                    {
                                        int ChecklistTempId = Convert.ToInt32(ObjService.ChecklistTempId);
                                        SaveChecklistTemp(ChecklistTempId, ProjectChecklistID);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ProjectChecklist obj = new ProjectChecklist();
                        obj.ProjectId = item.ProjectId;
                        obj.SortOrder = item.Id;
                        obj.ChecklistName = item.Service.Name + " Checklist";
                        db.ProjectChecklist.Add(obj);
                        db.SaveChanges();
                    }
                    //Service status change
                    var ProService = db.ProjectService.Where(x => x.Id == item.Id).FirstOrDefault();
                    ProService.IsNewAdded = false;
                    db.SaveChanges();
                }
            }
            //if (list.Count != 0)
            //{
            //    //Service added new into project
            //    var listServices = new ProjectServicesModel().ListOfProjectServicesAddedNew(Convert.ToInt32(id));
            //    foreach (var item in listServices)
            //    {
            //        int totalServices = Convert.ToInt32(item.Quantity);
            //        //int a = i + 1;
            //        if (totalServices > 0)
            //        {
            //            for (int i = 0; i < totalServices; i++)
            //            {
            //                //Checklist for ServiceNameHere
            //                ProjectChecklist obj = new ProjectChecklist();
            //                obj.ProjectId = item.ProjectID;
            //                obj.SortOrder = item.ID;
            //                if (item.Service.ChecklistTempID != null)
            //                {
            //                    obj.ChecklistName = item.Service.ChecklistTemplate.Title;
            //                }
            //                else
            //                {
            //                    obj.ChecklistName = "Checklist for " + item.Service.Name;
            //                }
            //                db.ProjectChecklists.Add(obj);
            //                db.SaveChanges();
            //                if (item.Service.ChecklistTempID != null)
            //                {
            //                    int cheklistIdP = obj.Id;
            //                    int ChecklistTempId = Convert.ToInt32(item.Service.ChecklistTempID);
            //                    new NBKProject.Controllers.ChecklistController().SaveChecklistTemp(ChecklistTempId, cheklistIdP);
            //                }
            //            }
            //        }
            //        else
            //        {

            //            ProjectChecklist obj = new ProjectChecklist();
            //            obj.ProjectId = item.ProjectID;
            //            obj.SortOrder = item.ID;
            //            obj.ChecklistName = item.Service.Name + " Checklist";
            //           // db.ProjectChecklists.Add(obj);
            //           // db.SaveChanges();
            //        }
            //        //Service status change
            //       // var ProService = db.ProjectServices.Where(x => x.ID == item.ID).FirstOrDefault();
            //       // ProService.IsNewAdded = false;
            //       // db.SaveChanges();
            //    }
            //    //End service added new code
            //    //list = (from r in db.ProjectChecklists where r.ProjectId == id select r).ToList();
            //}
        }


        public void SaveChecklistTemp(int CheckListID, int ProjectChecklistID)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();

            List<ChecklistItemTemplate> listChecklistItems = (from r in dbcontext.ChecklistItemTemplate where r.ChecklistId == CheckListID select r).ToList();
            foreach (var item in listChecklistItems)
            {
                ChecklistItems chk = new ChecklistItems();
                chk.ChecklistId = ProjectChecklistID;
                chk.Title = item.Title;
                chk.SortOrder = item.SortOrder;
                dbcontext.ChecklistItems.Add(chk);
                dbcontext.SaveChanges();
            }
        }
        public ChecklistTemplate ChecklistTempByServiceID(int ? serviceID)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Service ObjService = dbcontext.Service.Where(x => x.Id == serviceID).FirstOrDefault();
            if (ObjService.ChecklistTempId != null)
            {
                return dbcontext.ChecklistTemplate.Where(x => x.Id == ObjService.ChecklistTempId).FirstOrDefault();
            }
            else {
                ChecklistTemplate obj = new ChecklistTemplate();
                return obj;
            }
        }

        public Service ServiceByServiceID(int? serviceID)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();

            return dbcontext.Service.Where(x => x.Id == serviceID).FirstOrDefault();
        }
        public List<ProjectService> ListOfProjectServices(int proId)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();   
            return dbcontext.ProjectService.Where(x => x.ProjectId == proId).ToList();
        }

        public List<ProjectService> ListOfProjectServicesByWorkflowID(int WorkflowID, int ProjectID)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<int> ServiceIDListAssociatedWithWorkflow = dbcontext.ServiceWorkflowCategory.Where(x => x.WorkflowCategoryId == WorkflowID).Select(x=>x.ServiceId).ToList();
            dbcontext = new NbkDbEntities();
            List<ProjectService> Data = dbcontext.ProjectService.Where(x =>x.ProjectId == ProjectID && ServiceIDListAssociatedWithWorkflow.Contains(Convert.ToInt32(x.ServiceId))).ToList();
            return Data;
        }
        public List<ProjectService> ListOfProjectServicesForUpdate(int proId, List<int> ProjectServiceIDs)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            return dbcontext.ProjectService.Where(x => x.ProjectId == proId && ProjectServiceIDs.Contains(x.Id)).ToList();
        }
        public void AddProjectParty(int projId, int PartyId, int PartyTypeId)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ProjectParty projParty = new ProjectParty();
            projParty.ProjectId = projId;
            if (PartyId == 0)
            {
                projParty.PartyId = null;
            }
            else
            {
                projParty.PartyId = PartyId;
            }
            projParty.PartyTypeId = PartyTypeId;
            dbcontext.ProjectParty.Add(projParty);
            dbcontext.SaveChanges();

        }

        public ContactBook GetDummyContact()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            return dbcontext.ContactBook.Where(x => x.Name == "Dummy Contact").FirstOrDefault();
        }

        public List<ServiceWorkflowCategory> GetServiceWorkflowCategoryByServiceID(List<ProjectServiceENT> ServicesList)
        {
            List<ServiceWorkflowCategory> Data = new List<ServiceWorkflowCategory>();
            
            if (ServicesList.Count != 0)
            {
                List<int?> ServiceIDs = ServicesList.Where(x=>x.IsNewAdded == true).Select(x => x.ServiceId).ToList();
                NbkDbEntities dbcontext = new NbkDbEntities();
                if (ServiceIDs != null)
                {
                    Data = dbcontext.ServiceWorkflowCategory.Where(x => ServiceIDs.Contains(x.ServiceId)).ToList();
                }
            }
            return Data;
        }

        public List<PartyType> DefaultPartyTypesList(List<ServiceWorkflowCategory> DataServiceWorkflowCatagory)
        {
            List<int> WorkflowCatagoryIDs = DataServiceWorkflowCatagory.Select(x => x.WorkflowCategoryId).ToList();
            NbkDbEntities dbcontext = new NbkDbEntities();
            return dbcontext.PartyType.Where(x => x.IsDefault == true && WorkflowCatagoryIDs.Contains(x.WorkflowCategoryId.GetValueOrDefault())).OrderByDescending(y => y.WorkflowCategoryId).ToList();
        }

        public int ProjectsMaxId()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            return dbcontext.Project.Max(x => x.Id);

        }

        public List<ProjectServiceENT> InsertProjectServicesList(List<ProjectServiceENT> ServicesList, int projectID)
        {
            if (ServicesList.Count != 0)
            {
                foreach (var item in ServicesList)
                {
                    if (item.IsNewAdded == true)
                    {
                        NbkDbEntities dbcontext = new NbkDbEntities();
                        ProjectService Data = new ProjectService()
                        {
                            ProjectId = projectID,
                            ServiceId = item.ServiceId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            IsNewAdded = false
                        };
                        dbcontext.ProjectService.Add(Data);
                        dbcontext.SaveChanges();
                        item.Id = Data.Id;
                    }
                }
            }
            return ServicesList;
        }

        public void UpdateProjectTitle(Project Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            dbcontext.Project.Attach(Obj);
            var update = dbcontext.Entry(Obj);
            update.Property(x => x.Title).IsModified = true;
            dbcontext.SaveChanges();
        }

        public void UpdateProjectArchiveStatus(int ProjectID, bool isArchive)
        {

            NbkDbEntities dbcontext = new NbkDbEntities();
            var Data = dbcontext.Project.Where(x => x.Id == ProjectID).FirstOrDefault();
            Data.IsArchived = isArchive;
            dbcontext.Project.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.IsArchived).IsModified = true;
            dbcontext.SaveChanges();
        }
           
    }
}
