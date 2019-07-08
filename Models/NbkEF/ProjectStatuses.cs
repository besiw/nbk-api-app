using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectStatuses
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
    }
}
