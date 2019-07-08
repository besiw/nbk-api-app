using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class EmailHistory
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PartyId { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public int? ProjectStatus { get; set; }
        public bool? Status { get; set; }
        public DateTime? Date { get; set; }
        public int? PartyTypeId { get; set; }
        public bool? IsEmail { get; set; }

        public virtual ContactBook Party { get; set; }
        public virtual PartyType PartyType { get; set; }
        public virtual Project Project { get; set; }
    }
}
