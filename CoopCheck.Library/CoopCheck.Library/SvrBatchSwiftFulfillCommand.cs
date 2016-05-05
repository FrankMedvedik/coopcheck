using System;

using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{
    [Serializable]
    public class SvrBatchSwiftFulfillCommand : CommandBase<SvrBatchSwiftFulfillCommand>
    {
        private int _batchNum;
        private string _email;


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

        public static void Execute(int batchNum, string email)
        {
            SvrBatchSwiftFulfillCommand result = null;
            result = DataPortal.Execute<SvrBatchSwiftFulfillCommand>(new SvrBatchSwiftFulfillCommand(batchNum, email));
        }
        private SvrBatchSwiftFulfillCommand(int batchNum, string email)
        {
            _batchNum = batchNum;
            _email = email;
        }
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.SvrFulfillSwift(_batchNum, _email);
            }
        }

    }

}
