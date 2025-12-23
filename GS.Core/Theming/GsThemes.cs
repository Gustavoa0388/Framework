using System.Drawing;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Temas oficiais do GS Core UI.
    /// </summary>
    public static class GsThemes
    {
        // ==========================================================
        // LIGHT THEME — PADRÃO GAS
        // ==========================================================
        public static GsTheme Light => new GsTheme
        {
            // Identidade
            Primary = Color.FromArgb(30, 90, 168),
            PrimaryDark = Color.FromArgb(22, 63, 115),
            PrimaryLight = Color.FromArgb(58, 123, 213),
            Secondary = Color.FromArgb(123, 63, 228),

            // Superfícies
            Surface = Color.White,
            SurfaceAlt = Color.FromArgb(243, 246, 250),
            Border = Color.FromArgb(208, 215, 226),

            // Texto
            TextPrimary = Color.FromArgb(26, 26, 26),
            TextSecondary = Color.FromArgb(95, 107, 122),
            TextOnPrimary = Color.White,

            // Títulos
            TitleFont = new Font("Segoe UI", 18F, FontStyle.Bold),
            SubtitleFont = new Font("Segoe UI", 14F, FontStyle.Bold),
            SectionFont = new Font("Segoe UI", 12F, FontStyle.Bold),

            TitleText = Color.White,                  // TextPrimary
            SubtitleText = Color.FromArgb(180, 180, 180),  // TextSecondary
            SectionText = Color.FromArgb(180, 180, 180),  // TextSecondary

            // Inputs
            InputBackground = Color.White,
            InputBorder = Color.FromArgb(208, 215, 226),
            InputHover = Color.FromArgb(230, 236, 245),
            InputFocus = Color.FromArgb(58, 123, 213),
            InputError = Color.FromArgb(211, 47, 47),
            TextPlaceholder = Color.FromArgb(160, 160, 160),

            // ProgressBar
            ProgressBackground = Color.FromArgb(230, 236, 245),
            ProgressFill = Color.FromArgb(30, 90, 168),
            ProgressBorder = Color.FromArgb(208, 215, 226),
            ProgressError = Color.FromArgb(211, 47, 47),
            ProgressSuccess = Color.FromArgb(46, 125, 50),

            // Grid
            GridHeaderBackground = Color.FromArgb(22, 63, 115),
            GridHeaderText = Color.White,
            // Grid utiliza a identidade primária como seleção
            GridSelection = Color.FromArgb(30, 90, 168),
            GridSelectionText = Color.White,

            // Estados
            Error = Color.FromArgb(211, 47, 47),
            Warning = Color.FromArgb(249, 168, 37),
            Success = Color.FromArgb(46, 125, 50),
            Info = Color.FromArgb(30, 136, 229),
                        
            // Fonte
            DefaultFont = new Font("Segoe UI", 9F),

            //Thema claro
            IsDark = false
        };

        // ==========================================================
        // DARK THEME — PREPARADO PARA FUTURO
        // ==========================================================
        public static GsTheme Dark => new GsTheme
        {
            // Identidade
            Primary = Color.FromArgb(58, 123, 213),
            PrimaryDark = Color.FromArgb(18, 32, 52),
            PrimaryLight = Color.FromArgb(90, 155, 245),
            Secondary = Color.FromArgb(155, 110, 255),

            // Superfícies
            Surface = Color.FromArgb(24, 26, 27),
            SurfaceAlt = Color.FromArgb(32, 35, 36),
            Border = Color.FromArgb(60, 60, 60),

            // Texto
            TextPrimary = Color.White,
            TextSecondary = Color.FromArgb(180, 180, 180),
            TextOnPrimary = Color.White,

            // Títulos
            TitleFont = new Font("Segoe UI", 18F, FontStyle.Bold),
            SubtitleFont = new Font("Segoe UI", 14F, FontStyle.Bold),
            SectionFont = new Font("Segoe UI", 12F, FontStyle.Bold),

            TitleText = Color.White,                  // TextPrimary
            SubtitleText = Color.FromArgb(180, 180, 180),  // TextSecondary
            SectionText = Color.FromArgb(180, 180, 180),  // TextSecondary

            // Inputs
            InputBackground = Color.FromArgb(32, 35, 36),
            InputBorder = Color.FromArgb(60, 60, 60),
            InputHover = Color.FromArgb(45, 50, 55),
            InputFocus = Color.FromArgb(90, 155, 245),
            InputError = Color.FromArgb(229, 57, 53),
            TextPlaceholder = Color.FromArgb(140, 140, 140),

            //ProgressBar
            ProgressBackground = Color.FromArgb(45, 50, 55),
            ProgressFill = Color.FromArgb(90, 155, 245),
            ProgressBorder = Color.FromArgb(60, 60, 60),
            ProgressError = Color.FromArgb(229, 57, 53),
            ProgressSuccess = Color.FromArgb(102, 187, 106),

            // Grid
            GridHeaderBackground = Color.FromArgb(18, 32, 52),
            GridHeaderText = Color.White,
            // Grid utiliza a identidade primária como seleção
            GridSelection = Color.FromArgb(58, 123, 213),
            GridSelectionText = Color.White,

            // Estados
            Error = Color.FromArgb(229, 57, 53),
            Warning = Color.FromArgb(255, 202, 40),
            Success = Color.FromArgb(102, 187, 106),
            Info = Color.FromArgb(100, 181, 246),

            // Fonte
            DefaultFont = new Font("Segoe UI", 9F),

            //Thema escuro
            IsDark = true
        };
    }
}
