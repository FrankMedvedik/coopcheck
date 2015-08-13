using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BusinessRules.AddRule(typeof(BatchSwiftFulfillCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
        }

        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(BatchSwiftFulfillCommand));
        }

        public static void BatchSwiftFulfill(int batchNum)
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

    [Serializable]
    public class BatchSwiftVoidCommand : CommandBase<BatchSwiftVoidCommand>
    {
        private int _batchNum;

#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
#else
        protected static void AddObjectAuthorizationRules()
#endif
        {
            BusinessRules.AddRule(typeof(BatchSwiftVoidCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
        }


        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(BatchSwiftVoidCommand));
        }

        public static void BatchSwiftVoid(int batchNum)
        {
            BatchSwiftVoidCommand result = null;
            result = DataPortal.Execute<BatchSwiftVoidCommand>(new BatchSwiftVoidCommand(batchNum));
        }
        private BatchSwiftVoidCommand(int batchNum)
        {
            _batchNum = batchNum;
        }
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.VoidSwiftBatch(_batchNum);
            }
        }
    }

}
