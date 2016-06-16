using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

namespace CoopCheck.WPF.Content.Settings
{
    public interface ISettingsAppearanceViewModel
    {
        Color[] AccentColors { get; }
        string[] FontSizes { get; }
        string[] Palettes { get; }
        Color SelectedAccentColor { get; set; }
        string SelectedFontSize { get; set; }
        string SelectedPalette { get; set; }
        Link SelectedTheme { get; set; }
        LinkCollection Themes { get; }

        void SaveSettings();
    }
}