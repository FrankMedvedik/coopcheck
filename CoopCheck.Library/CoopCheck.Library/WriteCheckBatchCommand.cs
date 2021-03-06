using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;
using System.Collections.Generic;

namespace CoopCheck.Library
{
    [Serializable]
    public class WriteCheckBatchCommand : ReadOnlyBase<WriteCheckBatchCommand>
    {
        #region properties
        public static readonly PropertyInfo<PaymentInfoList> PaymentInfoListProperty = RegisterProperty<PaymentInfoList>(c => c.List);
        public PaymentInfoList List
        {
            get { return GetProperty(PaymentInfoListProperty); }
            private set { LoadProperty(PaymentInfoListProperty, value); }
        }

        #endregion

        #region business rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
#if!Debug
            // TODO: add business rules
            BusinessRules.AddRule(typeof(WriteCheckBatchCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
#endif
        }
        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(WriteCheckBatchCommand));
        }

        #endregion
        #region Factory Methods
#if !SILVERLIGHT
        public static WriteCheckBatchCommand Execute(int batchNum, int accountId, int firstCheckNum)
        {

            return DataPortal.Fetch<WriteCheckBatchCommand>(new Criteria() { BatchNum = batchNum, AccountId = accountId, FirstCheckNum = firstCheckNum });
        }
#endif

        public static void Execute(int batchNum, int accountId, int firstCheckNum, EventHandler<DataPortalResult<WriteCheckBatchCommand>> callback)
        {
            DataPortal.BeginFetch<WriteCheckBatchCommand>(new Criteria() { BatchNum = batchNum, AccountId = accountId, FirstCheckNum = firstCheckNum }, callback);
        }
        #endregion

        #region Client-side Code

        public static readonly PropertyInfo<PaymentInfoList> ResultProperty = RegisterProperty<PaymentInfoList>(p => p.Result);
        public PaymentInfoList Result
        {
            get { return ReadProperty(ResultProperty); }
            private set { LoadProperty(ResultProperty, value); }
        }

        private void BeforeServer()
        {
            // TODO: implement code to run on client
            // before server is called
        }

        private void AfterServer()
        {
            // TODO: implement code to run on client
            // after server is called
        }

        #endregion

        #region Server-side Code
        [Serializable()]
        protected class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> BatchNumProperty = RegisterProperty<int>(c => c.BatchNum);
            public int BatchNum
            {
                get { return ReadProperty(BatchNumProperty); }
                set { LoadProperty(BatchNumProperty, value); }
            }
            public static readonly PropertyInfo<int> AccountIdProperty = RegisterProperty<int>(c => c.AccountId);
            public int AccountId
            {
                get { return ReadProperty(AccountIdProperty); }
                set { LoadProperty(AccountIdProperty, value); }
            }
            public static readonly PropertyInfo<int> firstCheckNumProperty = RegisterProperty<int>(c => c.FirstCheckNum);
            public int FirstCheckNum
            {
                get { return ReadProperty(firstCheckNumProperty); }
                set { LoadProperty(firstCheckNumProperty, value); }
            }

        }
#if !SILVERLIGHT
        private void DataPortal_Fetch(Criteria crit)
        {
            using (var dalManager = DAL.DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                List<PaymentInfoDto> data = dal.WriteCheckBatch(crit.BatchNum, crit.AccountId, crit.FirstCheckNum);
                List = PaymentInfoList.LoadPaymentInfoList(data);
            }
        }
#endif
        #endregion
    }
}