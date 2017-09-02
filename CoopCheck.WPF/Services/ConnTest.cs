using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoopCheck.WPF.Content.Payment.Summary;
using FirstFloor.ModernUI.Windows.Controls;
using log4net;
using Reckner.WPF.Services;

namespace CoopCheck.WPF.Services
{
    class ConnTest
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool ValidateAllServices()
        {
            bool retval = false;
            string msg = string.Empty;
            try
            {
                if (!BatchSvc.CheckConnection() || !RptSvc.CheckConnection())
                    msg = "Cannot reach database.";
                //if(!EmailSvc.CheckConnection())
                //    msg += "\nCannot connect to email server.";
                //if (UserAuthSvc.CheckConnection())
                //    msg += "\nCannot validate your access.";
                //if (DataCleanVoucherImportSvc.CheckConnection())
                //    msg += "\nCannot obtain online payment and address validator services)";
                else
                {
                    msg = ConnTest.AllServicesOk;
                    retval = true;  
                }
                if(!retval)
                    ModernDialog.ShowMessage(msg, "No network connection to Reckner found", MessageBoxButton.OK);
                log.Error(msg);
                return retval;
            }
            catch (Exception ex)
            {

                msg += " " + ex.Message;
                ModernDialog.ShowMessage(msg, "there is a problem", MessageBoxButton.OK);
                log.Error(msg);
                return false;

            }
        }

        public const string AllServicesOk = "All services available";
    }
}
