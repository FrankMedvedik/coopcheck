using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using CoopCheck.Repository;
using System.Linq;
using DataClean;

namespace CoopCheck.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CoopCheckEntities();
            var p = new ParseResultDictionary();
            Console.WriteLine("Load Codes");
            int totalCnt= 0;
            int insCnt = 0;
            foreach (var v in p.ToList())
            {
                totalCnt += 1;
                try
                {
                    var z = new MelissaResultReference()
                    {
                        Code = v.Value.Code,
                        Type = v.Value.Type,
                        LongDescription = v.Value.LongDescription,
                        ShortDescription = v.Value.ShortDescription
                    };
                    ctx.MelissaResultReferences.Add(z);

                    ctx.SaveChanges();
                    insCnt += 1;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} {1} {2}", v.Value.Code, v.Value.Type, v.Value.LongDescription);
                    Console.WriteLine(" Error {0} ", e.Message + " || "  + ((e.InnerException != null) ? e.InnerException.Message : ""));
                }

            }

            Console.WriteLine(" !!Done!! ");
            Console.WriteLine(" {0} Rows Read {1} Rows written ", totalCnt, insCnt);
            Console.ReadKey();
            //Console.WriteLine("CHECKS");

            //foreach (var v in ctx.vwPayments.Take(100).ToList())
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", v.tran_id, v.job_num, v.batch_num, v.check_num);
                
            //};
            //Console.ReadKey();

            //Console.WriteLine("Bank Accounts");

            //foreach (var v in ctx.bank_accounts.Take(100).ToList())
            //{
            //    Console.WriteLine("{0} {1}  ", v.account_id, v.account_name);
            //};
            //Console.ReadKey();
        }
    }
}
