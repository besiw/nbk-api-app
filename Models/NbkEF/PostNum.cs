using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class PostNum
    {
        public int Id { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Kommunenummer { get; set; }
        public string Kommunenavn { get; set; }
        public string Kategori { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
    }
}
