using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ProjectLeaderTemplate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public int? SortOrder { get; set; }
    }
}
