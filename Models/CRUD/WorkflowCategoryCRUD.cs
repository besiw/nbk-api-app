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
    public class WorkflowCategoryCRUD
    {
        #region WorkflowCategory
        public WorkflowCategoryENT SelectSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategory Obj = dbcontext.WorkflowCategory.Where(x => x.Id == Id).FirstOrDefault();
            WorkflowCategoryENT Data = new WorkflowCategoryENT()
            {
                Id = Obj.Id,
                Name = Obj.Name, 
                IsDefault = Obj.IsDefault
            };
            return Data;


        }
        public List<WorkflowCategoryENT> GetAll()
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<WorkflowCategory> Obj = dbcontext.WorkflowCategory.ToList();
            List<WorkflowCategoryENT> Data = new List<WorkflowCategoryENT>();
            Data.AddRange(Obj.Select(i => new WorkflowCategoryENT
            {
                Id = i.Id,
                Name = i.Name,
                IsDefault = i.IsDefault
            }));

            return Data;
        }

        public void DeleteSingle(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategory Obj = dbcontext.WorkflowCategory.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.WorkflowCategory.Remove(Obj);
            dbcontext.SaveChanges();            
        }

        public WorkflowCategoryENT UpdateSelectSingle(WorkflowCategoryENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategory Data = new WorkflowCategory()
            {
                Id = Obj.Id,
                Name = Obj.Name,
                IsDefault = Obj.IsDefault
            };


            dbcontext.WorkflowCategory.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.IsDefault).IsModified = true;

            dbcontext.SaveChanges();

            return Obj;
        }


        public WorkflowCategoryENT CreateSingle(WorkflowCategoryENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategory Data = new WorkflowCategory()
            {
                Name = Obj.Name,
                IsDefault = Obj.IsDefault
            };


            dbcontext.WorkflowCategory.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }
        #endregion

        #region WorkflowCategorySteps
        public WorkflowCategoryStepENT SelectSingleWorkflowCategoryStep(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategorySteps Obj = dbcontext.WorkflowCategorySteps.Where(x => x.Id == Id).FirstOrDefault();
            WorkflowCategoryStepENT Data = new WorkflowCategoryStepENT()
            {
                Id = Obj.Id,
                WorkflowCategoryId = Obj.WorkflowCategoryId,
                StepName = Obj.StepName,
                IsActive = Obj.IsActive,
                IsTransferable = Obj.IsTransferable,
                StepSequence = Obj.StepSequence
            };
            return Data;
        }

        public WorkflowCategoryStepENT CreateSingleWorkflowCategoryStep(WorkflowCategoryStepENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategorySteps Data = new WorkflowCategorySteps()
            {
                WorkflowCategoryId = Obj.WorkflowCategoryId,
                StepName = Obj.StepName,
                IsActive = Obj.IsActive,
                IsTransferable = Obj.IsTransferable,
                StepSequence = Obj.StepSequence
            };


            dbcontext.WorkflowCategorySteps.Add(Data);
            dbcontext.SaveChanges();

            Obj.Id = Data.Id;

            return Obj;
        }

        public List<WorkflowCategoryStepENT> SelectSingleWorkflowCategoryStepsForOneWorkflow(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            List<WorkflowCategorySteps> Obj = dbcontext.WorkflowCategorySteps.Where(x => x.WorkflowCategoryId == Id).ToList();
            List<WorkflowCategoryStepENT> Data = new List<WorkflowCategoryStepENT>();
            Data.AddRange(Obj.Select(i => new WorkflowCategoryStepENT
            {
                Id = i.Id,
                WorkflowCategoryId = i.WorkflowCategoryId,
                StepName = i.StepName,
                IsActive = i.IsActive,
                IsTransferable = i.IsTransferable,
                StepSequence = i.StepSequence
            }));

            return Data;
        }

        public WorkflowCategoryStepENT UpdateSingleWorkflowCategoryStep(WorkflowCategoryStepENT Obj)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategorySteps Data = new WorkflowCategorySteps()
            {
                Id = Obj.Id,
                WorkflowCategoryId = Obj.WorkflowCategoryId,
                StepName = Obj.StepName,
                IsActive = Obj.IsActive,
                IsTransferable = Obj.IsTransferable,
                StepSequence = Obj.StepSequence
            };


            dbcontext.WorkflowCategorySteps.Attach(Data);
            var update = dbcontext.Entry(Data);
            update.Property(x => x.WorkflowCategoryId).IsModified = true;
            update.Property(x => x.StepName).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            update.Property(x => x.IsTransferable).IsModified = true;
            update.Property(x => x.StepSequence).IsModified = true;
            dbcontext.SaveChanges();
            return Obj;
        }


        public void DeleteSingleWorkflowCategoryStep(int Id)
        {
            NbkDbEntities dbcontext = new NbkDbEntities();
            WorkflowCategorySteps Obj = dbcontext.WorkflowCategorySteps.Where(x => x.Id == Id).FirstOrDefault();
            dbcontext.WorkflowCategorySteps.Remove(Obj);
            dbcontext.SaveChanges();
        }

       


        
        #endregion
    }
}
