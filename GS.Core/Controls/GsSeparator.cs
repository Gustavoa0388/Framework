using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Separador visual padrão do GS Core.
    /// Suporta orientação horizontal ou vertical e aplica tema automaticamente.
    /// </summary>
    public class GsSeparator : Control, IThemedControl
    {
        private GsTheme _theme;

        // ============================
        // PROPRIEDADES
        // ============================

        [Category("GS Core")]
        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;

        [Category("GS Core")]
        [DefaultValue(1)]
        public int Thickness { get; set; } = 1;

        [Category("GS Core")]
        [DefaultValue(0)]
        public int InnerPadding { get; set; } = 0;

        // ============================
        // CONSTRUTOR
        // ============================

        public GsSeparator()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );

            Height = 1;
            Width = 100;
        }

        // ============================
        // THEME
        // ============================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.Border;
            Invalidate();
        }

        // ============================
        // PAINT
        // ============================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_theme == null)
                return;

            Graphics g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Color.Transparent);

            using var brush = new SolidBrush(_theme.Border);

            if (Orientation == Orientation.Horizontal)
            {
                int y = (Height - Thickness) / 2;
                g.FillRectangle(
                    brush,
                    InnerPadding,
                    y,
                    Width - (InnerPadding * 2),
                    Thickness
                );
            }
            else
            {
                int x = (Width - Thickness) / 2;
                g.FillRectangle(
                    brush,
                    x,
                    InnerPadding,
                    Thickness,
                    Height - (InnerPadding * 2)
                );
            }
        }
    }
}
