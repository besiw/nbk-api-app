using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ChecklistItemTemplate
    {
        public int Id { get; set; }
        public int? ChecklistId { get; set; }
        public string Title { get; set; }
        public int? SortOrder { get; set; }

        public virtual ChecklistTemplate Checklist { get; set; }
    }
}
