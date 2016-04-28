using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CoopCheck.DAL;
using System.Collections.Generic;

namespace CoopCheck.Library
{
    [Serializable]
    public class NextCheckNumCommand : CommandBase<NextCheckNumCommand>
    {
             
        public static bool CanExecuteCommand()
        {
            return Csla.ApplicationContext.User.IsInRole("RECKNER\\CoopCheckAdmin");
        }


        private NextCheckNumCommand(int accoundId)
        {
            AccountId = accoundId;
        }

        public static PropertyInfo<int> AccountIdProperty = RegisterProperty<int>(c => c.AccountId);
        public int AccountId
        {
            get { return ReadProperty(AccountIdProperty); }
            private set { LoadProperty(AccountIdProperty, value); }
        }

        public static PropertyInfo<int> NextCheckNumProperty = RegisterProperty<int>(c => c.NextCheckNum);
        public int NextCheckNum
        {
            get { return ReadProperty(NextCheckNumProperty); }
            private set { LoadProperty(NextCheckNumProperty, value); }
        }

        public static int Execute(int accountId)
        {
#if Release
            if (!CanExecuteCommand()) throw new System.Security.SecurityException("Not authorized to execute command.");
#endif
            var cmd = new NextCheckNumCommand(accountId);
            var retVal = DataPortal.Execute<NextCheckNumCommand>(cmd).NextCheckNum;
            return retVal;
        }
#if !SILVERLIGHT
        protected override void DataPortal_Execute()
        {

            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                NextCheckNum = dal.NextCheckNum(AccountId);
            }
        }
#endif
    }
}
