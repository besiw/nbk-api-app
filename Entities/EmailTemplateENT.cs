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
    
        public class WrapperEmailTemplate
        {
            public EmailTemplateENT EmailTemplate { get; set; }
        }

        public class WrapperMultiEmailTemplates
        {
            public List<EmailTemplateENT> MultiEmailTemplates { get; set; }
        }
        public class EmailTemplateENT
        {
            public int Id { get; set; }        
            public string Title { get; set; }
            public string Template { get; set; }
        }

    public static class EmailHashTags
    {
        public static List<string> EmailHashTagsList = new List<string>()
        {
            "#CustomerName#",
            "#Description#",
            "#Name#",
            "#PhoneNumber#",
            "#Email#",
            "#Designation#",
            "#ansvarlig#",
            "#Address#",
            "#ProjectTitle#",
            "#CustomerPhone#",
            "#BuildingSupplier#",
            "#InspectorName#"
        };
    }

    public class EmailWorkflow
    {
        public string Title { get; set; }

        public string Content { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
    }

}
