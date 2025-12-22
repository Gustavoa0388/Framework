using System;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// Tela de consulta de clientes.
    /// Compatível com FormBaseConsulta atual.
    /// </summary>
    public partial class FrmConsultaCliente : FormBaseConsulta
    {
        public FrmConsultaCliente()
        {
            InitializeComponent();

            Text = "Consulta de Clientes";

            ConfigurarGrid();
            CarregarDadosLocal();
        }

        // ==========================================================
        // GRID
        // ==========================================================
        private void ConfigurarGrid()
        {
            Grid.AutoGenerateColumns = false;
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Grid.Columns.Clear();

            Grid.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Código",
                DataPropertyName = "Id",
                Width = 80
            });

            Grid.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            });

            Grid.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "E-mail",
                DataPropertyName = "Email",
                Width = 220
            });
        }

        // ==========================================================
        // DADOS (LOCAL, SEM OVERRIDE)
        // ==========================================================
        private void CarregarDadosLocal()
        {
            var lista = new[]
            {
                new { Id = 1, Nome = "João Silva", Email = "joao@email.com" },
                new { Id = 2, Nome = "Maria Souza", Email = "maria@email.com" },
                new { Id = 3, Nome = "Carlos Pereira", Email = "carlos@email.com" }
            };

            Grid.DataSource = lista;
        }

        // ==========================================================
        // AÇÕES
        // ==========================================================
        protected override void OnNovoClick(object sender, EventArgs e)
        {
            using (var frm = new FrmCadastroCliente())
            {
                frm.ShowDialog();
            }

            CarregarDadosLocal();
        }

        protected override void OnEditarClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            // Enquanto não houver construtor com ID
            using (var frm = new FrmCadastroCliente())
            {
                frm.ShowDialog();
            }

            CarregarDadosLocal();
        }

        protected override void OnExcluirClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (!FormMsg.Confirm("Deseja excluir o cliente selecionado?"))
                return;

            // Exclusão futura aqui

            CarregarDadosLocal();
        }
    }
}
