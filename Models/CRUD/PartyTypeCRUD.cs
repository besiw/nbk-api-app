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
    public class PartyTypeCRUD
    {
        public PartyTypeENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            PartyType Obj = dbcontext.PartyType.Where(x => x.Id == Id).FirstOrDefault();
            PartyTypeENT Data = new PartyTypeENT()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                IsDefault = Obj.IsDefault
            };
            return Data;


        }
        public List<PartyTypeENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<PartyType> Obj = dbcontext.PartyType.ToList();
            List<PartyTypeENT> Data = new List<PartyTypeENT>();
            Data.AddRange(Obj.Select(i => new PartyTypeENT
            {
                Id = i.Id,
                Name = i.Name,
                IsDefault = i.IsDefault
            }));

            return Data;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            PartyType Obj = dbcontext.PartyType.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.PartyType.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public PartyTypeENT UpdateSelectSingle(PartyTypeENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            PartyType Data = new PartyType()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                IsDefault = Obj.IsDefault
            };


            dbcontext.PartyType.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.IsDefault).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public PartyTypeENT CreateSingle(PartyTypeENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            PartyType Data = new PartyType()
            {
                Name = Obj.Name,
                IsDefault = Obj.IsDefault
            };


            dbcontext.PartyType.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
