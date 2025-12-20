using System.Drawing;

namespace GS.Core.UI.Theming
{
    public static class GsThemes
    {
        public static GsTheme Light = new GsTheme
        {
            BackColor = Color.White,
            ForeColor = Color.Black,
            Primary = Color.FromArgb(25, 25, 25), // Preto suave
            Secondary = Color.Gray,
            InputBack = Color.White,
            Border = Color.Silver,
            DefaultFont = new Font("Segoe UI", 9f),
            InputBackground = Color.White,                  // Fundo limpo
            InputHover = Color.FromArgb(245, 245, 245),// Cinza claro suave
            InputFocus = Color.FromArgb(25, 25, 25),// Azul claro de foco
            TextPrimary = Color.FromArgb(50, 50, 50),   // Preto suave (menos agressivo)
            InputError = Color.FromArgb(255, 230, 230),
            TextSecondary = Color.FromArgb(80, 80, 80), // Cinza escuro para texto secundário
            InputBorder = Color.FromArgb(200, 200, 200),
            IsDark = false,
            Error = Color.FromArgb(220, 53, 69), // vermelho Bootstrap-style

        };

        public static GsTheme Dark = new GsTheme
        {
            BackColor = Color.FromArgb(32, 32, 32),
            ForeColor = Color.White,
            Primary = Color.FromArgb(0, 153, 255),
            Secondary = Color.DarkGray,
            InputBack = Color.FromArgb(45, 45, 45),
            Border = Color.FromArgb(70, 70, 70),
            DefaultFont = new Font("Segoe UI", 9f),
            InputBackground = Color.FromArgb(32, 32, 32),   // Fundo escuro padrão VS
            InputHover = Color.FromArgb(110, 110, 110),   // Hover discreto
            InputFocus = Color.FromArgb(160, 160, 160),  // Azul acinzentado
            TextPrimary = Color.FromArgb(230, 230, 230), // Branco suave
            InputError = Color.FromArgb(90, 40, 40),
            InputBorder = Color.FromArgb(80, 80, 80),
            TextSecondary = Color.Gainsboro, // Cinza claro para texto secundário
            IsDark = true,
            Error = Color.FromArgb(255, 99, 99), // vermelho suave no dark

        };
    }
}
