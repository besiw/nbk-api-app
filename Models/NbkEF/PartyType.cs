using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class PartyType
    {
        public PartyType()
        {
            Doc = new HashSet<Doc>();
            DocType = new HashSet<DocType>();
            EmailHistory = new HashSet<EmailHistory>();
            ProjectParty = new HashSet<ProjectParty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<Doc> Doc { get; set; }
        public virtual ICollection<DocType> DocType { get; set; }
        public virtual ICollection<EmailHistory> EmailHistory { get; set; }
        public virtual ICollection<ProjectParty> ProjectParty { get; set; }
    }
}
