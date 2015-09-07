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
                Console.WriteLine("{0} {1} {2}", v.job_num, v.batch_num, v.total_amount);

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
