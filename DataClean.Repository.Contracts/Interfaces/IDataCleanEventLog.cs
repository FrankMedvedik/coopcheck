using System;

namespace DataClean.Repository.Contracts.Interfaces
{
    public interface IDataCleanEventLog
    {
        DateTime DataCleanDate { get; set; }
        string DataCleanEventJson { get; set; }
        int ID { get; set; }
    }
}