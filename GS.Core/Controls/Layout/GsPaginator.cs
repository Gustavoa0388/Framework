using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// Controle de paginação padronizado do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Navegar entre páginas
    /// - Exibir estado atual
    /// - Emitir evento semântico
    /// 
    /// NÃO FAZ:
    /// - Carga de dados
    /// - Filtro
    /// - Integração direta com grid
    /// </summary>
    public class GsPaginator : UserControl, IThemedControl
    {
        // ======================================================
        // EVENTOS
        // ======================================================

        public event EventHandler<GsPageChangedEventArgs> PageChanged;

        // ======================================================
        // CAMPOS
        // ======================================================

        private readonly Button btnPrev;
        private readonly Button btnNext;
        private readonly Label lblInfo;

        private int _currentPage = 1;
        private int _totalItems;

        // ======================================================
        // PROPRIEDADES
        // ======================================================

        public int PageSize { get; set; } = 10;

        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                Recalcular();
            }
        }

        public int CurrentPage => _currentPage;

        public int TotalPages =>
            PageSize <= 0
                ? 0
                : (int)Math.Ceiling((double)TotalItems / PageSize);

        public GsPaginatorState State { get; set; } = GsPaginatorState.Ready;

        // ======================================================
        // CONSTRUTOR
        // ======================================================

        public GsPaginator()
        {
            Height = 36;
            Dock = DockStyle.Bottom;

            btnPrev = CriarBotao("<");
            btnNext = CriarBotao(">");

            lblInfo = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 160
            };

            btnPrev.Click += (_, _) => GoToPage(_currentPage - 1);
            btnNext.Click += (_, _) => GoToPage(_currentPage + 1);

            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(10, 6, 10, 6)
            };

            layout.Controls.Add(btnPrev);
            layout.Controls.Add(lblInfo);
            layout.Controls.Add(btnNext);

            Controls.Add(layout);

            AtualizarUI();
        }

        // ======================================================
        // MÉTODOS PÚBLICOS
        // ======================================================

        public void GoToPage(int page)
        {
            if (State != GsPaginatorState.Ready)
                return;

            if (page < 1 || page > TotalPages)
                return;

            _currentPage = page;

            AtualizarUI();

            PageChanged?.Invoke(
                this,
                new GsPageChangedEventArgs(
                    _currentPage,
                    PageSize,
                    TotalPages
                )
            );
        }

        public void Reset()
        {
            _currentPage = 1;
            AtualizarUI();
        }

        // ======================================================
        // LÓGICA INTERNA
        // ======================================================

        private void Recalcular()
        {
            if (_currentPage > TotalPages)
                _currentPage = TotalPages > 0 ? TotalPages : 1;

            AtualizarUI();
        }

        private void AtualizarUI()
        {
            lblInfo.Text = TotalPages == 0
                ? "0 / 0"
                : $"{_currentPage} / {TotalPages}";

            bool enabled = State == GsPaginatorState.Ready && TotalPages > 1;

            btnPrev.Enabled = enabled && _currentPage > 1;
            btnNext.Enabled = enabled && _currentPage < TotalPages;
        }

        private Button CriarBotao(string texto)
        {
            return new Button
            {
                Text = texto,
                Width = 32,
                Height = 24
            };
        }

        // ======================================================
        // THEME
        // ======================================================

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = theme.Surface;

            foreach (Control c in Controls)
            {
                c.Font = theme.DefaultFont;
                c.ForeColor = theme.TextPrimary;
            }
        }
    }
}
