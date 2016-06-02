
using CoopCheck.Repository.Services;

namespace CoopCheck.Repository
{
    using System;

    public partial class vwOpenBatch
    {
        public bool BadBatch
        {
            get { return (batch_dscr == null) || batch_dscr.Substring(0, 8) != jobnum.ToString(); }
        }

        public bool IsSwiftBatch
        {
            get { return (batch_dscr != null ) && batch_dscr.Contains("Swift"); }
        }
    }
}
