using System;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Services
{
    public static class BatchSvc
    {
        public static Task<BatchEdit> GetBatchEditAsync(int batchNum)
        {

            return Task<BatchEdit>.Factory.StartNew(() => BatchEdit.GetBatchEdit(batchNum));

        }

        public static Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }
    }
}
