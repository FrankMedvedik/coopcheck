using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{
    [Serializable]
    public class VoidCheckCommand : CommandBase<ClearCheckCommand>
    {


        
private int _tranId;  

        #region Authorization
        public static bool CanExecuteCommand()
        {
//#if !DEBUG
//            return Csla.ApplicationContext.User.IsInRole("RECKNER\\CoopCheckAdmin");
//#endif
            return true;
        }
#endregion

#region Factory Methods
        public static void Execute(int tranId)
        {
            if (!CanExecuteCommand()) throw new System.Security.SecurityException("Not authorized to execute void check command.");
            DataPortal.Execute<VoidCheckCommand>(new VoidCheckCommand(tranId));
        }
        private VoidCheckCommand(int tranId)
        {
            _tranId = tranId;
        }
#endregion
#region Server-side Code
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.VoidCheck(_tranId);
            }
        }
#endregion
    }
}