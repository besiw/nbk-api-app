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
    public class UserCRUD
    {
        public void UpdateToken(int userID , string Token)
        {
            NbkDbEntities context = new NbkDbEntities();
            DateTime Today = DateTime.Now;
            var db = context.Users.Where(x => x.Id == userID).FirstOrDefault();
            db.Token = Token;
            db.TokenValidTo = Today.AddDays(7);
            db.TokenValidFrom = Today;
            context.SaveChanges();
        }

        public string ValidateToken(int userID, string Token)
        {
            NbkDbEntities context = new NbkDbEntities();
            DateTime Today = DateTime.Now;
            var db = context.Users.Where(x => x.Id == userID).FirstOrDefault();
            if (db != null)
            {
                if (db.Token == Token)
                {
                    if(db.TokenValidTo == null)
                    {
                        db.TokenValidTo = DateTime.Now.AddDays(-2);
                        db.TokenValidFrom = DateTime.Now.AddDays(-1);
                        context.SaveChanges();
                    }
                    if(db.TokenValidTo < Today)
                    {
                        return "TokenExpired";
                    }
                    else
                    {
                        db.TokenValidTo = DateTime.Now.AddDays(7);
                        db.TokenValidFrom = DateTime.Now;
                        context.SaveChanges();
                        return "ExtendedTokenExpiry";
                    }
                    
                }
                else
                {
                    return "IncorrectToken";
                }
            }
            else
            {

                return "IncorrectToken";
            }
        }

        public bool? IsAdminUser(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            bool? isAdmin = dbcontext.Users
                .Join(dbcontext.ContactBook,
                x => x.ContactId,
                y => y.Id,
                (x, y) => new { Y = y, X = x })
                .Where(xAndy => xAndy.X.Id == Id).Select(x => x.Y.IsAdmin).FirstOrDefault();

            return isAdmin;
        }
    }
}
