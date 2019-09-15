using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class WorkflowCategorySteps
    {
        public int Id { get; set; }
        public int WorkflowCategoryId { get; set; }
        public string StepName { get; set; }
        public int StepSequence { get; set; }
        public bool IsActive { get; set; }
        public bool IsTransferable { get; set; }
    }
}
