using GS.Core.UI.Theming;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Display
{
    /// <summary>
    /// GsLabel
    /// 
    /// Label avançado do GS Core com compatibilidade LEGACY
    /// para o Designer WinForms.
    /// </summary>
    public class GsLabel : Control, IThemedControl
    {
        // ============================
        // LEGACY — PROPRIEDADES DO DESIGNER
        // ============================

        [Category("Legacy")]
        public Color Cor1 { get; set; } = Color.Black;

        [Category("Legacy")]
        public Color Cor2 { get; set; } = Color.Black;

        [Category("Legacy")]
        public int Angulo { get; set; }

        [Category("Legacy")]
        public bool AtivarSombra { get; set; }

        [Category("Legacy")]
        public Color CorSombra { get; set; } = Color.Black;

        /// <summary>
        /// LEGACY: alinhamento do texto (Designer)
        /// </summary>
        [Category("Legacy")]
        public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleLeft;

        /// <summary>
        /// LEGACY: mapeia para Location.X
        /// </summary>
        [Category("Legacy")]
        public int X
        {
            get => Location.X;
            set => Location = new Point(value, Location.Y);
        }

        /// <summary>
        /// LEGACY: mapeia para Location.Y
        /// </summary>
        [Category("Legacy")]
        public int Y
        {
            get => Location.Y;
            set => Location = new Point(Location.X, value);
        }

        // ============================
        // GS CORE — PROPRIEDADES ATIVAS
        // ============================

        public Control TargetControl { get; set; }

        [Category("GS Core")]
        public bool Required { get; set; }

        [Category("GS Core")]
        public bool UseEllipsis { get; set; } = true;

        private GsTheme _theme;

        public GsLabel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw,
                true
            );

            AutoSize = false;
            Height = 24;
        }

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            Font = theme.DefaultFont;
            ForeColor = theme.TextPrimary;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_theme == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            string text = Text;
            if (Required)
                text += " *";

            Rectangle area = ClientRectangle;

            using Brush textBrush = new SolidBrush(ForeColor);

            StringFormat format = CriarStringFormat(TextAlign);

            // Sombra (LEGACY)
            if (AtivarSombra)
            {
                using var shadowBrush = new SolidBrush(Color.FromArgb(80, CorSombra));
                var shadowRect = new Rectangle(area.X + 1, area.Y + 1, area.Width, area.Height);
                g.DrawString(text, Font, shadowBrush, shadowRect, format);
            }

            g.DrawString(text, Font, textBrush, area, format);
        }

        /// <summary>
        /// Converte ContentAlignment em StringFormat
        /// </summary>
        private StringFormat CriarStringFormat(ContentAlignment align)
        {
            var format = new StringFormat
            {
                Trimming = UseEllipsis ? StringTrimming.EllipsisCharacter : StringTrimming.None,
                FormatFlags = StringFormatFlags.NoWrap
            };

            switch (align)
            {
                case ContentAlignment.TopLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }

            return format;
        }
    }
}
