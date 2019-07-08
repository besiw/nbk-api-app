using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NBKProject.Services;
using NBKProject.Entities;
using NBKProject.Models.NbkEF;
using NBKProject.Models.CRUD;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NBKProject.Entities
{
    public class WrapperPartyType
    {
        public PartyTypeENT PartyType { get; set; }
    }

    public class WrapperMultiPartyTypes
    {
        public List<PartyTypeENT> MultiPartyTypes { get; set; }
    }
    public class PartyTypeENT
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
