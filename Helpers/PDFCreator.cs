using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.Ajax.Utilities;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NBKProject.Entities;
using NBKProject.Models.NbkEF;
using NBKProject.Models.CRUD;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text.pdf;
using System.IO;
using System.Threading;
using iTextSharp.text;
using System.Net;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using iTextSharp.text.html.simpleparser;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace NBKProject.Helpers
{
    public class PDFCreator
    {
       

        public ProjectWorkflowENT PdfGenerate(ProjectWorkflowENT Param, IHostingEnvironment _hostingEnvironment)
        {
            ProjectENT model = new ProjectCRUD().SelectSingle(Param.ProjectId);
            string title = RemoveSpecialCharacters(model.Title);
            string fileName = title + ".pdf";
            
            CompanyProfileENT setings = new CompanyCRUD().SelectAll();

            #region Testing code
            //string contentRootPath = "https:\\nbk-api-dev.azurewebsites.net";//_hostingEnvironment.ContentRootPath;
            //Uri baseUri = new Uri("http://nbk-api-dev.azurewebsites.net");
            //Uri myUri = new Uri(baseUri, "Resources/Files/");
            //Param.AttachmentURL = myUri + fileName;
            //HTMLToPdf(myUri + "Sample.pdf", myUri + "Docs/" + fileName, model, setings, contentRootPath, Param);
            #endregion

            var uriBuilder = new UriBuilder("https://nbk-api-dev.azurewebsites.net/Resources/Files/");
            var uriBuilderDoc = new UriBuilder("https://nbk-api-dev.azurewebsites.net/Resources/Files/Docs/");
            var uriBuilderImage = new UriBuilder("https://nbk-api-dev.azurewebsites.net/Resources/");
            Uri finalUrl = uriBuilder.Uri;
            Uri finalUrlDoc = uriBuilderDoc.Uri;
            Uri finalUrlImage = uriBuilderImage.Uri;
            //var request = WebRequest.Create(finalUrl);
            //string webRootPath = _hostingEnvironment.WebRootPath;
            //string contentRootPath =  "https://nbk-api-dev.azurewebsites.net";//_hostingEnvironment.ContentRootPath;
            Param.AttachmentURL = "https:\\nbk-api-dev.azurewebsites.net" + "\\Resources\\Files\\" + fileName;
            HTMLToPdf(finalUrl + "Sample.pdf", finalUrlDoc +  fileName, model, setings, finalUrlImage+"", Param);
            return Param;
        }

        public string RemoveSpecialCharacters(string input)
        {
            input = Regex.Replace(input, @"\s+", "");
            Regex r = new Regex("(?:[^a-zA-Z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            return r.Replace(input, String.Empty);
        }

        public void HTMLToPdf(string FilePath, string newpath, ProjectENT model, CompanyProfileENT setings, string contentRootPath, ProjectWorkflowENT Param)
        {
            PostCodeENT PostNumbData = new MiscellaneousCRUD().GetSinglePostCodeByPostNumber(Convert.ToString(setings.postCode));
            List<ProjectService> ListProjectServices = new ProjectCRUD().ListOfProjectServicesByWorkflowID(Param.WorkflowId, Param.ProjectId);
            List<int?> ServiceIDs = ListProjectServices.Select(x => x.ServiceId).ToList();
            List<ServiceENT> ServiceMultiData = new ServiceCRUD().GetMultipleServiceByMultiServiceIDs(ServiceIDs);

            string pdfPath = newpath;  // The file location of the new PDF file

            PdfReader reader = new PdfReader(FilePath); // filename is the path to the PDF template file.
            FileStream output = new FileStream(pdfPath, FileMode.OpenOrCreate);  // you can also use a Memory Stream.
            PdfStamper stamper = new PdfStamper(reader, output); // the stamper is required for updating fields.
            AcroFields af = reader.AcroFields;
            stamper.AcroFields.GenerateAppearances = false;
            ////Table one
            //stamper.AcroFields.SetField("Tekst498", "uzb");
            //stamper.AcroFields.SetField("Tekst257", "uzb1");
            //stamper.AcroFields.SetField("Tekst261", "uzb2");
            //stamper.AcroFields.SetField("Tekst266", "uzb3");
            stamper.AcroFields.SetField("Tekst264", "uzb3");
            stamper.AcroFields.SetField("Tekst257", model.GardsNo);
            stamper.AcroFields.SetField("Tekst261", model.Bruksnmmer);
            stamper.AcroFields.SetField("Tekst264", model.Kommune);
            stamper.AcroFields.SetField("Tekst254", model.Address);
            stamper.AcroFields.SetField("Tekst255", model.PostNo);
            stamper.AcroFields.SetField("Tekst256", model.Poststed);
            ////Table two
            stamper.AcroFields.SetField("Foretak_2", setings.companyName);
            stamper.AcroFields.SetField("Organisasjonsnr", setings.organizationalNumber);
            stamper.AcroFields.SetField("Adresse_2", setings.address);
            stamper.AcroFields.SetField("Tekst4", Convert.ToString(setings.postCode));
            //stamper.AcroFields.SetField("Tekst5", setings.City.Name);
            stamper.AcroFields.SetField("Tekst5", PostNumbData.Poststed);
            stamper.AcroFields.SetField("Tekst105", setings.ownerName);
            stamper.AcroFields.SetField("Telefon", setings.telephone);
            stamper.AcroFields.SetField("Mobiltelefon", setings.mobile);
            //stamper.AcroFields.SetField("Tekst106", setings.EmailAddress);
            ////Services Table
            int a = 1;
            //Empty 1st two rows of grid
            stamper.AcroFields.SetField("NedtrekkslisteRow1", "-");
            stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow1", "");
            stamper.AcroFields.SetField("Nedtrekksliste23", "-");
            stamper.AcroFields.SetField("Avmerkingsboks10#1", "Off");
            stamper.AcroFields.SetField("Avmerkingsboks810", "Off");

            stamper.AcroFields.SetField("Nedtrekk121", "-");
            stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow2", "");
            stamper.AcroFields.SetField("Nedtrekksliste24", "-");
            stamper.AcroFields.SetField("Avmerkingsboks1101", "Off");
            stamper.AcroFields.SetField("Avmerkingsboks110#10", "Off");
            //Empty Ends
            foreach (var item in ListProjectServices)
            {
                ServiceENT DataSingleService = ServiceMultiData.Where(x=>x.Id ==(Convert.ToInt32(item.ServiceId))).FirstOrDefault();
                if (DataSingleService.ServiceTypeId != 3)
                {
                    if (a == 1)
                    {
                        //string valuename = GetCheckBoxExportValue(af, "Avmerkingsboks10#1");
                        stamper.AcroFields.SetField("NedtrekkslisteRow1", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow1", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste23", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks10#1", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks810", "Ja");
                    }
                    else if (a == 2)
                    {
                        //string valuename = GetCheckBoxExportValue(af, "Avmerkingsboks1102");
                        stamper.AcroFields.SetField("Nedtrekk121", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow2", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste24", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1101", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks110#10", "Ja");
                    }
                    else if (a == 3)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow21", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow3", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste25", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1102", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks1102#10", "Ja");
                    }
                    else if (a == 4)
                    {
                        //stamper.AcroFields.SetField("NedtrekkslisteRow4", "kontroll");
                        stamper.AcroFields.SetField("NedtrekkslisteRow3", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow4", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste26", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1103", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8103", "Ja");
                    }
                    else if (a == 5)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow5", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow5", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste27", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1104", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8104", "Ja");
                    }
                    else if (a == 6)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow6", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow6", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste28", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1105", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8105", "Ja");
                    }
                    else if (a == 7)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow7", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow7", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste29", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1106", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8106", "Ja");
                    }
                    else if (a == 8)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow8", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow8", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste30", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1107", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8107", "Ja");
                    }
                    else if (a == 9)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow9", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow9", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste31", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1108", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8108", "Ja");
                    }
                    else if (a == 10)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow10", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow10", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste32", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks1109", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks8109", "Ja");
                    }
                    else if (a == 11)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow109", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow109", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste3131", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks11080", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks81081", "Ja");
                    }
                    else if (a == 12)
                    {
                        stamper.AcroFields.SetField("NedtrekkslisteRow1110", "kontroll");
                        stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow1010", item.Service.Name);
                        stamper.AcroFields.SetField("Nedtrekksliste3232", DataSingleService.ServiceTypeId.ToString());
                        stamper.AcroFields.SetField("Avmerkingsboks11092", "Ja");
                        stamper.AcroFields.SetField("Avmerkingsboks81092", "Ja");
                    }
                    #region commented
                    //stamper.AcroFields.SetField("NedtrekkslisteRow1", "kontroll");
                    //stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow1", item.Service.Name);
                    ////stamper.AcroFields.SetField("Responsible_ActionClass_" + a, item.Quantity.ToString());
                    //stamper.AcroFields.SetField("Nedtrekksliste23", item.Service.ServiceTypeID.ToString());
                    ////stamper.AcroFields.SetField("Responsible_FramePermit_" + a, "X");
                    ////stamper.AcroFields.SetField("Responsible_BuildingPermit_" + a, "X");

                    //stamper.AcroFields.SetField("Avmerkingsboks10#1", "47");
                    //stamper.AcroFields.SetField("Avmerkingsboks810", "78");
                    ////var aaa = stamper.AcroFields.Fields["Avmerkingsboks10#1"]; 

                    //stamper.AcroFields.SetField("Responsible_Function_" + a, "kontroll");
                    //stamper.AcroFields.SetField("Responsible_Description_" + a, item.Service.Name);
                    ////stamper.AcroFields.SetField("Responsible_ActionClass_" + a, item.Quantity.ToString());
                    //stamper.AcroFields.SetField("Responsible_ActionClass_" + a, item.Service.ServiceTypeID.ToString());
                    //stamper.AcroFields.SetField("Responsible_FramePermit_" + a, "X");
                    //stamper.AcroFields.SetField("Responsible_BuildingPermit_" + a, "X");
                    //stamper.AcroFields.SetField("Responsible_TempUsePermit_" + a, "X");
                    //stamper.AcroFields.SetField("Responsible_Complete_" + a, "X");
                    #endregion
                    a++;
                }
            }
            if (a == 1 && ListProjectServices.Count == 0)
            {
                stamper.AcroFields.SetField("NedtrekkslisteRow1", "-");
                stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow1", "");
                stamper.AcroFields.SetField("Nedtrekksliste23", "-");
                stamper.AcroFields.SetField("Avmerkingsboks10#1", "Off");
                stamper.AcroFields.SetField("Avmerkingsboks810", "Off");

                a++;
            }
            if (a == 2 && ListProjectServices.Count == 1)
            {
                stamper.AcroFields.SetField("Nedtrekk121", "-");
                stamper.AcroFields.SetField("Beskrivelse av ansvarsområdetRow2", "");
                stamper.AcroFields.SetField("Nedtrekksliste24", "-");
                stamper.AcroFields.SetField("Avmerkingsboks1101", "Off");
                stamper.AcroFields.SetField("Avmerkingsboks110#10", "Off");
            }

            ////Last Table    

            //Changings done after clients feedback/////////////////////////////////////////////////////////
            //stamper.AcroFields.SetField("ResponsibleApp_Company", model.Contact.Name);
            //stamper.AcroFields.SetField("Date", DateTime.Now.ToString("dd.MM.yy"));
            //stamper.AcroFields.SetField("ResponsibleApp_Person_Cap", model.Customer.Name);
            //stamper.AcroFields.SetField("ResponsibleApp_Company", "");

            stamper.AcroFields.SetField("Tekst98", DateTime.Now.ToString("dd.MM.yy"));
            stamper.AcroFields.SetField("Gjentas med blokkbokstaver", setings.ownerName);




            var pdfContentByte = stamper.GetOverContent(1);

            //var url = @"http://nbk.d.com.pk/Resources/global/images/RuneSignature.jpg";
            var url = contentRootPath+"/Images/RuneSignature.jpg";

            //var uri = new Uri(url);
            //var path = Path.GetFileName(uri.AbsolutePath);


            //HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(url);

            //  below lines will be commented for azure

            //WebResponse imageResponse = imageRequest.GetResponse();
            //Stream inputImageStream = imageResponse.GetResponseStream();
            //////Stream inputImageStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
            //image.SetAbsolutePosition(150, 60);
            //image.ScalePercent(40f);
            //pdfContentByte.AddImage(image);
            #region commented
            ////Add signature image code STARTS
            //AcroFields.FieldPosition fieldPosition = stamper.AcroFields.GetFieldPositions("Gjentas med blokkbokstaver")[0];
            //PushbuttonField imageField = new PushbuttonField(stamper.Writer, fieldPosition.position, "Gjentas med blokkbokstaver");
            //imageField.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
            //string imageFile = "http://nbk.d.com.pk/Resources/global/images/RuneSignature.jpg";
            //imageField.Image = iTextSharp.text.Image.GetInstance(imageFile);

            //imageField.ScaleIcon = PushbuttonField.SCALE_ICON_ALWAYS;
            //imageField.ProportionalIcon = false;
            //imageField.Options = BaseField.READ_ONLY;

            //stamper.AcroFields.RemoveField("Gjentas med blokkbokstaver");
            //stamper.AddAnnotation(imageField.Field, fieldPosition.page);
            ////Add signature image code ENDS

            ////stamper.AcroFields.ReplacePushbuttonField("Gjentas med blokkbokstaver", imageField.Field);
            #endregion
            stamper.Close();
            reader.Close();
        }

        
        public ProjectWorkflowENT PdfSave(ProjectWorkflowENT Param, IHostingEnvironment _hostingEnvironment, IFormFile FileInRequest)
        {
            //Deleting old file with same name
            String fileNameInDoc = Param.AttachmentURL;
            if (fileNameInDoc != null || fileNameInDoc != string.Empty)
            {
                if ((System.IO.File.Exists(fileNameInDoc)))
                {
                    System.IO.File.Delete(fileNameInDoc);
                }
            }


            //Renaming File
            string fileNameTrim = FileInRequest.FileName.Remove(FileInRequest.FileName.Length - 5);
            var filetitle = RemoveSpecialCharacters(fileNameTrim);
            string RemoveSpaceFileTitle = filetitle.Replace(" ", "");
            if (RemoveSpaceFileTitle.Length > 30)
            {
                RemoveSpaceFileTitle = RemoveSpaceFileTitle.Substring(0, 30);
            }

            ProjectENT model = new ProjectCRUD().SelectSingle(Param.ProjectId);
            var title = RemoveSpecialCharacters(model.Title);
            string RemoveSpaceTitle = title.Replace(" ", "");
            if (RemoveSpaceTitle.Length > 30)
            {
                RemoveSpaceTitle = RemoveSpaceTitle.Substring(0, 30);
            }
            //finally concatinating title, file name, random name
            //file name convention
            string[] fileExt = FileInRequest.FileName.Split('.');
            int countIndex = fileExt.Count();
            string ext = fileExt[countIndex - 1];
            string CombinedName = RemoveSpaceTitle + "-" + RemoveSpaceFileTitle + "." + ext;



            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var uploads = contentRootPath+ "\\Resources\\Files\\Docs\\";
            
             var filePath = Path.Combine(uploads, CombinedName);
             using (var fileStream = new FileStream(filePath, FileMode.Create))
             {
                FileInRequest.CopyToAsync(fileStream);
             }

            string OrignalFileName = PDFReadOnly(CombinedName, uploads);

            Param.FileName = OrignalFileName;
            Param.RootURL = uploads;

            return Param;
        }


        public string PDFReadOnly(string fileName, string path)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Resources\\Files\\Docs\\";
            string FilePath = path + fileName;
            //string NewFileName = "Ro" + fileName;
            string fName = fileName.Split('.')[0];
            //fName = fName + "-ERKLÆRING-ANSVARSRETT.pdf";
            Random rand = new Random();
            fName = fName + "-AnsvarligSokerFile-" + rand.Next(0, 2500) + ".pdf";
            string NewFileName = fName;
            string newpath = path + NewFileName;
            //Delete file
            FileDelete(newpath);

            string pdfPath = newpath;  // The file location of the new PDF file

            PdfReader reader = new PdfReader(FilePath); // filename is the path to the PDF template file.

            FileStream output = new FileStream(pdfPath, FileMode.OpenOrCreate);  // you can also use a Memory Stream.

            PdfStamper stamper = new PdfStamper(reader, output); // the stamper is required for updating fields.

            //Fields seting to Read only
            foreach (var de in stamper.AcroFields.Fields)
            {
                stamper.AcroFields.SetFieldProperty(de.Key.ToString(),
                                           "setfflags",
                                            PdfFormField.FF_READ_ONLY,
                                            null);
            }
            //http://stackoverflow.com/questions/11171864/set-all-pdf-fields-to-readonly


            stamper.Close();
            reader.Close();

            //Delete file first
            FileDelete(FilePath);

            return NewFileName;
        }

        public void FileDelete(string PathWithFileName)
        {
            if (PathWithFileName != null || PathWithFileName != string.Empty)
            {
                if ((System.IO.File.Exists(PathWithFileName)))
                {
                    System.IO.File.Delete(PathWithFileName);
                }
            }
        }
    }
}
