using System.Windows;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Status
{
    public interface IStatusViewModel
    {
        Visibility ShowError { get; }
        StatusInfo Status { get; set; }
    }
}