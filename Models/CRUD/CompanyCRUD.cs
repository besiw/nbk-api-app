using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NBKProject.Entities;
using NBKProject.Helpers;
using NBKProject.Models.NbkEF;

namespace NBKProject.Models.CRUD
{
    public class CompanyCRUD
    {
        public CompanyProfileENT SelectAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            GeneralSetting Obj = dbcontext.GeneralSetting.First();
            CompanyProfileENT Data = new CompanyProfileENT()
            {
                Id = Obj.Id,
                address = Obj.Address,
                companyName = Obj.CompanyName,
                //EmailAddress = Obj.EmailAddress,
                nameOnEmailAddress = Obj.EmailSenderName,
                mobile = Obj.Mobile,
                organizationalNumber = Obj.OrganizationalNumber,
                ownerName = Obj.OwnerName,
                postCode = Obj.PostCode,
                senderEmailAddress = Obj.SenderEmailAddress,
                telephone = Obj.Telephone
            };
            return Data;
            

        }

        public CompanyProfileENT Update(CompanyProfileENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();            
            GeneralSetting Data = new GeneralSetting()
            {
                Id = Obj.Id,
                Address = Obj.address,
                CompanyName = Obj.companyName,
                //EmailAddress = Obj.EmailAddress,
                EmailSenderName = Obj.nameOnEmailAddress,
                Mobile = Obj.mobile,
                OrganizationalNumber = Obj.organizationalNumber,
                OwnerName = Obj.ownerName,
                PostCode = Obj.postCode,
                SenderEmailAddress = Obj.senderEmailAddress,
                Telephone = Obj.telephone
            };


            dbcontext.GeneralSetting.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Address).IsModified = true;
            update.Property(x => x.CompanyName).IsModified = true;
            update.Property(x => x.EmailSenderName).IsModified = true;
            update.Property(x => x.OrganizationalNumber).IsModified = true;
            update.Property(x => x.OwnerName).IsModified = true;
            update.Property(x => x.PostCode).IsModified = true;
            update.Property(x => x.SenderEmailAddress).IsModified = true;
            update.Property(x => x.Telephone).IsModified = true;
            update.Property(x => x.Mobile).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }
    }
}
