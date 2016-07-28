
using CoopCheck.Repository.Services;

namespace CoopCheck.Repository
{
    using System;

    public partial class vwBatchRpt
    {
        public bool BadBatch
        {
            get
            {
                if (string.IsNullOrEmpty(batch_dscr)) return false;
                if (batch_dscr.Length <8 ) return false;
                return batch_dscr.Substring(0, 8) != job_num.ToString();
            }
        }
    }
}

