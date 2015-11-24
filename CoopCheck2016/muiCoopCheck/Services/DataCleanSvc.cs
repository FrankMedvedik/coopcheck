using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using DataClean;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanSvc
    {

        public static async Task<List<OutputStreetAddress>> ValidateAddresses(List<InputStreetAddress> newVouchers)
        {
            var outRows = await Task<OutputStreetAddress[]>.Factory.StartNew(() =>
            {
                    var mc = GetMelissaReference();
                    var c = ConfigurationManager.AppSettings;
                    var d = new DataCleaner(c, mc);
                    var v = d.VerifyAndCleanAddress(newVouchers.ToArray());
                    return v;
            });
            return outRows.ToList();
        }

        public static List<ParseResult> GetMelissaReference()
        {
            var r = new List<ParseResult>();
            using (var ctx = new CoopCheckEntities())
            {
                foreach (var p in  ctx.MelissaResultReferences)
                {
                    r.Add(new ParseResult()
                    {
                        Code = p.Code.Trim(),
                        AlternateAddressExists = p.AlternateAddressExists.GetValueOrDefault(false),
                        LongDescription = p.LongDescription,
                        ShortDescription = p.ShortDescription,
                        Type = p.Type
                    });
                }
            }
            return r;
            }
        }
}