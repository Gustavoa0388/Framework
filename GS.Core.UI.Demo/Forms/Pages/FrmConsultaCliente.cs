using System;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// Tela de consulta de clientes.
    /// Implementa FormBaseConsulta do GS Core.
    /// </summary>
    public partial class FrmConsultaCliente : FormBaseConsulta

    {
        public FrmConsultaCliente()
        {
            Text = "Consulta de Clientes";

            ConfigurarGrid();
            CarregarDados();
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
        // DADOS
        // ==========================================================
        protected override void CarregarDados()
        {
            // Mock local (sem banco)
            var lista = new[]
            {
                new { Id = 1, Nome = "João Silva", Email = "joao@email.com" },
                new { Id = 2, Nome = "Maria Souza", Email = "maria@email.com" },
                new { Id = 3, Nome = "Carlos Pereira", Email = "carlos@email.com" }
            };

            Grid.DataSource = lista;
        }
    }
}
