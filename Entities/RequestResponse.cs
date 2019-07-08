using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NBKProject.Entities
{
    public class RequestResponse
    {
        public string Message { get; set; }
        //public string Errors { get; set; }
        public bool Success { get; set; }
        [IgnoreDataMember]
        public int UserProfileID { get; set; }
        [IgnoreDataMember]
        public bool isAdminUser { get; set; }
    }
}
