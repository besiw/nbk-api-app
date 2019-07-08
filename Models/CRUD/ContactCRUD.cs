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
    public class ContactCRUD
    {
        public ContactENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ContactBook Obj = dbcontext.ContactBook.Where(x => x.Id == Id).FirstOrDefault();
            ContactENT Data = new ContactENT()
            {
                 Id = Obj.Id,
                 Name = Obj.Name,
                 CompanyName = Obj.CompanyName,
                 ContactNo = Obj.ContactNo,
                 Email = Obj.Email
            };
            return Data;


        }
        public List<ContactENT> GetAll(int PageNo, string SearchByName, string SearchByEmail, string SearchByCompany, string SearchByNumber)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<ContactBook> Obj = new List<ContactBook>();
            if (PageNo == 0)
            {
                if (SearchByName == null && SearchByEmail == null && SearchByCompany == null && SearchByNumber == null)
                {
                    Obj = dbcontext.ContactBook.ToList();
                }
                else if(SearchByName != null)
                {
                    Obj = dbcontext.ContactBook.Where(x=>x.Name == SearchByName).ToList();
                }
                else if (SearchByEmail != null)
                {
                    Obj = dbcontext.ContactBook.Where(x => x.Email == SearchByEmail).ToList();
                }
                else if (SearchByCompany != null)
                {
                    Obj = dbcontext.ContactBook.Where(x => x.CompanyName == SearchByCompany).ToList();
                }
                else if (SearchByNumber != null)
                {
                    Obj = dbcontext.ContactBook.Where(x => x.ContactNo == SearchByNumber).ToList();
                }
            }
            else if(PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;
                    
                    if (SearchByName == null && SearchByEmail == null && SearchByCompany == null && SearchByNumber == null)
                    {
                        Obj = dbcontext.ContactBook.Take(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByName).Take(PageNo).ToList();
                    }
                    else if (SearchByEmail != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByEmail).Take(PageNo).ToList();
                    }
                    else if (SearchByCompany != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByCompany).Take(PageNo).ToList();
                    }
                    else if (SearchByNumber != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByNumber).Take(PageNo).ToList();
                    }
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;
                    
                    if (SearchByName == null && SearchByEmail == null && SearchByCompany == null && SearchByNumber == null)
                    {
                        Obj = dbcontext.ContactBook.Skip(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByName).Skip(PageNo).ToList();
                    }
                    else if (SearchByEmail != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByEmail).Skip(PageNo).ToList();
                    }
                    else if (SearchByCompany != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByCompany).Skip(PageNo).ToList();
                    }
                    else if (SearchByNumber != null)
                    {
                        Obj = dbcontext.ContactBook.Where(x => x.Name == SearchByNumber).Skip(PageNo).ToList();
                    }
                }
            }
            List<ContactENT> Data = new List<ContactENT>();
            Data.AddRange(Obj.Select(i => new ContactENT
            {
                Id = i.Id,
                Name = i.Name,
                CompanyName = i.CompanyName,
                ContactNo = i.ContactNo,
                Email = i.Email
            }));

            return Data;
        }

        public bool CheckIfContactAsocWithProject(int id)
        {
            bool ifExist = false;
            NbkDbEntities dbcontext = new NbkDbEntities();
            Project obj = dbcontext.Project.Where(x => x.ContactPersonId == id).FirstOrDefault();
            if(obj != null)
            {
                ifExist = true;
            }
            return ifExist;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ContactBook Obj = dbcontext.ContactBook.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.ContactBook.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public ContactENT UpdateSelectSingle(ContactENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ContactBook Data = new ContactBook()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                CompanyName = Obj.CompanyName,
                ContactNo = Obj.ContactNo,
                Email = Obj.Email
            };


            dbcontext.ContactBook.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.CompanyName).IsModified = true;
            update.Property(x => x.ContactNo).IsModified = true;
            update.Property(x => x.Email).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public ContactENT CreateSingle(ContactENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            ContactBook Data = new ContactBook()
            {
                Name = Obj.Name,
                CompanyName = Obj.CompanyName,
                ContactNo = Obj.ContactNo,
                Email = Obj.Email
            };


            dbcontext.ContactBook.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
    }
}
