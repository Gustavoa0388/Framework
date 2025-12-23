using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsRadioOption
    /// 
    /// RadioButton moderno do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Representar uma opção exclusiva
    /// - Integrar com GsTheme
    /// - Visual consistente entre Light/Dark
    /// 
    /// NÃO FAZ:
    /// - Lógica de banco
    /// - Uso de Tag
    /// - Animação custom
    /// </summary>
    public class GsRadioOption : RadioButton, IThemedControl
    {
        private const int RadioSize = 14;
        private GsTheme _theme;

        public GsRadioOption()
        {
            AutoSize = false;
            Height = 22;

            Cursor = Cursors.Hand;
            Padding = new Padding(RadioSize + 6, 0, 0, 0);
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;

            Font = theme.DefaultFont;
            ForeColor = theme.RadioText;

            Invalidate();
        }

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? BackColor);

            // Área do radio
            Rectangle circle = new Rectangle(0, (Height - RadioSize) / 2, RadioSize, RadioSize);

            using (var pen = new Pen(_theme.RadioBorder, 1))
                g.DrawEllipse(pen, circle);

            if (Checked)
            {
                Rectangle inner = Rectangle.Inflate(circle, -4, -4);
                using (var brush = new SolidBrush(_theme.RadioFill))
                    g.FillEllipse(brush, inner);
            }

            // Texto
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                new Rectangle(RadioSize + 6, 0, Width, Height),
                ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );
        }
    }
}
