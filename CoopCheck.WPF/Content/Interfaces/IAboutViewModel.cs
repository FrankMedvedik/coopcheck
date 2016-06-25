using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IAboutViewModel
    {
        string PublishedVersion { get; }
        string UserName { get; set; }
        ObservableCollection<string> UserRights { get; set; }
    }
}