using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ContactBook
    {
        public ContactBook()
        {
            Doc = new HashSet<Doc>();
            EmailHistory = new HashSet<EmailHistory>();
            ProjectContactPerson = new HashSet<Project>();
            ProjectCustomer = new HashSet<Project>();
            ProjectParty = new HashSet<ProjectParty>();
            ProjectProjectLeader = new HashSet<Project>();
        }

        public int Id { get; set; }
        public int? OldId { get; set; }
        public int? CityId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string VismaId { get; set; }
        public string ContactName { get; set; }
        public int? PartyTypeId { get; set; }
        public string Comment { get; set; }
        public bool? IsCompany { get; set; }
        public string CompName { get; set; }
        public bool? ToBeDeleted { get; set; }
        public string PostNo { get; set; }
        public string Poststed { get; set; }
        public string Kommune { get; set; }
        public string TripletexId { get; set; }
        public string TempEmail { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Doc> Doc { get; set; }
        public virtual ICollection<EmailHistory> EmailHistory { get; set; }
        public virtual ICollection<Project> ProjectContactPerson { get; set; }
        public virtual ICollection<Project> ProjectCustomer { get; set; }
        public virtual ICollection<ProjectParty> ProjectParty { get; set; }
        public virtual ICollection<Project> ProjectProjectLeader { get; set; }
    }
}
