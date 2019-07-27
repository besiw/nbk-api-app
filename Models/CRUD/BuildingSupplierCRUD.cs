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
    public class BuildingSupplierCRUD
    {
        public BuildingSupplierENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            BuildingSupplierTemplate Obj = dbcontext.BuildingSupplierTemplate.Where(x => x.Id == Id).FirstOrDefault();
            BuildingSupplierENT Data = new BuildingSupplierENT()
            {
                Id = Obj.Id,
                Title = Obj.Title
            };
            return Data;


        }
        public List<BuildingSupplierENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<BuildingSupplierTemplate> Obj = dbcontext.BuildingSupplierTemplate.ToList();
            List<BuildingSupplierENT> Data = new List<BuildingSupplierENT>();
            Data.AddRange(Obj.Select(i => new BuildingSupplierENT
            {
                Id = i.Id,
                Title = i.Title
            }));

            return Data;
        }

        public List<ProjectAsociatedWithBuildingSup> DeleteSingle(int Id)
        {
            List<ProjectAsociatedWithBuildingSup> Projects = new List<ProjectAsociatedWithBuildingSup>();
            NbkDbEntities dbcontext = new NbkDbEntities();
            #region Check if it is used in any project already
            Projects.AddRange(dbcontext.Project.Where(x => x.BuildingSupplierId == Id).Select(x => new ProjectAsociatedWithBuildingSup { Id = x.Id, Title = x.Title }).ToList());
            if(Projects != null)
            {
                if(Projects.Count > 0)
                {
                    return Projects;
                }
            }            
            #endregion

            dbcontext = new NbkDbEntities();
            BuildingSupplierTemplate Obj = dbcontext.BuildingSupplierTemplate.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.BuildingSupplierTemplate.Remove(Obj);
            dbcontext.SaveChanges();
            return Projects;
        }

        public BuildingSupplierENT UpdateSelectSingle(BuildingSupplierENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            BuildingSupplierTemplate Data = new BuildingSupplierTemplate()
            {
                Id = Obj.Id,
                Title = Obj.Title
            };


            dbcontext.BuildingSupplierTemplate.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Title).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public BuildingSupplierENT CreateSingle(BuildingSupplierENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            BuildingSupplierTemplate Data = new BuildingSupplierTemplate()
            {
                Title = Obj.Title
            };


            dbcontext.BuildingSupplierTemplate.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
