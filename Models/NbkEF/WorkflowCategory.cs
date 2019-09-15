using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class WorkflowCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
    }
}
