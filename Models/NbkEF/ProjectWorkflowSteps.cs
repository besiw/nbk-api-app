using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectWorkflowSteps
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int WorkflowId { get; set; }
        public int WorkflowStepId { get; set; }
        public bool IsTransfer { get; set; }
        public int? TaskId { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? InsertedBy { get; set; }
    }
}
