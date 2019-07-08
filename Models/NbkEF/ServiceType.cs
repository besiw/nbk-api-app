using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            Service = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<Service> Service { get; set; }
    }
}
