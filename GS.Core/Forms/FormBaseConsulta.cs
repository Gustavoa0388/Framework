using GS.Core.UI.Controls;
using GS.Core.UI.Controls.Legacy;
using GS.Core.UI.Theming;
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
    /// Classe base para telas de consulta/listagem.
    ///
    /// ARQUITETURA:
    /// - NÃO usa Designer
    /// - Criação 100% por código
    /// - Responsável por layout, filtro, grid e ações
    ///
    /// REGRAS:
    /// - Telas filhas NÃO devem manipular controles base
    /// - Designer nunca referencia membros daqui
    /// </summary>
    public class FormBaseConsulta : GsBaseForm
    {
        // =============================
        // CONTROLES BASE
        // =============================

        protected TextBox txtFiltro;
        protected GsDataGridView Grid;
        protected FlowLayoutPanel pnlAcoes;
        protected Label lblPagina;

        protected GsButton btnNovo;
        protected GsButton btnEditar;
        protected GsButton btnExcluir;
        protected GsButton btnFechar;
        protected GsButton btnBuscar;

        // =============================
        // PAGINAÇÃO / BUSCA
        // =============================

        private System.Windows.Forms.Timer _debounceTimer;
        private const int DebounceDelay = 300;

        protected GsPaginator paginator;
        private List<object> _dadosPaginados;


        // =============================
        // CONSTRUTOR
        // =============================

        protected FormBaseConsulta()
        {
            SuspendLayout();

            
            CriarGrid();
            CriarAcoes();
            CriarFiltro();
            CriarPaginacao();

            ResumeLayout();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BackColor = BackColor; // neutro, mantém padrão
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

            txtFiltro = new TextBox
            {
                Width = 300
            };

            // ENTER executa busca
            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ExecutarBusca();
                }
            };

            // 🔥 LIMPOU O CAMPO → VOLTA TUDO AUTOMATICAMENTE
            txtFiltro.TextChanged += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(txtFiltro.Text))
                {
                    ExecutarBusca();
                }
            };

            btnBuscar = new GsButton
            {
                Text = "Buscar",
                Width = 90
            };
            btnBuscar.Click += (_, _) => ExecutarBusca();

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
                Padding = new Padding(0, 2, 0, 0) // 👈 ESSENCIAL
            };

            Grid = new GsDataGridView
            {
                Dock = DockStyle.Fill
            };

            pnlGrid.Controls.Add(Grid);
            Controls.Add(pnlGrid);
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


        protected string TextoFiltro => txtFiltro?.Text?.Trim();

        protected virtual void ExecutarBusca()
        {
            CarregarDados();
        }

        protected void AtualizarPaginacao(IEnumerable<object> dados)
        {
            _dadosPaginados = dados.ToList();

            paginator.TotalItems = _dadosPaginados.Count;
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
        // AÇÕES (OVERRIDE)
        // =============================

        protected virtual void CarregarDados() { }
        protected virtual void OnNovoClick(object sender, EventArgs e) { }
        protected virtual void OnEditarClick(object sender, EventArgs e) { }
        protected virtual void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null) return;
            if (!FormMsg.Confirm("Deseja excluir o registro selecionado?")) return;
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
    }
}
