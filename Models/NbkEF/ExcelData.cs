using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ExcelData
    {
        public int Id { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Kommunenavn { get; set; }
    }
}
