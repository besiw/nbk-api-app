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
    public class DocTypeCRUD
    {
        public DocTypeENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            DocType Obj = dbcontext.DocType.Where(x => x.Id == Id).FirstOrDefault();
            DocTypeENT Data = new DocTypeENT()
            {
                Id = Obj.Id,
                PartyTypeId = Obj.PartyTypeId,
                DocName = Obj.DocName,
                isRequired = Obj.IsRequired
            };
            return Data;


        }
        public List<DocTypeENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<DocType> Obj = dbcontext.DocType.ToList();
            List<DocTypeENT> Data = new List<DocTypeENT>();
            Data.AddRange(Obj.Select(i => new DocTypeENT
            {
                Id = i.Id,
                PartyTypeId = i.PartyTypeId,
                DocName = i.DocName,
                isRequired = i.IsRequired
            }));

            return Data;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            DocType Obj = dbcontext.DocType.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.DocType.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public DocTypeENT UpdateSelectSingle(DocTypeENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            DocType Data = new DocType()
            {
                Id = Obj.Id,
                PartyTypeId = Obj.PartyTypeId,
                DocName = Obj.DocName,
                IsRequired = Obj.isRequired
            };


            dbcontext.DocType.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.DocName).IsModified = true;
            update.Property(x => x.IsRequired).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public DocTypeENT CreateSingle(DocTypeENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            DocType Data = new DocType()
            {
                PartyTypeId = Obj.PartyTypeId,
                DocName = Obj.DocName,
                IsRequired = Obj.isRequired
            };


            dbcontext.DocType.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
