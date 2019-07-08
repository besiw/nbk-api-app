using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ChecklistItemImage
    {
        public int Id { get; set; }
        public int? ChecklistItemId { get; set; }
        public string ImageName { get; set; }
        public DateTime? CaptureDate { get; set; }
        public string ImageSize { get; set; }
        public string ImageType { get; set; }
        public int? PartyId { get; set; }
        public bool? IsOkForFinalPdf { get; set; }

        public virtual ChecklistItems ChecklistItem { get; set; }
    }
}
