using System;
using DataClean.Models;

namespace DataClean.Interfaces
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