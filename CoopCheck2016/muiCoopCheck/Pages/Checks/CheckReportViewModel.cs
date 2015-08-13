using System;
using System.Collections.ObjectModel;
using CoopCheck.Library;
using GalaSoft.MvvmLight;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Checks
{
    ///// <summary>
    ///// This class contains properties that a View can data bind to.
    ///// <para>
    ///// See http://www.galasoft.ch/mvvm
    ///// </para>
    ///// </summary>
    //public class CheckReportViewModel : ViewModelBase
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the CheckReportViewModel class.
    //    /// </summary>
    //    public CheckReportViewModel()
    //    {
    //       ResetState();
    //        StatusMsg = "";
    //    }

    //    private ObservableCollection<CheckInfo> _checks = new ObservableCollection<CheckInfo>();
    //    public ObservableCollection<CheckInfo> Checks  {             
    //        get { return _checks ; }
    //        set
    //        {
    //            _checks = value;
    //            NotifyPropertyChanged();
    //        }
    //    }


    //    #region DisplayState
    //    private bool _canProceed;
    //    private string _errorMsg;
    //    private string _statusMsg;

    //    private void ResetState()
    //    {
    //        StatusMsg = "";
    //        ErrorMsg = "";
    //        CanProceed = false;
    //        ShowGridData = false;
    //    }
    //    public string StatusMsg
    //    {
    //        get { return _statusMsg; }
    //        set
    //        {
    //            _statusMsg = value;
    //            NotifyPropertyChanged();
    //        }
    //    }
    //    public bool CanProceed
    //    {
    //        get { return _canProceed; }
    //        set{
    //            _canProceed = value;
    //            NotifyPropertyChanged();
    //        }

    //    }

    //    public string ErrorMsg
    //    {
    //        get { return _errorMsg; }
    //        set
    //        {
    //            _errorMsg = value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    private Boolean _showGridData;

    //    public Boolean ShowGridData
    //    {
    //        get { return _showGridData; }
    //        set
    //        {
    //            _showGridData = value;
    //            NotifyPropertyChanged();
    //        }
    //    }


    //    #endregion

    //    #region ReportCriteria

    //    private CheckReportOptions _selectedBatch;
    //    public BatchEditImport SelectedBatch
    //    {
    //        get { return _selectedBatch; }
    //        set
    //        {
    //            _selectedBatch = value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    #endregion

    //}
    
}