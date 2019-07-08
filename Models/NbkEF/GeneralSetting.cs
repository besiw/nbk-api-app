using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class GeneralSetting
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string OrganizationalNumber { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public int? PostCode { get; set; }
        public int? CityId { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string EmailSenderName { get; set; }
        public string SenderEmailAddress { get; set; }

        public virtual City City { get; set; }
        public virtual PostNumber PostCodeNavigation { get; set; }
    }
}
