using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class VoucherImportEditView : UserControl
    {
        public VoucherImportEditView()
        {
            InitializeComponent();
        }


        public void RefreshLookups()
        {
            rvAddressLine1.SetState(null, null);
            rvAddressLine2.SetState(null, null);
            rvMunicipality.SetState(null, null);
            rvRegion.SetState(null, null);
            rvPostalCode.SetState(null, null);
        }
    }
}