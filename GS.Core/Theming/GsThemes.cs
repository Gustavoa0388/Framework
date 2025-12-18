using System.Drawing;

namespace GS.Core.UI.Theming
{
    public static class GsThemes
    {
        public static GsTheme Light = new GsTheme
        {
            BackColor = Color.White,
            ForeColor = Color.Black,
            Primary = Color.FromArgb(0, 120, 215),
            Secondary = Color.Gray,
            InputBack = Color.White,
            Border = Color.Silver,
            DefaultFont = SystemFonts.DefaultFont
        };

        public static GsTheme Dark = new GsTheme
        {
            BackColor = Color.FromArgb(32, 32, 32),
            ForeColor = Color.White,
            Primary = Color.FromArgb(0, 153, 255),
            Secondary = Color.DarkGray,
            InputBack = Color.FromArgb(45, 45, 45),
            Border = Color.FromArgb(70, 70, 70),
            DefaultFont = SystemFonts.DefaultFont
        };
    }
}
