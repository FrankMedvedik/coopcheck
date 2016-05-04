using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{
    [Serializable]
    public class ClearCheckCommand : CommandBase<ClearCheckCommand>
    {
        private int _tranId;
        private DateTime _clearedDate;
        private decimal _clearedAmount;

        #region Authorization
        public static bool CanExecuteCommand()
        {
#if !DEBUG
            return Csla.ApplicationContext.User.IsInRole("RECKNER\\CoopCheckAdmin");
#endif
            return true;
        }
#endregion

#region Factory Methods
        public static void Execute(int tranId, DateTime clearedDate, decimal clearedAmount)
        {
            if (!CanExecuteCommand()) throw new System.Security.SecurityException("Not authorized to execute clear check command.");
            DataPortal.Execute<ClearCheckCommand>(new ClearCheckCommand(tranId, clearedDate, clearedAmount));
        }
        private ClearCheckCommand(int tranId, DateTime clearedDate, decimal clearedAmount)
        {
            _tranId = tranId;
            _clearedDate = clearedDate;
            _clearedAmount = clearedAmount;
        }
#endregion
#region Server-side Code
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.ClearCheck(_tranId, _clearedDate, _clearedAmount);
            }
        }
#endregion
    }
}