using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class Doc
    {
        public int Id { get; set; }
        public int? PartyId { get; set; }
        public int? PartyTypeId { get; set; }
        public int? PartyDocTypeId { get; set; }
        public int? ProjectId { get; set; }
        public int? OtherDocs { get; set; }
        public string FileName { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsApproved { get; set; }

        public virtual ContactBook Party { get; set; }
        public virtual DocType PartyDocType { get; set; }
        public virtual PartyType PartyType { get; set; }
        public virtual Project Project { get; set; }
    }
}
