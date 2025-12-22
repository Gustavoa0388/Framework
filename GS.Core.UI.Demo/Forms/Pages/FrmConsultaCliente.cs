using GS.Core.UI.Controls;
using GS.Core.UI.Forms;
using System;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// Tela de consulta de clientes.
    /// Exemplo funcional usando GS Core.
    /// </summary>
    public partial class FrmConsultaCliente : FormBaseConsulta
    {

        public FrmConsultaCliente()
        {

            Text = "Consulta de Clientes";

            ConfigurarGrid();
            CarregarDados();
        }

        private List<object> _clientes;


        // ==========================================================
        // GRID
        // ==========================================================
        private void ConfigurarGrid()
        {
            Grid.AutoGenerateColumns = false;
            Grid.Columns.Clear();

            Grid.Columns.Add(GsGridColumn.Id("Id").Build());
            Grid.Columns.Add(GsGridColumn.Text("Nome", "Nome", "Nome").Build());
            Grid.Columns.Add(GsGridColumn.Email("Email").Build());
        }

        // ==========================================================
        // DADOS (mock local)
        // ==========================================================
        protected override void CarregarDados()
        {
            _clientes = new List<object>
    {
        new { Id = 1, Nome = "João Silva", Email = "joao@email.com" },
        new { Id = 2, Nome = "Maria Souza", Email = "maria@email.com" },
        new { Id = 3, Nome = "Carlos Pereira", Email = "carlos@email.com" },
        // simule mais dados aqui
    };

            AtualizarPaginacao(_clientes);
        }


        // ==========================================================
        // AÇÕES
        // ==========================================================
        protected override void OnNovoClick(object sender, EventArgs e)
        {
            FormMsg.Info("Novo cliente (exemplo)");
        }

        protected override void OnEditarClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            FormMsg.Info("Editar cliente selecionado");
        }

        protected override void OnExcluirClick(object sender, EventArgs e)
        {
            base.OnExcluirClick(sender, e);

            if (Grid.CurrentRow == null)
                return;

            // Exclusão mock
            FormMsg.Success("Cliente excluído (exemplo)");
        }
    }
}
