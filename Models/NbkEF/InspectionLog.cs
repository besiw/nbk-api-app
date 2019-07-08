using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class InspectionLog
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
