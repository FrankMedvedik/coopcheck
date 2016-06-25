using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

namespace CoopCheck.WPF.Content.Interfaces
{
    public interface IAppearanceViewModel
    {
        Color[] AccentColors { get; }
        string[] FontSizes { get; }
        Color SelectedAccentColor { get; set; }
        string SelectedFontSize { get; set; }
        Link SelectedTheme { get; set; }
        LinkCollection Themes { get; }

        void SaveSettings();
    }
}