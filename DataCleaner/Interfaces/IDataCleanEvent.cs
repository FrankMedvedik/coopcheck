using System;
using DataClean.Models;

namespace DataClean.Interfaces
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