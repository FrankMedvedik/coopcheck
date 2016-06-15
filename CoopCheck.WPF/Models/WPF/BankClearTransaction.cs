﻿using System;
using CoopCheck.WPF.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class BankClearTransaction : IBankClearTransaction
    {
            public string AccountDesignator { get; set; }
            public DateTime PostDate { get; set; }
            public string SerialNumber { get; set; }
            public string Description { get; set; }
            public decimal RawAmount { get; set; }
            public string CRDR { get; set; }

        public decimal Amount
        {
            get { return (CRDR == "CR") ? RawAmount * -1 : RawAmount; }
        }

    }
}