using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;
using GS.Core.UI.Controls.States;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Controle de paginação padrão do GS Core.
    /// Emite eventos de navegação sem acoplamento a grids ou dados.
    /// </summary>
    public class GsPaginator : Control, IThemedControl
    {
        private GsTheme _theme;

        private int _pageSize = 10;
        private int _totalItems;
        private int _currentPage = 1;

        // ============================
        // PROPRIEDADES
        // ============================

        [Category("GS Core")]
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = Math.Max(1, value);
                CurrentPage = 1;
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = Math.Max(0, value);
                CurrentPage = Math.Min(CurrentPage, TotalPages);
                Invalidate();
            }
        }

        [Category("GS Core")]
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                int newPage = Math.Max(1, Math.Min(TotalPages, value));
                if (_currentPage == newPage)
                    return;

                _currentPage = newPage;
                OnPageChanged();
                Invalidate();
            }
        }

        [Browsable(false)]
        public int TotalPages =>
            PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalItems / PageSize);

        [Category("GS Core")]
        [DefaultValue(true)]
        public bool ShowPageNumbers { get; set; } = true;

        // ============================
        // EVENTOS
        // ============================

        public event EventHandler<PageChangedEventArgs> PageChanged;

        // ============================
        // CONSTRUTOR
        // ============================

        public GsPaginator()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true
            );

            Height = 32;
            Width = 240;
        }

        // ============================
        // THEME
        // ============================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;
            BackColor = theme.Surface;
            ForeColor = theme.TextPrimary;
            Font = theme.DefaultFont;
            Invalidate();
        }

        // ============================
        // EVENTOS INTERNOS
        // ============================

        protected virtual void OnPageChanged()
        {
            PageChanged?.Invoke(
                this,
                new PageChangedEventArgs(CurrentPage, PageSize)
            );
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

            string text = $"Página {CurrentPage} de {TotalPages}";
            SizeF size = g.MeasureString(text, Font);

            float x = (Width - size.Width) / 2;
            float y = (Height - size.Height) / 2;

            using var brush = new SolidBrush(ForeColor);
            g.DrawString(text, Font, brush, x, y);
        }
    }
}
