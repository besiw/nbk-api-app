using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class BuildingSupplierTemplate
    {
        public BuildingSupplierTemplate()
        {
            Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
