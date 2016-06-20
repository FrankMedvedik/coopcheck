using System.Windows;
using CoopCheck.Domain.Models;

namespace CoopCheck.WPF.Content.Status
{
    public interface IStatusViewModel
    {
        Visibility ShowError { get; }
        StatusInfo Status { get; set; }
    }
}