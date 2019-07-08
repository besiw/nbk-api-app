using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class Users
    {
        public Users()
        {
            Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? UserTypeId { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public bool? IsActive { get; set; }
        public string Picture { get; set; }
        public string Token { get; set; }
        public DateTime? TokenValidFrom { get; set; }
        public DateTime? TokenValidTo { get; set; }
        public int? ContactId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}
