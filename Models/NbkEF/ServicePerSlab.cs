using System;
using System.Collections.Generic;

namespace NBKProject.Models.NbkEF
{
    public partial class ServicePerSlab
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? RangeFrom { get; set; }
        public int? RangeTo { get; set; }
        public string Rate { get; set; }

        public virtual Service Service { get; set; }
    }
}
