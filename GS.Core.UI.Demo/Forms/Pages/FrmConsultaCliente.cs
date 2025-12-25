using GS.Core.UI.Controls.Data;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Demo.Forms.Pages;
using GS.Core.UI.Forms;
using GS.Core.UI.Forms.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmConsultaCliente
    ///
    /// Tela DEMO de consulta de clientes.
    ///
    /// OBJETIVO:
    /// - Demonstrar fluxo Consulta → Cadastro → Retorno
    /// - Validar navegação desacoplada
    /// </summary>
    public partial class FrmConsultaCliente : FormBaseConsulta
    {
        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public FrmConsultaCliente()
        {
            Text = "Consulta de Clientes";

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(550, 550);
            MinimumSize = new Size(400, 500);

            ConfigurarGrid();
            ExecutarBusca();
        }

        // ==========================================================
        // GRID
        // ==========================================================

        private void ConfigurarGrid()
        {
            Grid.AutoGenerateColumns = false;
            Grid.Columns.Clear();
            Grid.ColumnsDefinition.Clear();

            Grid.ColumnsDefinition.Add(new GsGridColumn("Código", "Id") { Width = 60 });
            Grid.ColumnsDefinition.Add(new GsGridColumn("Nome", "Nome") { Width = 200 });
            Grid.ColumnsDefinition.Add(new GsGridColumn("E-mail", "Email") { Width = 200 });
            Grid.ColumnsDefinition.Add(new GsGridColumn("Ativo", "Ativo") { Width = 60 });

            Grid.BuildColumns();
        }

        // ==========================================================
        // DADOS
        // ==========================================================

        protected override void CarregarDados()
        {
            try
            {
                StateView.State = GsUxState.Loading;
                StateView.Message = "Carregando clientes...";
                StateView.Visible = true;

                var clientes = new List<ClienteDto>
                {
                    new ClienteDto { Id = 1, Nome = "Luiz Silva", Email = "lyuiz@email.com", Ativo = true },
                    new ClienteDto { Id = 2, Nome = "Camila Silva", Email = "camila@email.com", Ativo = true },
                    new ClienteDto { Id = 3, Nome = "Carlos Pereira", Email = "carlos@email.com", Ativo = true },
                    new ClienteDto { Id = 4, Nome = "José Pinto", Email = "jose@email.com", Ativo = false },
                    new ClienteDto { Id = 5, Nome = "Marcia Lopes", Email = "marcia@email.com", Ativo = true },
                    new ClienteDto { Id = 6, Nome = "Lucas Souza", Email = "lucas@email.com", Ativo = false },
                    new ClienteDto { Id = 7, Nome = "Paula Xavier", Email = "paula@email.com", Ativo = true },
                    new ClienteDto { Id = 8, Nome = "Gustavo Silva", Email = "gustavo@email.com", Ativo = true },
                    new ClienteDto { Id = 9, Nome = "Luciane Pereira", Email = "carlos@email.com", Ativo = true },
                    new ClienteDto { Id = 10, Nome = "Ingrid Pereira", Email = "ingrid@email.com", Ativo = true },
                    new ClienteDto { Id = 11, Nome = "João Silva", Email = "joao@email.com", Ativo = false },
                    new ClienteDto { Id = 12, Nome =  "Maria Souza", Email = "maria@email.com", Ativo = false },
                    new ClienteDto { Id = 13, Nome = "Marcelo Pereira", Email = "marcelo@email.com", Ativo = true },
                    new ClienteDto { Id = 14, Nome = "Luciano Pereira", Email = "luciano@email.com", Ativo = true },
                    new ClienteDto { Id = 15, Nome = "Gabriela Pereira", Email = "gabriela@email.com", Ativo = true }
                };

                AtualizarPaginacao(clientes.Cast<object>());
            }
            catch (Exception ex)
            {
                StateView.State = GsUxState.Error;
                StateView.Message = ex.Message;
                StateView.Visible = true;
            }
        }

        // ==========================================================
        // AÇÕES
        // ==========================================================

        protected override void OnNovoClick(object sender, EventArgs e)
        {
            // Abre o cadastro via infraestrutura oficial de navegação
            var result = GsNavigationService.OpenModal<FrmCadastroCliente>(this);

            // Tela pai decide o fluxo com base no resultado
            if (result.IsSaved)
            {
                ExecutarBusca();
            }
        }


        // =====================================================
        // DTO (DEMO)
        // =====================================================

        private class ClienteDto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public bool Ativo { get; set; }
        }
    }
}
