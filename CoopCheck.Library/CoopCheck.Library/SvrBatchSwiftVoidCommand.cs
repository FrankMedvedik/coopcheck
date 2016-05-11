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
    public class SvrBatchSwiftVoidCommand : CommandBase<SvrBatchSwiftVoidCommand>
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
            BusinessRules.AddRule(typeof(SvrBatchSwiftVoidCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
        #endif
        }


        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(SvrBatchSwiftVoidCommand));
        }

        public static void Execute(int batchNum, string email)
        {
            SvrBatchSwiftVoidCommand result = null;
            result = DataPortal.Execute<SvrBatchSwiftVoidCommand>(new SvrBatchSwiftVoidCommand(batchNum, email));
        }
        private SvrBatchSwiftVoidCommand(int batchNum, string email)
        {
            _batchNum = batchNum;
            _email = email;
        }
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.SvrVoidSwiftBatch(_batchNum, _email);
            }
        }
    }

}
