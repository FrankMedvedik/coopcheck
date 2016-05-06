using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{

    [Serializable]
    public class VoidSwiftPromoCodeCommand : CommandBase<VoidSwiftPromoCodeCommand>
    {
        private int _tranId;

#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
#else
        protected static void AddObjectAuthorizationRules()
#endif
        {
#if !DEBUG
            BusinessRules.AddRule(typeof(VoidSwiftPromoCodeCommand), new IsInRole(AuthorizationActions.ExecuteMethod, "RECKNER\\CoopCheckAdmin"));
#endif
        }


        public static bool CanExecuteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.ExecuteMethod, typeof(VoidSwiftPromoCodeCommand));
        }

        public static void Execute(int tranId)
        {
            VoidSwiftPromoCodeCommand result = null;
            result = DataPortal.Execute<VoidSwiftPromoCodeCommand>(new VoidSwiftPromoCodeCommand(tranId));
        }
        private VoidSwiftPromoCodeCommand(int tranId)
        {
            _tranId = tranId;    }
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.VoidSwiftPromoCode(_tranId);
            }
        }
    }

}
