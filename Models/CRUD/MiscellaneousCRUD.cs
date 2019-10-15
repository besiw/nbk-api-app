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
using System.ComponentModel.DataAnnotations;

namespace NBKProject.Models.CRUD
{
    public class MiscellaneousCRUD
    {
        public List<PostCodeENT> GetAllPostCode()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<PostNum> Obj = dbcontext.PostNum.ToList();
            List<PostCodeENT> Data = new List<PostCodeENT>();
            Data.AddRange(Obj.Select(i=> new PostCodeENT
            {
                Id = i.Id,
                Kategori = i.Kategori,
                Poststed = i.Poststed,
                Postnummer = i.Postnummer,
                Kommunenavn =i.Kommunenavn,
                Kommunenummer = i.Kommunenummer
            }));
            
            return Data;


        }

        public PostCodeENT GetSinglePostCodeByPostNumber(string PostNumber)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            PostNum Obj = dbcontext.PostNum.Where(x=>x.Postnummer == PostNumber).FirstOrDefault();
            PostCodeENT Data = new PostCodeENT()
            {
                Id = Obj.Id,
                Kategori = Obj.Kategori,
                Poststed = Obj.Poststed,
                Postnummer = Obj.Postnummer,
                Kommunenavn = Obj.Kommunenavn,
                Kommunenummer = Obj.Kommunenummer
            };

            return Data;
        }
    }
}
