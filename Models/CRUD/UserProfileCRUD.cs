using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NBKProject.Entities;
using NBKProject.Helpers;
using NBKProject.Models.NbkEF;
using NBKProject.Models.CRUD;
using System.ComponentModel.DataAnnotations;

namespace NBKProject.Models.CRUD
{
    public class UserProfileCRUD
    {
        public UserProfileENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Users Obj = dbcontext.Users.Where(x => x.Id == Id).FirstOrDefault();
            UserProfileENT Data = new UserProfileENT()
            {
                Id = Obj.Id,
                Designation = Obj.Designation,
                IsActive = Obj.IsActive,
                Password = Obj.Password,
                Picture = Obj.Picture,
                UserName = Obj.UserName,
                UserTypeId = Obj.UserTypeId,
                ContactId = Obj.ContactId
            };
            return Data;


        }
        public List<UserProfileENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Users> Obj = dbcontext.Users.ToList();
            List<UserProfileENT> Data = new List<UserProfileENT>();
            Data.AddRange(Obj.Select(i => new UserProfileENT
            {
                Id = i.Id,
                Designation = i.Designation,
                IsActive = i.IsActive,
                Password = i.Password,
                Picture = i.Picture,
                UserName = i.UserName,
                UserTypeId = i.UserTypeId,
                ContactId = i.ContactId
            }));

            return Data;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Users Obj = dbcontext.Users.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.Users.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public UserProfileENT UpdateSelectSingle(UserProfileENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Users Data = new Users()
            {
                Id = Obj.Id,
                Designation = Obj.Designation,
                IsActive = Obj.IsActive,
                Password = Obj.Password,
                Picture = Obj.Picture,
                UserName = Obj.UserName,
                UserTypeId = Obj.UserTypeId,
                ContactId = Obj.ContactId
            };


            dbcontext.Users.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Designation).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            update.Property(x => x.Password).IsModified = true;
            update.Property(x => x.Picture).IsModified = true;
            update.Property(x => x.UserName).IsModified = true;
            update.Property(x => x.UserTypeId).IsModified = true;
            update.Property(x => x.ContactId).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public UserProfileENT CreateSingle(UserProfileENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Users Data = new Users()
            {
                Designation = Obj.Designation,
                IsActive = Obj.IsActive,
                Password = Obj.Password,
                Picture = Obj.Picture,
                UserName = Obj.UserName,
                UserTypeId = Obj.UserTypeId,
                ContactId = Obj.ContactId
            };


            dbcontext.Users.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }

        public UserProfileENT SearchUserByEmail(string email)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Project ObjProj = dbcontext.Project.FirstOrDefault();
            ContactBook UserContact = dbcontext.ContactBook.Where(x => x.Email == email).FirstOrDefault();

            Users Obj = dbcontext.Users.Where(x => x.ContactId == UserContact.Id).FirstOrDefault();
            UserProfileENT Data = new UserProfileENT()
            {
                Id = Obj.Id,
                Designation = Obj.Designation,
                IsActive = Obj.IsActive,
                Password = Obj.Password,
                Picture = Obj.Picture,
                UserName = Obj.UserName,
                UserTypeId = Obj.UserTypeId,
                ContactId = Obj.ContactId                
            };
            return Data;
        }

    }
}
