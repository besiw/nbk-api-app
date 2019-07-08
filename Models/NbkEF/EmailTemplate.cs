using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class EmailTemplate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
    }
}
