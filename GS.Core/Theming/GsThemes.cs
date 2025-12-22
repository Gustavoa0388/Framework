using System.Drawing;

namespace GS.Core.UI.Theming
{
    public static class GsThemes
    {
        public static readonly GsTheme Light = new GsTheme
        {
            // Base
            BackColor = Color.White,
            ForeColor = Color.Black,
            DefaultFont = SystemFonts.MessageBoxFont,

            // Branding
            Primary = Color.FromArgb(130, 40, 140),
            Secondary = Color.FromArgb(220, 170, 230),
            Border = Color.Gainsboro,
            Error = Color.Firebrick,

            // Textos
            TextPrimary = Color.Black,
            TextSecondary = Color.DimGray,
            TextPlaceholder = Color.Gray,

            // Inputs
            InputBackground = Color.White,
            InputBorder = Color.Gainsboro,
            InputHover = Color.FromArgb(240, 240, 240),
            InputFocus = Color.FromArgb(130, 40, 140),
            InputError = Color.Firebrick,

            // GRID ⭐
            GridBackground = Color.White,
            GridSurface = Color.White,
            GridSurfaceAlt = Color.FromArgb(245, 245, 245),
            GridHeaderBackground = Color.FromArgb(240, 240, 240),
            GridHeaderText = Color.Black,
            GridRowSelected = Color.FromArgb(130, 40, 140),
            GridRowSelectedText = Color.White,
            GridBorder = Color.Gainsboro,

            IsDark = false
        };

        public static readonly GsTheme Dark = new GsTheme
        {
            // Base
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.White,
            DefaultFont = SystemFonts.MessageBoxFont,

            // Branding
            Primary = Color.FromArgb(180, 90, 200),
            Secondary = Color.FromArgb(110, 60, 120),
            Border = Color.FromArgb(60, 60, 60),
            Error = Color.IndianRed,

            // Textos
            TextPrimary = Color.White,
            TextSecondary = Color.LightGray,
            TextPlaceholder = Color.Gray,

            // Inputs
            InputBackground = Color.FromArgb(45, 45, 45),
            InputBorder = Color.FromArgb(70, 70, 70),
            InputHover = Color.FromArgb(60, 60, 60),
            InputFocus = Color.FromArgb(180, 90, 200),
            InputError = Color.IndianRed,

            // GRID ⭐
            GridBackground = Color.FromArgb(30, 30, 30),
            GridSurface = Color.FromArgb(35, 35, 35),
            GridSurfaceAlt = Color.FromArgb(45, 45, 45),
            GridHeaderBackground = Color.FromArgb(50, 50, 50),
            GridHeaderText = Color.White,
            GridRowSelected = Color.FromArgb(180, 90, 200),
            GridRowSelectedText = Color.White,
            GridBorder = Color.FromArgb(70, 70, 70),

            IsDark = true
        };
    }
}
