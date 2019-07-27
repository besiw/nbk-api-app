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
    public class WrapperBuildingSupplier
    {
        public BuildingSupplierENT BuildingSupplier { get; set; }
    }

    public class WrapperMultiBuildingSuppliers
    {
        public List<BuildingSupplierENT> MultiBuildingSuppliers { get; set; }
    }
    public class BuildingSupplierENT
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Title { get; set; }
    }
    public class ProjectAsociatedWithBuildingSup
    {
        public int  Id { get; set; }
        public string Title { get; set; }
    }
    public class ResponseBuildingSupplier
    {
        public RequestResponse RequestResponse { get; set; }
        public List<ProjectAsociatedWithBuildingSup> ProjectAsociatedWithBuildingSupplier { get; set; }
    }
}
