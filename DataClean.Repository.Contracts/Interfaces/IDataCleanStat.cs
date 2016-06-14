using System;

namespace DataClean.Repository.Contracts.Interfaces
{
    public interface IDataCleanStat
    {
        int? CacheCnt { get; set; }
        int? CleanCnt { get; set; }
        DateTime? CleanDate { get; set; }
        int ID { get; set; }
    }
}