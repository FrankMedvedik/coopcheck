//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DataClean.Repository.Contracts.Interfaces;

namespace DataClean.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class DataCleanEventLog : IDataCleanEventLog
    {
        public int ID { get; set; }
        public System.DateTime DataCleanDate { get; set; }
        public string DataCleanEventJson { get; set; }
    }
}