using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using CoopCheck.Reports.Contracts;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository;

namespace CoopCheck.Reports.Services
{
    public  class JobLogSvc : IJobLogSvc
    {
        private ICoopCheckEntities _coopCheckEntities;


        public ICoopCheckEntities CoopCheckEntities
        {
            get { return _coopCheckEntities ?? (_coopCheckEntities = new CoopCheckEntities()); }
            set { _coopCheckEntities = value; }
        }

       

        public async Task<Contracts.Models.JobLog> GetJobLog(int jobNum)
        {
               var x = new Contracts.Models.JobLog();
                if (jobNum != 0)
                try
                {
                    var dbJob = await _coopCheckEntities.JobLogs.FirstAsync(j => j.JobNum == jobNum);
                    x = Mapper.Map<Repository.JobLog, Contracts.Models.JobLog>(dbJob);
                    
                }
                catch (Exception e)
                {
                    x = new Contracts.Models.JobLog();
                    Console.Out.Write(e + jobNum.ToString());
                }
                return x;
        }
       

        public async Task<string> GetJobName(int jobNum)
        {
            var z = await GetJobLog(jobNum);
            return (z == null) ? ReportConstants.BadJobName : z.JobName;
        }
    }
}