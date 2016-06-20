using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models.Reports;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Domain.Services
{
    public class JobLogSvc
    {
        public static string BadJobName = "job number is not defined";
        private readonly ICoopCheckEntities _ctx;

        public JobLogSvc(ICoopCheckEntities ctx)
        {
            _ctx = ctx;
        }
        public  async Task<IJobLog> GetJobLog(int jobNum)
        {
                if (jobNum != 0)
                try
                {
                    return await _ctx.JobLogs.FirstAsync(j => j.JobNum == jobNum);
                }
                catch (Exception e)
                {
                    
                    Console.Out.Write(e + jobNum.ToString());
                }
                return new JobLog();
         }
        
        public async Task<string> GetJobName(int jobNum)
        {
            var z = await GetJobLog(jobNum);
            return (z == null) ? BadJobName : z.JobName;
        }
    }
}