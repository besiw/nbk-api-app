using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectChecklist
    {
        public ProjectChecklist()
        {
            ChecklistItems = new HashSet<ChecklistItems>();
        }

        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? SortOrder { get; set; }
        public string ChecklistName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ChecklistItems> ChecklistItems { get; set; }
    }
}
