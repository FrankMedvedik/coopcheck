using System;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleanEvent
    {
        int ID { get; }
        DateTime DataCleanDate { get; set; }
        bool HasBeenDataCleaned { get; }
       IInputStreetAddress Input { get; set; }
       IOutputStreetAddress Output { get; set; }
        int GetHashCode();
    }
    }