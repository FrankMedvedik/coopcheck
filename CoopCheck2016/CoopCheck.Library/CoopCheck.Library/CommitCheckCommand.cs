using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;
using System.Collections.Generic;

namespace CoopCheck.Library
{
    [Serializable]
    public class CommitCheckCommand : CommandBase<CommitCheckCommand>
    {
        private int _batchNum;
        private int _lastCheckNum;

        #region Authorization
        public static bool CanExecuteCommand()
        {
#if !DEBUG

            return Csla.ApplicationContext.User.IsInRole("RECKNER\\CoopCheckAdmin");
#else
            return true;
#endif
        }
#endregion
#region Factory Methods
        public static void Execute(int batchNum, int lastCheckNum)
        {
            if (!CanExecuteCommand()) throw new System.Security.SecurityException("Not authorized to execute command.");

            DataPortal.Execute<CommitCheckCommand>(new CommitCheckCommand(batchNum, lastCheckNum));
        }
        private CommitCheckCommand(int batchNum, int lastCheckNum)
        {
            _batchNum = batchNum;
            _lastCheckNum = lastCheckNum;
        }
#endregion

#region Server-side Code
        protected override void DataPortal_Execute()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                dal.CommitChecks(_batchNum, _lastCheckNum);
            }
        }
#endregion
    }
}
