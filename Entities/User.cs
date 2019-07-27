﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBKProject.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool ? IsAdmin { get; set; }
    }
}
