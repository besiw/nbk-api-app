using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NBKProject.Models.NbkEF
{
    public partial class NbkDbEntities : DbContext
    {
        public NbkDbEntities()
        {
        }

        public NbkDbEntities(DbContextOptions<NbkDbEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<BuildingSupplierTemplate> BuildingSupplierTemplate { get; set; }
        public virtual DbSet<ChecklistItemImage> ChecklistItemImage { get; set; }
        public virtual DbSet<ChecklistItemTemplate> ChecklistItemTemplate { get; set; }
        public virtual DbSet<ChecklistItems> ChecklistItems { get; set; }
        public virtual DbSet<ChecklistTemplate> ChecklistTemplate { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ContactBook> ContactBook { get; set; }
        public virtual DbSet<Doc> Doc { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<EmailHistory> EmailHistory { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
        public virtual DbSet<ExcelData> ExcelData { get; set; }
        public virtual DbSet<GeneralSetting> GeneralSetting { get; set; }
        public virtual DbSet<InspectionLog> InspectionLog { get; set; }
        public virtual DbSet<PartyType> PartyType { get; set; }
        public virtual DbSet<PostNum> PostNum { get; set; }
        public virtual DbSet<PostNumber> PostNumber { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectChecklist> ProjectChecklist { get; set; }
        public virtual DbSet<ProjectLeaderTemplate> ProjectLeaderTemplate { get; set; }
        public virtual DbSet<ProjectParty> ProjectParty { get; set; }
        public virtual DbSet<ProjectService> ProjectService { get; set; }
        public virtual DbSet<ProjectStatuses> ProjectStatuses { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServicePerSlab> ServicePerSlab { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=(local);Database=nbk_Db;Persist Security Info=True;User ID=sa;Password=123;");
                //Test DB below
                // optionsBuilder.UseSqlServer("Data Source=182.50.133.110;Database=nbk_Db1;Persist Security Info=True;User ID=nbkUser;Password=nbk@@007;");
                //azure db
                //optionsBuilder.UseSqlServer("Data Source=nbk.database.windows.net,1433;Database=nbk_Db;Persist Security Info=True;User ID=admin@nbk@nbk;Password=so19#GreenVision;");
                //amazon test db
                optionsBuilder.UseSqlServer("Data Source=nbk-db1.c3e125qqptxq.eu-central-1.rds.amazonaws.com,1433;Database=nbk-Db1;Persist Security Info=True;User ID=nbkUser;Password=6DDipFLWBm;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<BuildingSupplierTemplate>(entity =>
            {
                entity.ToTable("BuildingSupplierTemplate", "nbkUser");
            });

            modelBuilder.Entity<ChecklistItemImage>(entity =>
            {
                entity.ToTable("ChecklistItemImage", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CaptureDate).HasColumnType("datetime");

                entity.Property(e => e.ChecklistItemId).HasColumnName("ChecklistItemID");

                entity.Property(e => e.ImageSize).HasMaxLength(50);

                entity.Property(e => e.ImageType).HasMaxLength(10);

                entity.Property(e => e.IsOkForFinalPdf).HasColumnName("isOkForFinalPDF");

                entity.HasOne(d => d.ChecklistItem)
                    .WithMany(p => p.ChecklistItemImage)
                    .HasForeignKey(d => d.ChecklistItemId)
                    .HasConstraintName("FK_ChecklistItemImage_ChecklistItems");
            });

            modelBuilder.Entity<ChecklistItemTemplate>(entity =>
            {
                entity.ToTable("ChecklistItemTemplate", "nbkUser");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.ChecklistItemTemplate)
                    .HasForeignKey(d => d.ChecklistId)
                    .HasConstraintName("FK_ChecklistItemTemplate_ChecklistTemplate");
            });

            modelBuilder.Entity<ChecklistItems>(entity =>
            {
                entity.ToTable("ChecklistItems", "nbkUser");

                entity.Property(e => e.EmailPartyDate).HasColumnType("datetime");

                entity.Property(e => e.EmailTempToPartiesIds).HasMaxLength(50);

                entity.Property(e => e.FixDate).HasColumnType("datetime");

                entity.Property(e => e.IsImageUploadedByParty).HasColumnName("isImageUploadedByParty");

                entity.Property(e => e.PartyUploadedImgDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.WasDev).HasColumnName("wasDev");

                entity.HasOne(d => d.Checklist)
                    .WithMany(p => p.ChecklistItems)
                    .HasForeignKey(d => d.ChecklistId)
                    .HasConstraintName("FK_ChecklistItems_ProjectChecklist");
            });

            modelBuilder.Entity<ChecklistTemplate>(entity =>
            {
                entity.ToTable("ChecklistTemplate", "nbkUser");

                entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<ContactBook>(entity =>
            {
                entity.ToTable("ContactBook", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompName).HasColumnName("compName");

                entity.Property(e => e.ContactNo).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.OldId).HasColumnName("OldID");

                entity.Property(e => e.PartyTypeId).HasColumnName("PartyTypeID");

                entity.Property(e => e.TempEmail)
                    .HasColumnName("Temp_Email")
                    .HasMaxLength(500);

                entity.Property(e => e.ToBeDeleted).HasColumnName("toBeDeleted");

                entity.Property(e => e.TripletexId)
                    .HasColumnName("TripletexID")
                    .HasMaxLength(200);

                entity.Property(e => e.VismaId)
                    .HasColumnName("VismaID")
                    .HasMaxLength(200);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ContactBook)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_ContactBook_City");
            });

            modelBuilder.Entity<Doc>(entity =>
            {
                entity.ToTable("Doc", "Party");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.PartyDocTypeId).HasColumnName("PartyDocTypeID");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.PartyTypeId).HasColumnName("PartyTypeID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.PartyDocType)
                    .WithMany(p => p.Doc)
                    .HasForeignKey(d => d.PartyDocTypeId)
                    .HasConstraintName("FK_Doc_DocType");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.Doc)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK_Doc_ContactBook");

                entity.HasOne(d => d.PartyType)
                    .WithMany(p => p.Doc)
                    .HasForeignKey(d => d.PartyTypeId)
                    .HasConstraintName("FK_Doc_PartyType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Doc)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Doc_Project");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.ToTable("DocType", "Party");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsRequired).HasColumnName("isRequired");

                entity.Property(e => e.PartyTypeId).HasColumnName("PartyTypeID");

                entity.HasOne(d => d.PartyType)
                    .WithMany(p => p.DocType)
                    .HasForeignKey(d => d.PartyTypeId)
                    .HasConstraintName("FK_DocType_PartyType");
            });

            modelBuilder.Entity<EmailHistory>(entity =>
            {
                entity.ToTable("EmailHistory", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FromEmail).HasMaxLength(250);

                entity.Property(e => e.IsEmail).HasColumnName("Is_email");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Subject).HasMaxLength(1000);

                entity.Property(e => e.ToEmail).HasMaxLength(250);

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.EmailHistory)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK_EmailHistory_ContactBook");

                entity.HasOne(d => d.PartyType)
                    .WithMany(p => p.EmailHistory)
                    .HasForeignKey(d => d.PartyTypeId)
                    .HasConstraintName("FK_EmailHistory_PartyType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.EmailHistory)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_EmailHistory_Project");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.ToTable("EmailTemplate", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<GeneralSetting>(entity =>
            {
                entity.ToTable("GeneralSetting", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.EmailAddress).HasMaxLength(500);

                entity.Property(e => e.EmailSenderName).HasMaxLength(500);

                entity.Property(e => e.Mobile).HasMaxLength(500);

                entity.Property(e => e.OrganizationalNumber).HasMaxLength(250);

                entity.Property(e => e.OwnerName).HasMaxLength(500);

                entity.Property(e => e.SenderEmailAddress).HasMaxLength(500);

                entity.Property(e => e.Telephone).HasMaxLength(500);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.GeneralSetting)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_GeneralSetting_City");

                entity.HasOne(d => d.PostCodeNavigation)
                    .WithMany(p => p.GeneralSetting)
                    .HasForeignKey(d => d.PostCode)
                    .HasConstraintName("FK_GeneralSetting_PostNumber");
            });

            modelBuilder.Entity<InspectionLog>(entity =>
            {
                entity.ToTable("InspectionLog", "nbkUser");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<PartyType>(entity =>
            {
                entity.ToTable("PartyType", "Party");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<PostNum>(entity =>
            {
                entity.ToTable("PostNum", "nbkUser");

                entity.Property(e => e.Kategori).HasMaxLength(50);
            });

            modelBuilder.Entity<PostNumber>(entity =>
            {
                entity.HasKey(e => e.Postnummer);

                entity.ToTable("PostNumber", "nbkUser");

                entity.Property(e => e.Postnummer).ValueGeneratedNever();

                entity.Property(e => e.Kategori).HasMaxLength(10);

                entity.Property(e => e.Kommunenavn).HasMaxLength(100);

                entity.Property(e => e.Poststed).HasMaxLength(100);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project", "nbkUser");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPartiesCdate)
                    .HasColumnName("AddPartiesCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.AnsvarligSokerCdate)
                    .HasColumnName("AnsvarligSokerCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.AssignInspectorCdate)
                    .HasColumnName("AssignInspectorCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.ContactPersonId).HasColumnName("ContactPersonID");

                entity.Property(e => e.CreateChecklistCdate)
                    .HasColumnName("CreateChecklistCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Dated).HasColumnType("datetime");

                entity.Property(e => e.EmailCustomerUpInspectionCd)
                    .HasColumnName("EmailCustomerUpInspectionCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FinalReportPdfCdate)
                    .HasColumnName("FinalReportPdfCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.GodkjensDate).HasColumnType("datetime");

                entity.Property(e => e.GratulererGodkjentCdate)
                    .HasColumnName("GratulererGodkjentCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.InspectionDate).HasColumnType("datetime");

                entity.Property(e => e.InspectorSignature).HasMaxLength(100);

                entity.Property(e => e.InvoiceSetCd)
                    .HasColumnName("InvoiceSetCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.InvoiceTripletexId)
                    .HasColumnName("InvoiceTripletexID")
                    .HasMaxLength(200);

                entity.Property(e => e.IsApprovedInspReport).HasColumnName("isApprovedInspReport");

                entity.Property(e => e.IsApprovedInspReportIsCompleted).HasColumnName("isApprovedInspReportIsCompleted");

                entity.Property(e => e.IsArchived).HasColumnName("isArchived");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsSubmitted).HasColumnName("isSubmitted");

                entity.Property(e => e.KontrollerklaeringPdfCd)
                    .HasColumnName("KontrollerklaeringPdfCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(100);

                entity.Property(e => e.Longitude).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PartiesDataCdate)
                    .HasColumnName("PartiesDataCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProjectImage).HasMaxLength(200);

                entity.Property(e => e.ProjectLeaderId).HasColumnName("ProjectLeaderID");

                entity.Property(e => e.ProjectStatus).HasMaxLength(50);

                entity.Property(e => e.ProjectSubCompleteCd)
                    .HasColumnName("ProjectSubCompleteCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProjectSubProcessCdate)
                    .HasColumnName("ProjectSubProcessCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.RemContactCustomerDate).HasColumnType("datetime");

                entity.Property(e => e.RemContactCustomerDdl).HasColumnName("RemContactCustomerDDL");

                entity.Property(e => e.ReviewInspReportCd)
                    .HasColumnName("ReviewInspReportCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.SetProLeaderContactCustomerCdate)
                    .HasColumnName("SetProLeaderContactCustomerCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SkipInspection).HasColumnName("skip_inspection");

                entity.Property(e => e.SoknadOmAnsvarsrettCdate)
                    .HasColumnName("SoknadOmAnsvarsrettCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SubmitInspectionRepRemindAgainCd)
                    .HasColumnName("SubmitInspectionRepRemindAgainCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.SubmitInspectionRepRemindCd)
                    .HasColumnName("SubmitInspectionRepRemindCD")
                    .HasColumnType("datetime");

                entity.Property(e => e.TakkBestillingenCdate)
                    .HasColumnName("TakkBestillingenCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TepmlateValue).HasMaxLength(50);

                entity.Property(e => e.UpcomingInspectionCdate)
                    .HasColumnName("UpcomingInspectionCDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.VismaId)
                    .HasColumnName("VismaID")
                    .HasMaxLength(200);

                entity.Property(e => e.VismaInvoiceId)
                    .HasColumnName("VismaInvoiceID")
                    .HasMaxLength(200);

                entity.HasOne(d => d.BuildingSupplier)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.BuildingSupplierId)
                    .HasConstraintName("FK_Project_BuildingSupplierTemplate");

                entity.HasOne(d => d.ContactPerson)
                    .WithMany(p => p.ProjectContactPerson)
                    .HasForeignKey(d => d.ContactPersonId)
                    .HasConstraintName("FK_Project_ContactBook_contactPersonId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProjectCustomer)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Project_ContactBook_customerId");

                entity.HasOne(d => d.Inspector)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.InspectorId)
                    .HasConstraintName("FK_Project_Users");

                entity.HasOne(d => d.ProjectLeader)
                    .WithMany(p => p.ProjectProjectLeader)
                    .HasForeignKey(d => d.ProjectLeaderId)
                    .HasConstraintName("FK_Project_ContactBook_projectLeaderId");
            });

            modelBuilder.Entity<ProjectChecklist>(entity =>
            {
                entity.ToTable("ProjectChecklist", "nbkUser");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectChecklist)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectChecklist_Project");
            });

            modelBuilder.Entity<ProjectLeaderTemplate>(entity =>
            {
                entity.ToTable("ProjectLeaderTemplate", "nbkUser");

                entity.Property(e => e.ContactNumber).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(150);
            });

            modelBuilder.Entity<ProjectParty>(entity =>
            {
                entity.ToTable("ProjectParty", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.PartyTypeId).HasColumnName("PartyTypeID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.ProjectParty)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK_ProjectParty_ContactBook");

                entity.HasOne(d => d.PartyType)
                    .WithMany(p => p.ProjectParty)
                    .HasForeignKey(d => d.PartyTypeId)
                    .HasConstraintName("FK_ProjectParty_PartyType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectParty)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectParty_Project");
            });

            modelBuilder.Entity<ProjectService>(entity =>
            {
                entity.ToTable("ProjectService", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectService)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectService_Project");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ProjectService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ProjectService_ProjectService");
            });

            modelBuilder.Entity<ProjectStatuses>(entity =>
            {
                entity.ToTable("ProjectStatuses", "nbkUser");

                entity.Property(e => e.Status).HasMaxLength(200);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChecklistTempId).HasColumnName("ChecklistTempID");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.Rate).HasMaxLength(50);

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

                entity.Property(e => e.TripletexId)
                    .HasColumnName("TripletexID")
                    .HasMaxLength(200);

                entity.Property(e => e.VismaId)
                    .HasColumnName("VismaID")
                    .HasMaxLength(200);

                entity.HasOne(d => d.ChecklistTemp)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.ChecklistTempId)
                    .HasConstraintName("FK_Service_ChecklistTemplate");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Service_ServiceType");
            });

            modelBuilder.Entity<ServicePerSlab>(entity =>
            {
                entity.ToTable("ServicePerSlab", "nbkUser");

                entity.Property(e => e.Rate).HasMaxLength(50);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicePerSlab)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServicePerSlab_Service");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType", "nbkUser");

                entity.Property(e => e.SortOrder).HasColumnName("sortOrder");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "nbkUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactNo).HasMaxLength(250);

                entity.Property(e => e.Designation).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Picture).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_User_UserType");
            });
        }
    }
}
