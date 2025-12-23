using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Display
{
    /// <summary>
    /// Label de status com barra de progresso opcional.
    /// Consome tokens existentes do GsTheme e suporta estados visuais.
    /// </summary>
    public class GsProgressLabel : Control, IThemedControl
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;

        private GsTheme _theme;

        // ============================
        // PROPRIEDADES PÚBLICAS
        // ============================

        [Category("GS Core")]
        public override string Text { get; set; } = string.Empty;

        [Category("GS Core")]
        public Image Image { get; set; }

        [Category("GS Core")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum) _maximum = _minimum;
                Value = _value;
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum < _minimum) _maximum = _minimum;
                Value = _value;
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(_minimum, Math.Min(_maximum, value));
                Invalidate();
            }
        }

        [Category("GS Core")]
        [DefaultValue(true)]
        public bool ShowProgress { get; set; } = true;

        [Category("GS Core")]
        [DefaultValue(ProgressState.Normal)]
        public ProgressState State { get; set; } = ProgressState.Normal;

        // ============================
        // CONSTRUTOR
        // ============================

        public GsProgressLabel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );

            Height = 28;
            Padding = new Padding(8, 4, 8, 4);
        }

        // ============================
        // THEME
        // ============================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.SurfaceAlt;
            ForeColor = theme.TextPrimary;
            Font = theme.DefaultFont;

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
            g.Clear(BackColor);

            Rectangle rect = ClientRectangle;

            // ----------------------------
            // PROGRESSO (FUNDO)
            // ----------------------------
            if (ShowProgress && _maximum > _minimum)
            {
                float percent = (float)(_value - _minimum) / (_maximum - _minimum);
                percent = Math.Max(0, Math.Min(1, percent));

                int fillWidth = (int)(rect.Width * percent);
                if (fillWidth > 0)
                {
                    Color fillColor = State switch
                    {
                        ProgressState.Success => _theme.ProgressSuccess,
                        ProgressState.Error => _theme.ProgressError,
                        _ => _theme.ProgressFill
                    };

                    using var fillBrush = new SolidBrush(fillColor);
                    g.FillRectangle(fillBrush, new Rectangle(rect.X, rect.Y, fillWidth, rect.Height));
                }
            }

            // ----------------------------
            // BORDA
            // ----------------------------
            using (var borderPen = new Pen(_theme.Border))
            {
                g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            // ----------------------------
            // CONTEÚDO (ÍCONE + TEXTO)
            // ----------------------------
            int x = Padding.Left;
            int centerY = rect.Y + rect.Height / 2;

            if (Image != null)
            {
                int iconSize = Math.Min(16, rect.Height - Padding.Vertical);
                int iconY = centerY - iconSize / 2;
                g.DrawImage(Image, new Rectangle(x, iconY, iconSize, iconSize));
                x += iconSize + 6;
            }

            using var textBrush = new SolidBrush(ForeColor);
            SizeF textSize = g.MeasureString(Text, Font);
            float textY = centerY - textSize.Height / 2;
            g.DrawString(Text, Font, textBrush, x, textY);
        }
    }        
}
