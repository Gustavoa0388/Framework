using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsSkeletonView
    ///
    /// Overlay visual leve para simulação de carregamento
    /// de grids ou listas.
    ///
    /// RESPONSABILIDADE:
    /// - Renderizar placeholders
    /// - Respeitar layout semântico
    ///
    /// NÃO FAZ:
    /// - Não carrega dados
    /// - Não bloqueia interação
    /// - Não controla tempo
    /// </summary>
    public partial class GsSkeletonView : UserControl, IThemedControl
    {
        private GsTheme _theme;
        private GsSkeletonLayout _layout;

        public GsSkeletonView()
        {
            Dock = DockStyle.Fill;
            Visible = false;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );
        }

        // =====================================================
        // API PÚBLICA
        // =====================================================

        /// <summary>
        /// Aplica o layout semântico do skeleton.
        /// </summary>
        public void SetLayout(GsSkeletonLayout layout)
        {
            _layout = layout;
            Invalidate();
        }

        /// <summary>
        /// Exibe o skeleton.
        /// </summary>
        public void ShowSkeleton()
        {
            Visible = true;
            BringToFront();
        }

        /// <summary>
        /// Oculta o skeleton.
        /// </summary>
        public void HideSkeleton()
        {
            Visible = false;
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.Surface;
            Invalidate();
        }

        // =====================================================
        // RENDER
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_theme == null || _layout == null)
                return;

            Graphics g = e.Graphics;
            g.Clear(BackColor);

            using var brush = new SolidBrush(_theme.Border);

            int y = 16;
            int width = ClientSize.Width - 32;

            for (int row = 0; row < _layout.Rows; row++)
            {
                int x = 16;

                foreach (var col in _layout.Columns)
                {
                    int colWidth = width * col / 100;

                    var rect = new Rectangle(
                        x,
                        y,
                        colWidth - 8,
                        _layout.RowHeight
                    );

                    g.FillRectangle(brush, rect);
                    x += colWidth;
                }

                y += _layout.RowHeight + 12;
            }
        }
    }
}
