﻿
using CoopCheck.Repository.Services;

namespace CoopCheck.Repository
{
    using System;

    public partial class vwBatchRpt
    {
        public bool BadBatch
        {
            get { return batch_dscr.Substring(0, 8) != job_num.ToString(); }
        }
    }
}