using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{
    public class PayVouchersViewModel : ViewModelBase
    {
        private ObservableCollection<OpenBatch> _openBatches;

        public ObservableCollection<OpenBatch> OpenBatches
        {
            get { return _openBatches; }
            set
            {
                _openBatches = value;
                NotifyPropertyChanged();
            }
        }

        private OpenBatch _selectedBatch = new OpenBatch();
        public OpenBatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<bank_account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyPropertyChanged();
            }
        }

        private bank_account _selectedAccount = new bank_account();
        public bank_account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<PaymentMethod> _paymentMethods;
        public ObservableCollection<PaymentMethod> PaymentMethods
        {
            get { return _paymentMethods; }
            set
            {
                _paymentMethods = value;
                NotifyPropertyChanged();
            }
        }

        private PaymentMethod _selectedPaymentMethod = new PaymentMethod();
        public PaymentMethod SelectedPaymentMethod
        {
            get { return _selectedPaymentMethod; }
            set
            {
                _selectedPaymentMethod = value;
                ShowCheckInfo = (SelectedPaymentMethod.Key == "Check"); 
                NotifyPropertyChanged();

            }
        }


        //public ICommand PayCommand
        //{
        //    get
        //    {
        //        if (_cmdPay == null)
        //            _cmdPay = new 
        //    return _cmdPay;
        //    }
        //}

        public PayVouchersViewModel()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "click select file to choose a new excel file to import voucher from",
                ErrorMessage = "No Errors"
            };

            Status = s;
            //Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
            //Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            //{
            //    Status = message.Content;
            //});

            StartOver();
        }

        public int StartingCheckNum
        {
            get { return _startingCheckNum; }
            set
            {
                _startingCheckNum = value;
                NotifyPropertyChanged();
            }
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.ImportCannotProceed)
            {
                CanProceed = false;
            }
            if (message.Notification == Notifications.ImportCanProceed)
            {
                CanProceed = true;
            }
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowCheckInfo
        {
            get { return _showCheckInfo; }
            set
            {
                _showCheckInfo = value;
                NotifyPropertyChanged();
            }
        }


        public bool ShowSwiftpayInfo
        {
            get { return _showSwiftpay; }
            set
            {
                _showSwiftpay = value;
                NotifyPropertyChanged();
            }
        }
        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value; 
                NotifyPropertyChanged();
            }
        }

        private StatusInfo _status;
        
        private bool _canProceed;
        private int _selectedBatchNum;
        private int _startingCheckNum;
        private ObservableCollection<bank_account> _accounts;
        private bool _showCheckInfo;
        private ICommand _cmdPay;
        private bool _showSwiftpay;

        private bool _isRefreshing;
        private RelayCommand _refreshCommand;

        //public RelayCommand RefreshCommand
        //{
        //    get
        //    {
        //        return _refreshCommand
        //          ?? (_refreshCommand = new RelayCommand(
        //            async () =>
        //            {
        //                if (_isRefreshing)
        //                {
        //                    return;
        //                }

        //                _isRefreshing = true;
        //                RefreshCommand.RaiseCanExecuteChanged();

        //                await Refresh();

        //                _isRefreshing = false;
        //                RefreshCommand.RaiseCanExecuteChanged();
        //            },
        //            () => !_isRefreshing));
        //    }
        //}
        //public ICommand AccountChangedCommand
        //{
        //    get { return _currentProcessingStep; }
        //    set
        //    {
        //        _currentProcessingStep = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public void GoToNextStep()
        //{
        //    CurrentProcessingStep++;
        //}

        //public void GoBackAStep()
        //{
        //    CurrentProcessingStep--;
        //}

        public async void StartOver()
        {
            Accounts = new ObservableCollection<bank_account>(await  BankAccountSvc.GetAccounts());
            OpenBatches = new ObservableCollection<OpenBatch>(await  OpenBatchSvc.GetOpenBatches());
            PaymentMethods = new ObservableCollection<PaymentMethod>( PaymentMethodSvc.GetMethods());
        }


        public void PrintChecks()
        {
            var app = new Microsoft.Office.Interop.Word.Application();
            var doc = new Microsoft.Office.Interop.Word.Document();
            doc = app.Documents.Add(Template: @"D:\githublocal\coopcheck\CoopCheck2016\muiCoopCheck\RecknerCheck.dotx");
            app.Visible = true;
            int checkNum = StartingCheckNum;
            var ctx = new CoopCheckEntities();
            var b = BatchEdit.GetBatchEdit(SelectedBatch.batch_num);
            foreach (var c in b.Vouchers)
            {
                foreach (Microsoft.Office.Interop.Word.Field f in doc.Fields)
                {
                    if(f.Code.Text.Contains("thank_you_1"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.ThankYou1 ?? "");
                   
                    }
                    else if (f.Code.Text.Contains("thank_you_2"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.ThankYou2 ?? "");
                    }

                    else if (f.Code.Text.Contains("marketing_research_message"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.MarketingResearchMessage ?? "");
                    }


                    else if (f.Code.Text.Contains("job_num"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.JobNum.ToString());
                        
                    }

                    else if (f.Code.Text.Contains("batch_num"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.Num.ToString());
                    }

                    else if (f.Code.Text.Contains("check_date"))
                    {
                        f.Select();
                        app.Selection.TypeText(DateTime.Now.ToString("MMMM dd, yyyy"));
                    }

                    else if (f.Code.Text.Contains("check_num"))
                    {
                        f.Select();
                        
                        app.Selection.TypeText(checkNum.ToString());
                    }
                    else if (f.Code.Text.Contains("tran_amount"))
                    {
                        f.Select();

                        app.Selection.TypeText(c.Amount.GetValueOrDefault().ToString());
                    }

                    else if (f.Code.Text.Contains("tran_amount"))
                    {
                        f.Select();
                        app.Selection.TypeText(c.Amount.GetValueOrDefault().ToString());
                    }

                    else if (f.Code.Text.Contains("tran_amount_text"))
                    {
                        f.Select();
                        app.Selection.TypeText(NumberConverter.NumberToCurrencyText(c.Amount.GetValueOrDefault(0),MidpointRounding.AwayFromZero));
                    }

                    else if (f.Code.Text.Contains("full_name"))
                    {
                        f.Select();
                        app.Selection.TypeText(Payee(c.Company,c.FullName));
                    }
                    else if (f.Code.Text.Contains("address_1"))
                    {
                        f.Select();
                        app.Selection.TypeText(c.AddressLine1);
                    }
                    else if (f.Code.Text.Contains("address_2"))
                    {
                        f.Select();
                        if(c.AddressLine2.Length != 0)
                            app.Selection.TypeText(c.AddressLine2 ?? "");
                        

                    }
                    else if (f.Code.Text.Contains("address_3"))
                    {
                        f.Select();
                        app.Selection.TypeText(String.Join(" ", c.Municipality, c.Region, c.PostalCode, c.Country));
                    }
                }
                checkNum++;
                doc.PrintOut();
                doc.SaveAs2(FileName: @"c:\temp\checks." + checkNum.ToString() + ".doc");

            }
            doc.Close();
            app.Quit();
        }

        public string Payee(string company, string fulllName)
        {
            if (company.Length != 0) return company;
            return fulllName;
        }

        public void Pay()
        {
            throw new NotImplementedException();
        }
    }
}