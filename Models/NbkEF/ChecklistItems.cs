using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ChecklistItems
    {
        public ChecklistItems()
        {
            ChecklistItemImage = new HashSet<ChecklistItemImage>();
        }

        public int Id { get; set; }
        public int? ChecklistId { get; set; }
        public string Title { get; set; }
        public int? SortOrder { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public DateTime? FixDate { get; set; }
        public bool? WasDev { get; set; }
        public DateTime? EmailPartyDate { get; set; }
        public DateTime? PartyUploadedImgDate { get; set; }
        public string EmailTempToPartiesIds { get; set; }
        public bool? IsImageUploadedByParty { get; set; }

        public virtual ProjectChecklist Checklist { get; set; }
        public virtual ICollection<ChecklistItemImage> ChecklistItemImage { get; set; }
    }
}
