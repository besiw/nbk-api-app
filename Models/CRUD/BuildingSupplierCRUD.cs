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

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            BuildingSupplierTemplate Obj = dbcontext.BuildingSupplierTemplate.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.BuildingSupplierTemplate.Remove(Obj);
            dbcontext.SaveChanges();
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
