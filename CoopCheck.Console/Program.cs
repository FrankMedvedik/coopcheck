using System;
using CoopCheck.Repository;
using System.Linq;

namespace CoopCheck.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CoopCheckEntities();
            Console.WriteLine("Batches");
            foreach (var v in ctx.vwBatchRpts.Take(100).ToList())
            {
                Console.WriteLine("{0} {1} {2}", NumberConverter.NumberToCurrencyText(v.job_num.GetValueOrDefault(0)), NumberConverter.NumberToCurrencyText(v.batch_num), NumberConverter.NumberToCurrencyText(v.total_amount.GetValueOrDefault(0)));

            };
            Console.ReadKey();
            Console.WriteLine("CHECKS");

            foreach (var v in ctx.vwPayments.Take(100).ToList())
            {
                Console.WriteLine("{0} {1} {2} {3}", v.tran_id, v.job_num, v.batch_num, v.check_num);
                
            };
            Console.ReadKey();

            Console.WriteLine("Bank Accounts");

            foreach (var v in ctx.bank_accounts.Take(100).ToList())
            {
                Console.WriteLine("{0} {1}  ", v.account_id, v.account_name);
            };
            Console.ReadKey();
        }
    }
}
