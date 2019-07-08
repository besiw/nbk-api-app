using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ChecklistTemplate
    {
        public ChecklistTemplate()
        {
            ChecklistItemTemplate = new HashSet<ChecklistItemTemplate>();
            Service = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<ChecklistItemTemplate> ChecklistItemTemplate { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}
