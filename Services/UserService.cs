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
using NBKProject.Models.CRUD;

namespace NBKProject.Services
{
    
        public interface IUserService
        {
            User Authenticate(string username, string password);
            IEnumerable<User> GetAll();
        }

        public class UserService : IUserService
        {

        

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FullName = "wong", UserName = "bessie", Password = "123" }
        };

        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
            {
                _appSettings = appSettings.Value;
            }

            public User Authenticate(string username, string password)
            {
            NbkDbEntities context = new NbkDbEntities();
            var db = context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            User userobj = new User()
            {
                Id = db.Id,
                FullName = db.FullName,
                UserName = db.UserName,
                Password = db.Password
            };
            _users.Add(userobj);

            var user = _users.SingleOrDefault(x => x.UserName == username && x.Password == password); 
                // return null if user not found
                if (user == null)
                    return null;

            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // remove password before returning
                user.Password = null;

            //Updating in DB
            new UserCRUD().UpdateToken(user.Id, user.Token);
            return user;
            }

            public IEnumerable<User> GetAll()
            {
                // return users without passwords
                return _users.Select(x => {
                    x.Password = null;
                    return x;
                });
            }
        }
    
}
