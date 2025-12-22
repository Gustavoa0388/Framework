using GS.Core.UI.Controls;
using GS.Core.UI.Forms;
using System;

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
            Grid.Columns.Clear();

            Grid.Columns.Add(GsGridColumn.Id("Id").Build());
            Grid.Columns.Add(GsGridColumn.Text("Nome", "Nome", "Nome").Build());
            Grid.Columns.Add(GsGridColumn.Email("Email").Build());
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
