using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;
using GS.Core.UI.Controls.States;

namespace GS.Core.UI.Controls.Display
{
    /// <summary>
    /// Container visual para exibição de imagens no GS Core.
    /// Suporta escala, borda e estados visuais com tema.
    /// </summary>
    public class GsImageBox : Control, IThemedControl
    {
        private GsTheme _theme;

        // ============================
        // PROPRIEDADES
        // ============================

        [Category("GS Core")]
        public Image Image { get; set; }

        [Category("GS Core")]
        [DefaultValue(GsImageScaleMode.Fit)]
        public GsImageScaleMode ScaleMode { get; set; } = GsImageScaleMode.Fit;

        [Category("GS Core")]
        [DefaultValue(true)]
        public bool ShowBorder { get; set; } = true;

        [Category("GS Core")]
        public Padding ImagePadding { get; set; } = new Padding(8);

        [Category("GS Core")]
        [DefaultValue(ImageBoxState.Normal)]
        public ImageBoxState State { get; set; } = ImageBoxState.Normal;

        // ============================
        // CONSTRUTOR
        // ============================

        public GsImageBox()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );

            Size = new Size(120, 120);
        }

        // ============================
        // THEME
        // ============================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.Surface;
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

            Rectangle contentRect = new Rectangle(
                ImagePadding.Left,
                ImagePadding.Top,
                Width - ImagePadding.Horizontal,
                Height - ImagePadding.Vertical
            );

            // ----------------------------
            // IMAGEM
            // ----------------------------
            if (Image != null)
            {
                Rectangle imgRect = CalculateImageRect(Image, contentRect, ScaleMode);
                g.DrawImage(Image, imgRect);
            }

            // ----------------------------
            // BORDA
            // ----------------------------
            if (ShowBorder)
            {
                using var pen = new Pen(_theme.Border);
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }

        // ============================
        // HELPERS
        // ============================

        private static Rectangle CalculateImageRect(
            Image image,
            Rectangle bounds,
            GsImageScaleMode mode)
        {
            if (mode == GsImageScaleMode.Stretch)
                return bounds;

            Size imgSize = image.Size;

            float ratioX = (float)bounds.Width / imgSize.Width;
            float ratioY = (float)bounds.Height / imgSize.Height;

            float ratio = mode == GsImageScaleMode.Fill
                ? Math.Max(ratioX, ratioY)
                : Math.Min(ratioX, ratioY);

            int w = (int)(imgSize.Width * ratio);
            int h = (int)(imgSize.Height * ratio);

            int x = bounds.X + (bounds.Width - w) / 2;
            int y = bounds.Y + (bounds.Height - h) / 2;

            return new Rectangle(x, y, w, h);
        }
    }
}
