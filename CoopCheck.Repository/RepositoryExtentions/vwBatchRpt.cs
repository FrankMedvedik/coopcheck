namespace CoopCheck.Repository
{
    using System;

    public partial class vwBatchRpt : IvwBatchRpt
    {
        public bool BadBatch
        {
            get { return batch_dscr.Substring(0, 8) != job_num.ToString(); }
        }
    }
}
