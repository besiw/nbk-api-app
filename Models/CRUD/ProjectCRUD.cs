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
        public List<ProjectENT> GetAllProjectList(int PageNo)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            if (PageNo == 0)
            {
                Obj = dbcontext.Project.ToList();
            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Take(PageNo).ToList();
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Skip(PageNo).ToList();
                }
            }
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

        public List<ProjectENT> GetAllProjectListNotArchivedOrDeleted(int PageNo)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            if (PageNo == 0)
            {
                Obj = dbcontext.Project.Where(x=>x.IsDeleted == null && x.IsArchived == null).ToList();
            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsDeleted == null && x.IsArchived == null).Take(PageNo).ToList();
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsDeleted == null && x.IsArchived == null).Skip(PageNo).ToList();
                }
            }
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

        public List<ProjectENT> GetAllArchivedProjectList(int PageNo)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            if (PageNo == 0)
            {
                Obj = dbcontext.Project.Where(x => x.IsArchived == true).ToList();
            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsArchived == true).Take(PageNo).ToList();
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsArchived == true).Skip(PageNo).ToList();
                }
            }
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

        public List<ProjectENT> GetAllDeletedProjectList(int PageNo)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Project> Obj = new List<Project>();
            if (PageNo == 0)
            {
                Obj = dbcontext.Project.Where(x => x.IsDeleted == true).ToList();
            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsDeleted == true).Take(PageNo).ToList();
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;
                    Obj = dbcontext.Project.Where(x => x.IsDeleted == true).Skip(PageNo).ToList();
                }
            }
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
                Title = Obj.Title
            };
            return Data;


        }
        


        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Project Obj = dbcontext.Project.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.Project.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public ProjectENT UpdateSelectSingle(ProjectENT Obj)
        {


            NbkDbEntities dbcontext = new NbkDbEntities();
            Project Data = new Project()
            {
                Id = Obj.Id,
                Title = Obj.Title
            };


            dbcontext.Project.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Title).IsModified = true;
            dbcontext.SaveChanges();

           
            return Obj;
        }


        public ProjectENT CreateSingle(ProjectENT Obj)
        {

            NbkDbEntities dbcontext = new NbkDbEntities();
            Project Data = new Project()
            {
                Title = Obj.Title
            };
            dbcontext.Project.Add(Data);
            dbcontext.SaveChanges();
            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
