using CoopCheck.Repository;

namespace Email1099.WPF.Services
{
    public static class JobSvc
    {
        public static vwJobRpt GetJob( int job_num)
        {
            return new vwJobRpt
            {
                projmgr = "frank medvedik",
            job_num = 20160001,
             jobname = "the best job!"
             
            };
        }
    }
}
