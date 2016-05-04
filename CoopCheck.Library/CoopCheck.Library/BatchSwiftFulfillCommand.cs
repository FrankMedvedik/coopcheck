using System;

using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{
    [Serializable]
    public class BatchSwiftFulfillCommand : CommandBase<BatchSwiftFulfillCommand>
    {
        private int _batchNum;


#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
#else
        protected static void AddObjectAuthorizationRules()
#endif
        {
#if !DEBUG        
            BusinessRules.AddRule(typeof(BatchSwiftFulfillCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
#endif            
        }

        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(BatchSwiftFulfillCommand));
        }

        public static void Execute(int batchNum)
        {
            BatchSwiftFulfillCommand result = null;
            result = DataPortal.Execute<BatchSwiftFulfillCommand>(new BatchSwiftFulfillCommand(batchNum));
        }
        private BatchSwiftFulfillCommand(int batchNum)
        {
            _batchNum = batchNum;
        }
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.FulfillSwift(_batchNum);
            }
        }

    }

}
