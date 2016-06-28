using System;
using DataClean.Contracts.Models;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleanEvent
    {
        int ID { get; }
        DateTime DataCleanDate { get; set; }
        bool HasBeenDataCleaned { get; }
       InputStreetAddress Input { get; set; }
       OutputStreetAddress Output { get; set; }
        int GetHashCode();
    }
    }