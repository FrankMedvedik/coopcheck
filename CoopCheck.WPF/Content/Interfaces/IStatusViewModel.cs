using System.Windows;
using CoopCheck.Domain.Contracts.Models;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IStatusViewModel
    {
        Visibility ShowError { get; }
        StatusInfo Status { get; set; }
    }
}