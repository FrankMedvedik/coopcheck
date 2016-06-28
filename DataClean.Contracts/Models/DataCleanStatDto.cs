using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Repository.Contracts.Interfaces;

namespace DataClean.Contracts.Models
{

    public class DataCleanStatDto : IDataCleanStat
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CleanDate { get; set; }
        public Nullable<int> CacheCnt { get; set; }
        public Nullable<int> CleanCnt { get; set; }
    }
}

