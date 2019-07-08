using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class PostNumber
    {
        public PostNumber()
        {
            GeneralSetting = new HashSet<GeneralSetting>();
        }

        public int Postnummer { get; set; }
        public string Poststed { get; set; }
        public int? Kommunenummer { get; set; }
        public string Kommunenavn { get; set; }
        public string Kategori { get; set; }

        public virtual ICollection<GeneralSetting> GeneralSetting { get; set; }
    }
}
