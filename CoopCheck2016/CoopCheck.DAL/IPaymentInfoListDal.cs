using System;
using System.Collections.Generic;
using Csla;

namespace CoopCheck.DAL
{
    public interface IPaymentInfoListDal
    {
        List<PaymentInfoDto> FetchBatch(int batchNum);
        List<PaymentInfoDto> FetchJob(int jobNum);
        List<PaymentInfoDto> FetchPerson(string personId);
        List<PaymentInfoDto> FetchById(int tranId);
        List<PaymentInfoDto> FetchByNum(string paymentNumber);

        void FulfillSwift(int batch_num);
        void VoidSwiftBatch(int batch_num);
        void VoidSwiftCode(int tran_id);

        List<PaymentInfoDto> WriteChecks(int batchNum, int accountId, int firstCheckNum);
        void CommitChecks(int batchNum, int lastCheckNum);
        void ClearCheck(int tranId, DateTime clearedDate, decimal clearedAmount);
        int NextCheckNum(int accoundId);
    }
}
