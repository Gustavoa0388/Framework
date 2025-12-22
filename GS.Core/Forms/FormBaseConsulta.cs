using GS.Core.UI.Forms;
using System;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    public partial class FormBaseConsulta : GsBaseForm
    {
        // 🔹 CONTROLES BASE (existem para os filhos)
        protected TextBox txtFiltro;
        protected DataGridView Grid;

        public FormBaseConsulta()
        {
            InitializeComponent();
            InicializarBase();
        }

        private void InicializarBase()
        {
            // Campo de filtro
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

            // Grid padrão
            Grid = new DataGridView
            {
                Left = 12,
                Top = txtFiltro.Bottom + 8,
                Width = ClientSize.Width - 24,
                Height = ClientSize.Height - txtFiltro.Bottom - 20,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Controls.Add(txtFiltro);
            Controls.Add(Grid);
        }

        // ==============================
        // CONTRATO PARA OS FILHOS
        // ==============================

        protected virtual void OnPesquisarClick()
        {
            CarregarDados();
        }

        protected virtual void CarregarDados()
        {
            // Implementado no form filho
        }

        protected virtual bool ConfirmarExclusao()
        {
            return MessageBox.Show(
                "Deseja realmente excluir o registro?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }
    }
}
