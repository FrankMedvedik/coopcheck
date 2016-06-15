﻿using System;
using DataClean.Contracts.Interfaces;

namespace DataClean.Models
{
    public class DataCleanEvent  : IDataCleanEvent
    {
        private DateTime _dataCleanDate;

        public DataCleanEvent()
        {
            DataCleanDate = DateTime.MinValue;
        }
        public DateTime DataCleanDate
        {
            get { return _dataCleanDate; }
            set
            {
                _dataCleanDate = value;
            }
        }
        public string RecordID
        {
            get
            {
                if (Input == null) return string.Empty;
                return Input.RecordID;
            }
        }
        public int ID
        {
            get
            {
                if (Input == null) return 0;
                return Input.ID;
            }
        }
 
        public bool HasBeenDataCleaned
        {
            get { return DataCleanDate != DateTime.MinValue; }
        }

        public IInputStreetAddress Input { get; set; }
        public IOutputStreetAddress Output { get; set; }
    
    }
}