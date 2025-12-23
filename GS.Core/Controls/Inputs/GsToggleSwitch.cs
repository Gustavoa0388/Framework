using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsToggleSwitch
    /// 
    /// Controle moderno de alternância (on/off) do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Representar estado booleano
    /// - Integrar com Theme
    /// - Exibir texto contextual opcional
    /// 
    /// NÃO FAZ:
    /// - Lógica de banco
    /// - Tag legacy
    /// - Múltiplos modelos visuais
    /// </summary>
    public class GsToggleSwitch : CheckBox, IThemedControl
    {
        public GsToggleSwitch()
        {
            AutoSize = false;
            Width = 50;
            Height = 24;

            Cursor = Cursors.Hand;
            Appearance = Appearance.Button;
            TextAlign = ContentAlignment.MiddleLeft;
            Text = string.Empty;
        }

        // =====================================================
        // PROPRIEDADES
        // =====================================================

        /// <summary>
        /// Texto exibido quando o toggle está ligado.
        /// </summary>
        [Category("GS Core")]
        public string OnText { get; set; } = "On";

        /// <summary>
        /// Texto exibido quando o toggle está desligado.
        /// </summary>
        [Category("GS Core")]
        public string OffText { get; set; } = "Off";

        /// <summary>
        /// Indica se o toggle está ligado.
        /// </summary>
        [Browsable(false)]
        public bool IsOn
        {
            get => Checked;
            set => Checked = value;
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            Font = theme.DefaultFont;
            ForeColor = theme.ToggleText;
            Invalidate();
        }

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.Transparent);

            var theme = ThemeManager.Current;

            Rectangle track = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle thumb = new Rectangle(
                Checked ? Width - Height : 0,
                0,
                Height,
                Height
            );

            using (var bg = new SolidBrush(Checked
                ? theme.ToggleOnBackground
                : theme.ToggleOffBackground))
            {
                g.FillEllipse(bg, track);
            }

            using (var thumbBrush = new SolidBrush(theme.ToggleThumb))
            {
                g.FillEllipse(thumbBrush, thumb);
            }

            string text = Checked ? OnText : OffText;

            if (!string.IsNullOrEmpty(text))
            {
                TextRenderer.DrawText(
                    g,
                    text,
                    Font,
                    new Rectangle(Width + 6, 0, 200, Height),
                    ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
            }
        }
    }
}
