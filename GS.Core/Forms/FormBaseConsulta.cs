using GS.Core.UI.Controls.Data;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Controls.Layout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormBaseConsulta
    ///
    /// Classe base para telas de consulta/listagem no GS Core.
    ///
    /// RESPONSABILIDADES:
    /// - Criar layout base (filtro, grid, paginação, ações)
    /// - Centralizar lógica de busca e paginação
    /// - Orquestrar ações padrão (Novo, Editar, Excluir, Fechar)
    ///
    /// REGRAS:
    /// - NÃO usa Designer
    /// - Telas filhas NÃO manipulam controles base diretamente
    /// - Telas filhas sobrescrevem apenas métodos virtuais
    /// </summary>
    public abstract class FormBaseConsulta : GsBaseForm
    {
        // =============================
        // CONTROLES BASE
        // =============================

        protected GsTextBox txtFiltro;
        protected GsDataGridView Grid;
        protected FlowLayoutPanel pnlAcoes;

        protected GsButton btnNovo;
        protected GsButton btnEditar;
        protected GsButton btnExcluir;
        protected GsButton btnFechar;
        protected GsButton btnBuscar;

        protected GsPaginator paginator;

        private List<object> _dadosPaginados = new();

        // =============================
        // CONSTRUTOR
        // =============================

        protected FormBaseConsulta()
        {
            SuspendLayout();

            CriarGrid();
            CriarFiltro();
            CriarPaginacao();
            CriarAcoes();

            ResumeLayout();
        }

        // =============================
        // FILTRO
        // =============================

        private void CriarFiltro()
        {
            var pnlFiltro = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 45,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            txtFiltro = new GsTextBox
            {
                Width = 300
            };

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ExecutarBusca();
                }
            };

            txtFiltro.TextChanged += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(txtFiltro.Text))
                    ExecutarBusca();
            };

            btnBuscar = CriarBotao("Buscar", (_, _) => ExecutarBusca());

            pnlFiltro.Controls.Add(txtFiltro);
            pnlFiltro.Controls.Add(btnBuscar);

            Controls.Add(pnlFiltro);
        }

        // =============================
        // GRID
        // =============================

        private void CriarGrid()
        {
            var pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 2, 0, 0)
            };

            Grid = new GsDataGridView
            {
                Dock = DockStyle.Fill
            };

            // 🔗 Integração oficial de ações do grid
            Grid.ActionRequested += OnGridActionRequested;

            pnlGrid.Controls.Add(Grid);
            Controls.Add(pnlGrid);

        }

        /// <summary>
        /// Handler central de ações solicitadas pelo grid.
        /// Centraliza Enter, duplo clique e futuras ações.
        /// </summary>
        private void OnGridActionRequested(object sender, GsGridActionEventArgs e)
        {
            switch (e.Action)
            {
                case GsGridAction.Edit:
                    OnEditarClick(this, EventArgs.Empty);
                    break;

                case GsGridAction.View:
                    OnVisualizarClick(this, EventArgs.Empty);
                    break;

                case GsGridAction.Select:
                    OnSelecionarClick(this, EventArgs.Empty);
                    break;
            }
        }
        /// <summary>
        /// Ação de visualização (opcional).
        /// </summary>
        protected virtual void OnVisualizarClick(object sender, EventArgs e)
        {
            // opcional — tela filha decide
        }

        /// <summary>
        /// Ação de seleção (opcional).
        /// </summary>
        protected virtual void OnSelecionarClick(object sender, EventArgs e)
        {
            // opcional — tela filha decide
        }


        // =============================
        // PAGINAÇÃO
        // =============================

        private void CriarPaginacao()
        {
            paginator = new GsPaginator
            {
                Dock = DockStyle.Bottom,
                PageSize = 10
            };

            paginator.PageChanged += (_, e) =>
            {
                AtualizarGridPagina(e.Page, e.PageSize);
            };

            Controls.Add(paginator);
        }
        
        // =============================
        // AÇÕES
        // =============================

        private void CriarAcoes()
        {
            pnlAcoes = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(10, 5, 10, 5),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnNovo = CriarBotao("Novo", OnNovoClick);
            btnEditar = CriarBotao("Editar", OnEditarClick);
            btnExcluir = CriarBotao("Excluir", OnExcluirClick);
            btnFechar = CriarBotao("Fechar", (_, _) => Close());

            pnlAcoes.Controls.AddRange(new Control[]
            {
                btnNovo, btnEditar, btnExcluir, btnFechar
            });

            Controls.Add(pnlAcoes);
        }

        // =============================
        // BUSCA / PAGINAÇÃO
        // =============================

        protected string TextoFiltro => txtFiltro?.Text?.Trim();

        protected virtual void ExecutarBusca()
        {
            Grid.State = GsGridState.Loading;
            paginator.State = GsPaginatorState.Loading;

            CarregarDados();
        }

        protected void AtualizarPaginacao(IEnumerable<object> dados)
        {
            _dadosPaginados = dados.ToList();

            paginator.TotalItems = _dadosPaginados.Count;
            paginator.Reset();

            Grid.State = _dadosPaginados.Count == 0
                ? GsGridState.Empty
                : GsGridState.Ready;

            paginator.State = GsPaginatorState.Ready;

            AtualizarGridPagina(1, paginator.PageSize);
        }
        private void AtualizarGridPagina(int page, int pageSize)
        {
            var pageData = _dadosPaginados
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Grid.DataSource = pageData;
        }

        // =============================
        // CONTRATOS PARA TELAS FILHAS
        // =============================

        protected abstract void CarregarDados();

        protected virtual void OnNovoClick(object sender, EventArgs e) { }
        protected virtual void OnEditarClick(object sender, EventArgs e) { }

        /// <summary>
        /// Ação de exclusão padrão.
        /// Usa confirmação modal legacy (FormMsg) por decisão consciente.
        /// </summary>
        protected virtual void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (!FormMsg.Confirm("Deseja excluir o registro selecionado?"))
                return;
        }

        // =============================
        // UTILITÁRIOS
        // =============================

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
    }
}
