using GS.Core.UI.Controls;
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
    /// Classe base para formulários de consulta/listagem.
    /// 
    /// STATUS:
    /// - LEGACY COMPATÍVEL (WinForms Designer)
    /// - PREPARADO PARA GS CORE
    /// 
    /// RESPONSABILIDADES:
    /// - Fornecer estrutura padrão de consulta
    /// - Centralizar grid, filtro, ações e paginação
    /// - Definir contrato para ações (Novo, Editar, Excluir)
    /// 
    /// IMPORTANTE:
    /// - Este formulário NÃO é puramente GS Core ainda
    /// - Nomes de controles e assinaturas NÃO DEVEM ser alterados
    /// - Refatoração estrutural ficará para a FASE EXTRA
    /// </summary>
    public partial class FormBaseConsulta : GsBaseForm
    {
        // =============================
        // CAMPOS PRIVADOS
        // =============================

        /// <summary>
        /// Timer usado para debounce do filtro de texto.
        /// System.Windows.Forms.Timer é obrigatório para evitar ambiguidade.
        /// </summary>
        private System.Windows.Forms.Timer _debounceTimer;

        /// <summary>
        /// Delay do debounce em milissegundos.
        /// </summary>
        private const int DebounceDelay = 300;

        /// <summary>
        /// Paginador genérico.
        /// Atualmente trabalha com object por compatibilidade.
        /// </summary>
        protected GsPaginator<object> _paginator;

        /// <summary>
        /// Label que exibe a página atual.
        /// Mantido como Label nativo por compatibilidade com o Designer.
        /// </summary>
        protected Label lblPagina;

        // =============================
        // CONTROLES BASE (CONTRATO)
        // =============================

        /// <summary>
        /// Campo de filtro de texto.
        /// Usado pelo Demo e pelos formulários filhos.
        /// </summary>
        protected TextBox txtFiltro;

        /// <summary>
        /// Grid principal de listagem.
        /// </summary>
        protected GsDataGridView Grid;

        /// <summary>
        /// Painel inferior de ações.
        /// </summary>
        protected Panel pnlAcoes;

        /// <summary>
        /// Botões de ação padrão.
        /// </summary>
        protected GsButton btnNovo;
        protected GsButton btnEditar;
        protected GsButton btnExcluir;
        protected GsButton btnFechar;

        // =============================
        // CONSTRUTOR
        // =============================

        public FormBaseConsulta()
        {
            InicializarBase();
        }

        // =============================
        // INICIALIZAÇÃO DA TELA
        // =============================

        /// <summary>
        /// Inicializa toda a estrutura visual do formulário.
        /// IMPORTANTE: a ordem de adição dos controles importa.
        /// </summary>
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

            txtFiltro = new TextBox
            {
                Dock = DockStyle.Left,
                Width = 300
            };

            // Dispara debounce ao digitar
            txtFiltro.TextChanged += (_, _) =>
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            };

            pnlFiltro.Controls.Add(txtFiltro);

            // =============================
            // TIMER DE DEBOUNCE
            // =============================

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

            // Evento de edição solicitado pelo grid
            Grid.EditarSolicitado += (_, _) =>
                OnEditarClick(this, EventArgs.Empty);

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
            // ADIÇÃO FINAL NA TELA
            // =============================

            Controls.Add(Grid);
            Controls.Add(pnlAcoes);
            Controls.Add(pnlFiltro);

            PositionarBotoes();
        }

        // =============================
        // EVENTOS DO FORM
        // =============================

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Grid?.ApplyTheme(ThemeManager.Current);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (pnlAcoes != null && pnlAcoes.IsHandleCreated)
                PositionarBotoes();
        }

        // =============================
        // PAGINAÇÃO
        // =============================

        /// <summary>
        /// Atualiza a paginação com uma nova lista de dados.
        /// </summary>
        protected void AtualizarPaginacao(IEnumerable<object> dados)
        {
            _paginator.SetSource(dados);

            Grid.DataSource = _paginator.GetCurrentPage().ToList();

            AtualizarLabelPagina();
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
            lblPagina.Text =
                $"Página {_paginator.CurrentPage} / {_paginator.TotalPages}";
        }

        // =============================
        // LAYOUT DOS BOTÕES
        // =============================

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

        // =============================
        // CRIAÇÃO DE BOTÕES
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

        // =============================
        // CONTRATO PARA CLASSES FILHAS
        // =============================

        /// <summary>
        /// Disparado ao pesquisar (filtro ou botão).
        /// </summary>
        protected virtual void OnPesquisarClick()
        {
            CarregarDados();
        }

        /// <summary>
        /// Método que deve carregar os dados no grid.
        /// </summary>
        protected virtual void CarregarDados() { }

        /// <summary>
        /// Ação Novo.
        /// </summary>
        protected virtual void OnNovoClick(object sender, EventArgs e) { }

        /// <summary>
        /// Ação Editar.
        /// </summary>
        protected virtual void OnEditarClick(object sender, EventArgs e) { }

        /// <summary>
        /// Ação Excluir.
        /// Implementação base já possui confirmação.
        /// </summary>
        protected virtual void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (!FormMsg.Confirm(
                "Deseja realmente excluir o registro selecionado?"))
                return;
        }
    }
}
