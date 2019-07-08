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
   
    public class WrapperService
    {
        public ServiceENT Service { get; set; }
    }

    public class WrapperMultiService
    {
        public List<ServiceENT> MultiService { get; set; }
    }
    public class ServiceENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public int? ServiceTypeId { get; set; }

        [Required(ErrorMessage = "Pricing strategy is required")]
        public int? ServiceChargedAs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Rate { get; set; }

        public List<ServicePerSlab> ServicePerSlabList { get; set; }

    }
}
