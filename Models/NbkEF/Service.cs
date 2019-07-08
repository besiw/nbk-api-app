using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class Service
    {
        public Service()
        {
            ProjectService = new HashSet<ProjectService>();
            ServicePerSlab = new HashSet<ServicePerSlab>();
        }

        public int Id { get; set; }
        public int? ServiceTypeId { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public int? ServiceChargedAs { get; set; }
        public int? ChecklistTempId { get; set; }
        public string VismaId { get; set; }
        public string TripletexId { get; set; }

        public virtual ChecklistTemplate ChecklistTemp { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<ProjectService> ProjectService { get; set; }
        public virtual ICollection<ServicePerSlab> ServicePerSlab { get; set; }
    }
}
