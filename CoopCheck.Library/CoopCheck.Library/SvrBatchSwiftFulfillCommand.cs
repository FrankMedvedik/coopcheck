using System;

using Csla;
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
    }

        public static bool CanExecuteObject()
        {
            return true;
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
