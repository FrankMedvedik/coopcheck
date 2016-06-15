using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Models;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public  class JobLogSvc : IJobLogSvc
    {
        private readonly ICoopCheckEntities _coopCheckEntities = null;

        public JobLogSvc(ICoopCheckEntities coopCheckEntities)
        {
            _coopCheckEntities = coopCheckEntities;
        }

        public string BadJobName = "job number is not defined";

        public async Task<IJobLog> GetJobLog(int jobNum)
        {
               var x = new JobLog();
                if (jobNum != 0)
                try
                {
                    return await _coopCheckEntities.JobLogs.FirstAsync(j => j.JobNum == jobNum);
                }
                catch (Exception e)
                {
                    x = new JobLog();
                    Console.Out.Write(e + jobNum.ToString());
                }
                return x;
        }
       

        public async Task<string> GetJobName(int jobNum)
        {
            var z = await GetJobLog(jobNum);
            return (z == null) ? BadJobName : z.JobName;
        }
    }
}