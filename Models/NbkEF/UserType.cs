using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
