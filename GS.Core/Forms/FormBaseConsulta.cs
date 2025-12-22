using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls;

namespace GS.Core.UI.Forms
{
    public partial class FormBaseConsulta : GsBaseForm
    {
        // =============================
        // CONTROLES BASE
        // =============================
        protected TextBox txtFiltro;
        protected DataGridView Grid;

        protected Panel pnlAcoes;
        protected GsButton btnNovo;
        protected GsButton btnEditar;
        protected GsButton btnExcluir;
        protected GsButton btnFechar;

        public FormBaseConsulta()
        {
            InitializeComponent();
            InicializarBase();
        }

        // =============================
        // INICIALIZAÇÃO
        // =============================
        private void InicializarBase()
        {
            // 🔹 Filtro
            txtFiltro = new TextBox
            {
                Left = 12,
                Top = 12,
                Width = 300
            };
            txtFiltro.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    OnPesquisarClick();
            };

            // 🔹 Grid
            Grid = new DataGridView
            {
                Left = 12,
                Top = txtFiltro.Bottom + 8,
                Width = ClientSize.Width - 24,
                Height = ClientSize.Height - 120,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // 🔹 Painel de ações
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

            Controls.Add(txtFiltro);
            Controls.Add(Grid);
            Controls.Add(pnlAcoes);

            PositionarBotoes();
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

            if (IsHandleCreated)
                PositionarBotoes();
        }


        private void PositionarBotoes()
        {
            if (pnlAcoes == null)
                return;

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

        protected virtual void CarregarDados()
        {
            // Implementado no form filho
        }

        protected virtual void OnNovoClick(object sender, EventArgs e) { }
        protected virtual void OnEditarClick(object sender, EventArgs e) { }

        protected virtual void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (!FormMsg.Confirm("Deseja realmente excluir o registro selecionado?"))
                return;

            // lógica no form filho
        }
    }
}
