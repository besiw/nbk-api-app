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
    public class WrapperProject
    {
        public ProjectENT Project { get; set; }
        
    }

    public class WrapperMultiProject
    {
        public List<ProjectENT> MultiProject { get; set; }
    }
    public class ProjectENT
    {
        public int Id { get; set; }
        public string VismaId { get; set; }
        public string Title { get; set; }
        public DateTime? Dated { get; set; }
        public int? CustomerId { get; set; }
        public int? ContactPersonId { get; set; }
        public int? BuildingSupplierId { get; set; }
        public string GardsNo { get; set; }
        public string Bruksnmmer { get; set; }
        public string Address { get; set; }
        public string PostNo { get; set; }
        public string Poststed { get; set; }
        public string Kommune { get; set; }
        public string Comments { get; set; }
        public int? InspectorId { get; set; }
        public int? ProjectLeaderId { get; set; }
        public DateTime? RemContactCustomerDate { get; set; }
        public int? RemContactCustomerDdl { get; set; }
        public string Description { get; set; }
        public DateTime? CompleteDate { get; set; }
        public bool? IsSubmitted { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string InspectionEventComment { get; set; }
        public DateTime? InspectionDate { get; set; }
        public DateTime? GodkjensDate { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectImage { get; set; }
        public string InspectorComment { get; set; }
        public string InspectorSignature { get; set; }
        public DateTime? TakkBestillingenCdate { get; set; }
        public DateTime? SoknadOmAnsvarsrettCdate { get; set; }
        public DateTime? AnsvarligSokerCdate { get; set; }
        public DateTime? GratulererGodkjentCdate { get; set; }
        public DateTime? CreateChecklistCdate { get; set; }
        public DateTime? AddPartiesCdate { get; set; }
        public DateTime? SetProLeaderContactCustomerCdate { get; set; }
        public DateTime? EmailCustomerUpInspectionCd { get; set; }
        public DateTime? UpcomingInspectionCdate { get; set; }
        public DateTime? PartiesDataCdate { get; set; }
        public DateTime? AssignInspectorCdate { get; set; }
        public DateTime? ProjectSubProcessCdate { get; set; }
        public DateTime? ProjectSubCompleteCd { get; set; }
        public DateTime? ReviewInspReportCd { get; set; }
        public DateTime? InvoiceSetCd { get; set; }
        public DateTime? SubmitInspectionRepRemindCd { get; set; }
        public DateTime? SubmitInspectionRepRemindAgainCd { get; set; }
        public DateTime? KontrollerklaeringPdfCd { get; set; }
        public DateTime? FinalReportPdfCdate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsApprovedInspReport { get; set; }
        public string VismaInvoiceId { get; set; }
        public bool? TakkBestillingenIsCompleted { get; set; }
        public bool? SoknadOmAnsvarsrettIsCompleted { get; set; }
        public bool? AnsvarligSokerIsCompleted { get; set; }
        public bool? GratulererGodkjentIsCompleted { get; set; }
        public bool? CreateChecklistIsCompleted { get; set; }
        public bool? AddPartiesIsCompleted { get; set; }
        public bool? SetProLeaderContactCustomerIsCompleted { get; set; }
        public bool? EmailCustomerUpInspectionIsCompleted { get; set; }
        public bool? PartiesDataIsCompleted { get; set; }
        public bool? AssignInspectorIsCompleted { get; set; }
        public bool? IsApprovedInspReportIsCompleted { get; set; }
        public string InvoiceTripletexId { get; set; }
        public string TepmlateValue { get; set; }
        public string Avvik { get; set; }
        public string AvvikSendtKommune { get; set; }
        public bool? SkipInspection { get; set; }
        public List<ProjectServiceENT> ProjectService { get; set; }
        public List<ServiceWorkflowCategoryENT> ProjectServiceWorkflowList { get; set; }

    }

    public class ProjectServiceENT
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public string Price { get; set; }
        public bool? IsNewAdded { get; set; }
        public  ServiceENT Service { get; set; }


    }



}
