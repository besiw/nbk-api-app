using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectParty
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PartyId { get; set; }
        public int? PartyTypeId { get; set; }

        public virtual ContactBook Party { get; set; }
        public virtual PartyType PartyType { get; set; }
        public virtual Project Project { get; set; }
    }
}
