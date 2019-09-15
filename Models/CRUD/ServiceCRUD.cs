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

namespace NBKProject.Models.CRUD
{
    public class ServiceCRUD
    {
        public ServiceENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Service Obj = dbcontext.Service.Where(x => x.Id == Id).FirstOrDefault();
            List<ServicePerSlab> ServicePerSlabList = dbcontext.ServicePerSlab.Where(x => x.ServiceId == Obj.Id).ToList();
            List<ServicePerSlabENT> ServicePerSlabListENT = new List<ServicePerSlabENT>();
            ServicePerSlabListENT.AddRange(ServicePerSlabList.Select(x => new ServicePerSlabENT
            { Id = x.Id, RangeFrom = x.RangeFrom, RangeTo = x.RangeTo, Rate = x.Rate, ServiceId = x.ServiceId }).ToList());
           ServiceENT Data = new ServiceENT()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                Description = Obj.Description,
                ServiceTypeId = Obj.ServiceTypeId,
                ServiceChargedAs = Obj.ServiceChargedAs,
                Rate = Obj.Rate,
                ServicePerSlabList = ServicePerSlabListENT

           };

            
            return Data;


        }
        public List<ServiceENT> GetAll(int PageNo, string SearchByName, string SearchByDescription)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<Service> Obj = new List<Service>();
            if (PageNo == 0)
            {
                if (SearchByName == null && SearchByDescription == null)
                {
                    Obj = dbcontext.Service.ToList();
                }
                else if (SearchByName != null)
                {
                    Obj = dbcontext.Service.Where(x => x.Name == SearchByName).ToList();
                }
                else if (SearchByDescription != null)
                {
                    Obj = dbcontext.Service.Where(x => x.Description == SearchByDescription).ToList();
                }

            }
            else if (PageNo > 0)
            {
                if (PageNo == 1)
                {
                    PageNo = PageNo * 10;

                    if (SearchByName == null && SearchByDescription == null)
                    {
                        Obj = dbcontext.Service.Take(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.Service.Where(x => x.Name == SearchByName).Take(PageNo).ToList();
                    }
                    else if (SearchByDescription != null)
                    {
                        Obj = dbcontext.Service.Where(x => x.Description  == SearchByDescription).Take(PageNo).ToList();
                    }
                    
                }
                else
                {
                    PageNo = PageNo - 1;
                    PageNo = PageNo * 10;

                    if (SearchByName == null && SearchByDescription == null )
                    {
                        Obj = dbcontext.Service.Skip(PageNo).ToList();
                    }
                    else if (SearchByName != null)
                    {
                        Obj = dbcontext.Service.Where(x => x.Name == SearchByName).Skip(PageNo).ToList();
                    }
                    else if (SearchByDescription != null)
                    {
                        Obj = dbcontext.Service.Where(x => x.Name == SearchByDescription).Skip(PageNo).ToList();
                    }
                    
                }
            }
            List<ServiceENT> Data = new List<ServiceENT>();
            Data.AddRange(Obj.Select(i => new ServiceENT
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                ServiceTypeId = i.ServiceTypeId,
                ServiceChargedAs = i.ServiceChargedAs,
                Rate = i.Rate
            }));

            return Data;
        }

        public bool CheckIfServiceAsocWithProject(int id)
        {
            bool ifExist = false;
            NbkDbEntities dbcontext = new NbkDbEntities();
            ProjectService obj = dbcontext.ProjectService.Where(x => x.ServiceId == id).FirstOrDefault();
            if (obj != null)
            {
                ifExist = true;
            }
            return ifExist;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            Service Obj = dbcontext.Service.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.Service.Remove(Obj);
            dbcontext.SaveChanges();
        }

        public ServiceENT UpdateSelectSingle(ServiceENT Obj)
        {
            if (Obj.ServiceChargedAs == 1)
            {
                Obj.Rate = Obj.Rate;
            }
            else
            {
                Obj.Rate = "0";
            }
            new ServiceCRUD().DeleteSlabService(Obj.Id);
            if (Obj.ServiceChargedAs == 2)
            {

                //Add Service rates per slabs
                AddServiceRateSlabsList(Obj.Id, Obj.ServicePerSlabList);
            }

            NbkDbEntities dbcontext = new NbkDbEntities();
            Service Data = new Service()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                Description = Obj.Description,
                ServiceTypeId = Obj.ServiceTypeId,
                ServiceChargedAs = Obj.ServiceChargedAs,
                Rate = Obj.Rate
            };


            dbcontext.Service.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.Description).IsModified = true;
            update.Property(x => x.ServiceTypeId).IsModified = true;
            update.Property(x => x.ServiceChargedAs).IsModified = true;
            update.Property(x => x.Rate).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public ServiceENT CreateSingle(ServiceENT Obj)
        {
            if (Obj.ServiceChargedAs == 1)
            {
                Obj.Rate = Obj.Rate;
            }
            else
            {
                Obj.Rate = "0";
            }
            if (Obj.ServiceChargedAs == 2)
            {

                //Add Service rates per slabs
                AddServiceRateSlabsList(Obj.Id, Obj.ServicePerSlabList);
            }

            NbkDbEntities dbcontext = new NbkDbEntities();
            Service Data = new Service()
            {
                Name = Obj.Name,
                Description = Obj.Description,
                ServiceTypeId = Obj.ServiceTypeId,
                ServiceChargedAs = Obj.ServiceChargedAs,
                Rate = Obj.Rate
            };


            dbcontext.Service.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            //Adding service workflow
            foreach (var item in Obj.ServiceWorkflowCategory)
            {
                dbcontext = new NbkDbEntities();
                ServiceWorkflowCategory ServiceData = new ServiceWorkflowCategory()
                {
                    ServiceId = item.ServiceId,
                    WorkflowCategoryId = item.WorkflowCategoryId
                };
                dbcontext.ServiceWorkflowCategory.Add(ServiceData);
                dbcontext.SaveChanges();
            }
            

            return Obj;
        }

        public void AddServiceRateSlabsList(int id, List<ServicePerSlabENT> ServicePerSlabList)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            var list = ServicePerSlabList;
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    var ServiceSlabObj = new ServicePerSlab();
                    ServiceSlabObj.ServiceId = id;
                    ServiceSlabObj.RangeFrom = item.RangeFrom;
                    ServiceSlabObj.RangeTo = item.RangeTo;
                    ServiceSlabObj.Rate = item.Rate;


                    dbcontext.ServicePerSlab.Add(ServiceSlabObj);
                    dbcontext.SaveChanges();
                }
            }
        }
        public void DeleteSlabService(int id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            dbcontext.ServicePerSlab.RemoveRange(dbcontext.ServicePerSlab.Where(x => x.ServiceId == id));
            dbcontext.SaveChanges();
        }
    }
}
