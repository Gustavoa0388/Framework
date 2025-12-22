using GS.Core.UI.Controls;
using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    public partial class FormBaseConsulta : GsBaseForm
    {
        private System.Windows.Forms.Timer _debounceTimer;
        private const int DebounceDelay = 300; // ms
        protected GsPaginator<object> _paginator;
        protected Label lblPagina;

        // =============================
        // CONTROLES BASE
        // =============================
        protected TextBox txtFiltro;
        protected GsDataGridView Grid;
        protected Panel pnlAcoes;
        protected GsButton btnNovo;
        protected GsButton btnEditar;
        protected GsButton btnExcluir;
        protected GsButton btnFechar;

        public FormBaseConsulta()
        {
            InicializarBase();
        }

        // =============================
        // INICIALIZAÇÃO
        // =============================
        private void InicializarBase()
        {
            // =============================
            // PAINEL DE FILTRO
            // =============================
            var pnlFiltro = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                Padding = new Padding(10)
            };

            // =============================
            // FILTRO
            // =============================
            txtFiltro = new TextBox
            {
                Dock = DockStyle.Left,
                Width = 300
            };

            txtFiltro.TextChanged += (_, _) =>
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            };


            pnlFiltro.Controls.Add(txtFiltro);

            _debounceTimer = new System.Windows.Forms.Timer
            {
                Interval = DebounceDelay
            };

            _debounceTimer.Tick += (_, _) =>
            {
                _debounceTimer.Stop();
                OnPesquisarClick();
            };


            // =============================
            // GRID
            // =============================
            Grid = new GsDataGridView
            {
                Dock = DockStyle.Fill
            };

            Grid.ApplyTheme(ThemeManager.Current);
            Grid.EditarSolicitado += (_, _) => OnEditarClick(this, EventArgs.Empty);


            // =============================
            // PAINEL DE AÇÕES
            // =============================
            pnlAcoes = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(10)
            };

            btnNovo = CriarBotao("Novo", OnNovoClick);
            btnEditar = CriarBotao("Editar", OnEditarClick);
            btnExcluir = CriarBotao("Excluir", OnExcluirClick);
            btnFechar = CriarBotao("Fechar", (s, e) => Close());

            pnlAcoes.Controls.AddRange(new Control[]
            {
        btnNovo, btnEditar, btnExcluir, btnFechar
            });


            // =============================
            // PAGINAÇÃO
            // =============================

            _paginator = new GsPaginator<object>
            {
                PageSize = 10
            };

            lblPagina = new Label
            {
                AutoSize = true,
                Text = "Página 1 / 1",
                Left = 12,
                Top = pnlAcoes.Top - 22
            };

            Controls.Add(lblPagina);

            // =============================
            // ADD NA TELA (ORDEM IMPORTA)
            // =============================
            Controls.Add(Grid);
            Controls.Add(pnlAcoes);
            Controls.Add(pnlFiltro);


            PositionarBotoes();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Grid?.ApplyTheme(ThemeManager.Current);
        }
        protected void AtualizarPaginacao(IEnumerable<object> dados)
        {
            _paginator.SetSource(dados);

            Grid.DataSource = _paginator.GetCurrentPage().ToList();

            lblPagina.Text = $"Página {_paginator.CurrentPage} / {_paginator.TotalPages}";
        }

        private GsButton CriarBotao(string texto, EventHandler click)
        {
            var btn = new GsButton
            {
                Text = texto,
                Width = 100,
                Height = 30
            };
            btn.Click += click;
            return btn;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (pnlAcoes != null && pnlAcoes.IsHandleCreated)
                PositionarBotoes();
        }

        private void PositionarBotoes()
        {
            int right = pnlAcoes.Width - 10;
            int top = 10;

            btnFechar.Location = new Point(right - btnFechar.Width, top);
            right -= btnFechar.Width + 10;

            btnExcluir.Location = new Point(right - btnExcluir.Width, top);
            right -= btnExcluir.Width + 10;

            btnEditar.Location = new Point(right - btnEditar.Width, top);
            right -= btnEditar.Width + 10;

            btnNovo.Location = new Point(right - btnNovo.Width, top);
        }

        protected void PaginaAnterior()
        {
            _paginator.Previous();
            Grid.DataSource = _paginator.GetCurrentPage().ToList();
            AtualizarLabelPagina();
        }

        protected void ProximaPagina()
        {
            _paginator.Next();
            Grid.DataSource = _paginator.GetCurrentPage().ToList();
            AtualizarLabelPagina();
        }

        private void AtualizarLabelPagina()
        {
            lblPagina.Text = $"Página {_paginator.CurrentPage} / {_paginator.TotalPages}";
        }


        // =============================
        // CONTRATO PARA OS FILHOS
        // =============================
        protected virtual void OnPesquisarClick()
        {
            CarregarDados();
        }

        protected virtual void CarregarDados() { }

        protected virtual void OnNovoClick(object sender, EventArgs e) { }
        protected virtual void OnEditarClick(object sender, EventArgs e) { }

        protected virtual void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (!FormMsg.Confirm("Deseja realmente excluir o registro selecionado?"))
                return;
        }
    }
}
