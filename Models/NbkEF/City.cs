using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class City
    {
        public City()
        {
            ContactBook = new HashSet<ContactBook>();
            GeneralSetting = new HashSet<GeneralSetting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }

        public virtual ICollection<ContactBook> ContactBook { get; set; }
        public virtual ICollection<GeneralSetting> GeneralSetting { get; set; }
    }
}
