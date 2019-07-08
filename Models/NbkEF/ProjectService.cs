using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectService
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public string Price { get; set; }
        public bool? IsNewAdded { get; set; }

        public virtual Project Project { get; set; }
        public virtual Service Service { get; set; }
    }
}
