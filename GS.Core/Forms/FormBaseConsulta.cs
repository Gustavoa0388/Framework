using GS.Core.UI.Controls;
using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    public partial class FormBaseConsulta : GsBaseForm
    {
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

            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    OnPesquisarClick();
            };

            pnlFiltro.Controls.Add(txtFiltro);

            // =============================
            // GRID
            // =============================
            Grid = new GsDataGridView
            {
                Dock = DockStyle.Fill
            };

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
