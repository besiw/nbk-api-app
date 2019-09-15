using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ServiceWorkflowCategory
    {
        public int Id { get; set; }
        public int WorkflowCategoryId { get; set; }
        public int ServiceId { get; set; }
    }
}
