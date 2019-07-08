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
    public class EmailTemplateCRUD
    {
        public EmailTemplateENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            EmailTemplate Obj = dbcontext.EmailTemplate.Where(x => x.Id == Id).FirstOrDefault();
            EmailTemplateENT Data = new EmailTemplateENT()
            {
                Id = Obj.Id,
                Title = Obj.Title,
                Template = Obj.Template
            };
            return Data;


        }
        public List<EmailTemplateENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<EmailTemplate> Obj = dbcontext.EmailTemplate.ToList();
            List<EmailTemplateENT> Data = new List<EmailTemplateENT>();
            Data.AddRange(Obj.Select(i => new EmailTemplateENT
            {
                Id = i.Id,
                Title = i.Title,
                Template = i.Template
            }));

            return Data;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            EmailTemplate Obj = dbcontext.EmailTemplate.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.EmailTemplate.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public EmailTemplateENT UpdateSelectSingle(EmailTemplateENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            EmailTemplate Data = new EmailTemplate()
            {
                Id = Obj.Id,
                Title = Obj.Title,
                Template = Obj.Template
            };
            dbcontext.EmailTemplate.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Title).IsModified = true;
            update.Property(x => x.Template).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public EmailTemplateENT CreateSingle(EmailTemplateENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            EmailTemplate Data = new EmailTemplate()
            {
                Title = Obj.Title,
                Template = Obj.Template
            };
            dbcontext.EmailTemplate.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
