using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    public class GsErrorLabel : Label, IThemedControl
    {
        public GsErrorLabel()
        {
            AutoSize = true;
            BackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleLeft;
            UseMnemonic = false;

            Visible = false; // começa oculto
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

        /// <summary>
        /// Exibe a mensagem de erro.
        /// </summary>
        public void ShowError(string message)
        {
            Text = message;
            Visible = true;
        }

        /// <summary>
        /// Oculta a mensagem de erro.
        /// </summary>
        public void ClearError()
        {
            Text = string.Empty;
            Visible = false;
        }

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = Color.Transparent;

            // Vermelho adaptado ao tema
            ForeColor = theme.IsDark
                ? Color.FromArgb(255, 120, 120) // vermelho suave no dark
                : Color.FromArgb(200, 0, 0);    // vermelho padrão no light

            Font = theme.DefaultFont != null
                ? new Font(theme.DefaultFont.FontFamily, 9f, FontStyle.Regular)
                : Font;

            Invalidate();
        }
    }
}
