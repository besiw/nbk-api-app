using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class DocType
    {
        public DocType()
        {
            Doc = new HashSet<Doc>();
        }

        public int Id { get; set; }
        public int? PartyTypeId { get; set; }
        public string DocName { get; set; }
        public bool? IsRequired { get; set; }
        public int? SortOrder { get; set; }

        public virtual PartyType PartyType { get; set; }
        public virtual ICollection<Doc> Doc { get; set; }
    }
}
