using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopCheck.Mocks
{
    public class BatchSwiftFulfillCommand
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void BatchSwiftFulfill(int batchNum , bool pass)
        {
                log.Info(String.Format("SwiftPay called user {0}  batch {1}", Environment.UserName, batchNum));
                System.Threading.Thread.Sleep(30000);
                if (!pass) throw new Exception("Swiftpay failed");
                log.Info(String.Format("SwiftPay completed {0}  batch {1} completed ok {2} ", Environment.UserName,
                    batchNum, pass));
            
        }
    }
}
