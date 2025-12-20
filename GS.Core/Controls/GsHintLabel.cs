using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    public class GsHintLabel : Label, IThemedControl
    {
        public GsHintLabel()
        {
            AutoSize = true;
            BackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleLeft;
            UseMnemonic = false;

            ApplyFont();
        }

        private void ApplyFont()
        {
            Font = new Font(
                Font.FontFamily,
                9f,
                FontStyle.Regular,
                GraphicsUnit.Point
            );
        }

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = Color.Transparent;

            ForeColor = theme.IsDark
                ? Color.FromArgb(170, 170, 170)   // cinza claro no dark
                : Color.FromArgb(90, 90, 90);     // cinza discreto no light

            Font = theme.DefaultFont != null
                ? new Font(theme.DefaultFont.FontFamily, 9f, FontStyle.Regular)
                : Font;

            Invalidate();
        }
    }
}
