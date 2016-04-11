using System;
using CoopCheck.Repository;
using Exception = System.Exception;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace Email1099.WPF.Services
{
    public static class JobSvc
    {
        public static vwJobRpt GetJob( int job_num)
        {
            return new vwJobRpt
            {
                projmgr = "Linda Diehl",
            job_num = 20160001,
             jobname = "the best job!"
             
            };
        }
    }
}
