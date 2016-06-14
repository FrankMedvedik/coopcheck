using System;

namespace DataClean.Repository.Contracts.Interfaces
{
    public interface IMelissaResultReference
    {
        string Code { get; set; }
        string Type { get; set; }
        string LongDescription { get; set; }
        string ShortDescription { get; set; }
        Nullable<bool> AlternateAddressExists { get; set; }
        Nullable<System.DateTime> updated { get; set; }
    }
}