
using CoopCheck.Repository.Services;

namespace CoopCheck.Repository
{
    using System;

    public partial class vwOpenBatch
    {
        public bool BadBatch
        {
            get { return batch_dscr.Substring(0, 8) != jobnum.ToString(); }
        }
    }
}
