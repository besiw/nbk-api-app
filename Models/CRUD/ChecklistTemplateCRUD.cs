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
    public class ChecklistTemplateCRUD
    {
        public ChecklistTemplateENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ChecklistTemplate Obj = dbcontext.ChecklistTemplate.Where(x => x.Id == Id).FirstOrDefault();
            List<ChecklistItemTemplate> ChecklistItemTemplateList = dbcontext.ChecklistItemTemplate.Where(x => x.ChecklistId == Obj.Id).ToList();
            if(ChecklistItemTemplateList == null)
            {
                ChecklistItemTemplateList.Add(new ChecklistItemTemplate()); 
            }
            Service CheckListAttchedWithService = dbcontext.Service.Where(x => x.ChecklistTempId == Obj.Id).FirstOrDefault();
            if(CheckListAttchedWithService == null)
            {
                CheckListAttchedWithService = new Service();
            }
            ChecklistTemplateENT Data = new ChecklistTemplateENT()
            {
                Id = Obj.Id,
                Title = Obj.Title,
                IsDefault = Obj.IsDefault,
                ChecklistItemTemplateList = new List<ChecklistItemTemplateENT>
                (
                    ChecklistItemTemplateList.Select(j => new ChecklistItemTemplateENT
                    {
                        Id = j.Id,
                        ChecklistId = j.ChecklistId,
                        Title = j.Title
                    })
                    ),
                CheckListAttchedWithService = new ServiceItemTemplateENT()
                {
                    Id = CheckListAttchedWithService.Id,
                    ChecklistTempId = CheckListAttchedWithService.ChecklistTempId,
                    Description = CheckListAttchedWithService.Description,
                    Name = CheckListAttchedWithService.Name,
                    Rate = CheckListAttchedWithService.Rate,
                    ServiceChargedAs = CheckListAttchedWithService.ServiceChargedAs,
                    ServiceTypeId = CheckListAttchedWithService.ServiceTypeId
                }
                
            };
            return Data;


        }
        public List<ChecklistTemplateENT> GetAll(int PageNo, string SearchByName)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<ChecklistTemplate> Obj = new List<ChecklistTemplate>();
            if (PageNo == 0)
            {
                if (SearchByName == null)
                {
                    Obj = dbcontext.ChecklistTemplate.ToList();
                }
                else if (SearchByName != null)
                {
                    Obj = dbcontext.ChecklistTemplate.Where(x => x.Title == SearchByName).ToList();
                }
                

            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;

                    if (SearchByName == null)
                    {
                        Obj = dbcontext.ChecklistTemplate.Take(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.ChecklistTemplate.Where(x => x.Title == SearchByName).Take(PageNo).ToList();
                    }
                    
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;

                    if (SearchByName == null )
                    {
                        Obj = dbcontext.ChecklistTemplate.Skip(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.ChecklistTemplate.Where(x => x.Title == SearchByName).Skip(PageNo).ToList();
                    }
                    

                }
            }
            List<ChecklistTemplateENT> Data = new List<ChecklistTemplateENT>();
            Data.AddRange(Obj.Select(i => new ChecklistTemplateENT
            {
                Id = i.Id,
                Title = i.Title,
                IsDefault = i.IsDefault,
                ChecklistItemTemplateList = new List<ChecklistItemTemplateENT>
                (
                    dbcontext.ChecklistItemTemplate.Where(x => x.ChecklistId == i.Id).Select(j => new ChecklistItemTemplateENT
                    {                        
                        Id = j.Id,
                        ChecklistId = j.ChecklistId,
                        Title = j.Title
                    })
                    ),
                CheckListAttchedWithService = dbcontext.Service.Where(x => x.ChecklistTempId == i.Id).Select(j => new ServiceItemTemplateENT
                {
                    Id = j.Id,
                    ChecklistTempId = j.ChecklistTempId,
                    Description = j.Description,
                    Name = j.Name,
                    Rate = j.Rate,
                    ServiceChargedAs = j.ServiceChargedAs,
                    ServiceTypeId = j.ServiceTypeId
                }).FirstOrDefault()
                
            }));

            

            return Data;
        }

        

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ChecklistTemplate Obj = dbcontext.ChecklistTemplate.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.ChecklistTemplate.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public ChecklistTemplateENT UpdateSelectSingle(ChecklistTemplateENT Obj)
        {
            
           
            NbkDbEntities dbcontext = new NbkDbEntities();
            ChecklistTemplate Data = new ChecklistTemplate()
            {
                Id = Obj.Id,
                Title = Obj.Title,
                IsDefault = Obj.IsDefault
            };


            dbcontext.ChecklistTemplate.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Title).IsModified = true;
            update.Property(x => x.IsDefault).IsModified = true;
            dbcontext.SaveChanges();

            if (Obj.ServiceSelectedID > 0)
            {
                var ServiceDetail = dbcontext.Service.Where(x => x.Id == Obj.ServiceSelectedID).FirstOrDefault();
                ServiceDetail.ChecklistTempId = Obj.Id;
                dbcontext.SaveChanges();
                var PreviousService = dbcontext.Service.Where(x => x.ChecklistTempId == Obj.Id && x.Id != Obj.ServiceSelectedID).FirstOrDefault();
                if (PreviousService != null)
                {
                    PreviousService.ChecklistTempId = null;
                    dbcontext.SaveChanges();
                }
            }


            return Obj;
        }


        public ChecklistTemplateENT CreateSingle(ChecklistTemplateENT Obj)
        {
           
            NbkDbEntities dbcontext = new NbkDbEntities();
            ChecklistTemplate Data = new ChecklistTemplate()
            {
                Title = Obj.Title                
            };
            dbcontext.ChecklistTemplate.Add(Data);
            dbcontext.SaveChanges();
            Obj.Id = Data.Id;

            return Obj;
        }

        
    }
}
