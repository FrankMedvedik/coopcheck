using System;
using System.Collections.Generic;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;

namespace CoopCheck.Library
{
    [Serializable]
    public class ClearCheckBatchCommand : CommandBase<ClearCheckBatchCommand>
    {
        //private int _tranId;
        //private DateTime _clearedDate;
        //private decimal _clearedAmount;
        //private List<CheckInfoDto> _checkBatch;

        #region Authorization
        public static bool CanExecuteCommand()
        {
#if DEBUG
            var a = Csla.ApplicationContext.User.IsInRole("RECKNER\\CoopCheckAdmin");
            return a;
#endif
  //          return true;
        }
        #endregion

        #region Factory Methods
        //public static void Execute(List<CheckInfoDto> checkBatch)
        public static void Execute()
        {
            if (!CanExecuteCommand()) throw new System.Security.SecurityException("Not authorized to execute clear check batch command.");
            DataPortal.Execute<ClearCheckBatchCommand>(new ClearCheckBatchCommand());
        }
        private ClearCheckBatchCommand()
        {
            //_checkBatch = checkBatch;
        }
#endregion
#region Server-side Code
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.ClearCheckBatch();
            }
        }
#endregion
    }
}