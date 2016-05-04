using System;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Services
{
    public static class JobLogSvc
    {
        public static string BadJobName = "job number is not defined";

        public static async Task<JobLog> GetJobLog(int jobNum)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = new JobLog();
                if (jobNum != 0)
                try
                {
                    x = ctx.JobLogs.First(j => j.JobNum == jobNum);
                }
                catch (Exception e)
                {
                    x = new JobLog();
                    Console.Out.Write(e + jobNum.ToString());
                }
                return x;
            }
        }

        public static async Task<string> GetJobName(int jobNum)
        {
            var z = await GetJobLog(jobNum);
            return (z == null) ? BadJobName : z.JobName;
        }
    }
}