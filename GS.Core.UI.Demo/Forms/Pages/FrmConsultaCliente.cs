using GS.Core.UI.Controls.Data;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Forms;
using System;
using System.Collections.Generic;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmConsultaCliente
    /// 
    /// Tela de consulta de clientes (DEMO).
    /// 
    /// OBJETIVO:
    /// - Demonstrar uso do FormBaseConsulta
    /// - Validar grid GS Core + paginação
    /// - Exercitar ações padrão (Novo / Editar / Excluir)
    /// 
    /// STATUS:
    /// - DEMO
    /// - Dados mockados
    /// - Não acessa banco real
    /// 
    /// OBSERVAÇÃO IMPORTANTE:
    /// - Utiliza Windows Forms Designer
    /// - Herda layout base e grid do FormBaseConsulta
    /// </summary>
    public partial class FrmConsultaCliente : FormBaseConsulta
    {             

        // ==========================================================
        // DADOS (MOCK)
        // ==========================================================

        private List<object> _clientes;


        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public FrmConsultaCliente()
        {
            Text = "Consulta de Clientes";

            // Tamanho inicial e mínimo (OBRIGATÓRIO)
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(550, 550);
            MinimumSize = new Size(400, 500);

            ConfigurarGrid();

            // PRIMEIRA CARGA DE DADOS
            ExecutarBusca();
        }

        // ==========================================================
        // CONFIGURAÇÃO DO GRID
        // ==========================================================

        /// <summary>
        /// Configura as colunas do grid.
        /// 
        /// OBS:
        /// - AutoGenerateColumns desativado propositalmente
        /// - Uso de GsGridColumn valida a API do GS Core
        /// </summary>
        private void ConfigurarGrid()
        {
            Grid.AutoGenerateColumns = false;

            // Limpa qualquer configuração anterior
            Grid.Columns.Clear();
            Grid.ColumnsDefinition.Clear();

            // ============================
            // DEFINIÇÃO DAS COLUNAS
            // ============================

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("Código", "Id")
                {
                    Width = 60,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                });

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("Nome", "Nome")
                {
                    Width = 200
                });

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("E-mail", "Email")
                {
                    Width = 200
                });

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("Ativo", "Ativo")
                {
                    Width = 60
                });

            // ============================
            // CONSTRÓI AS COLUNAS REAIS
            // ============================

            Grid.BuildColumns();
        }


        // ==========================================================
        // CARREGAMENTO DE DADOS
        // ==========================================================

        /// <summary>
        /// Carrega dados no grid.
        /// 
        /// Neste DEMO:
        /// - Dados são mockados em memória
        /// - Serve apenas para validar paginação e grid
        /// </summary>
        protected override void CarregarDados()
        {
            try
            {
                StateView.State = GsUxState.Loading;
                StateView.Message = "Carregando clientes...";
                StateView.Visible = true;

                var clientes = new List<ClienteDto>
        {
            new ClienteDto { Id = 1, Nome = "João Silva", Email = "joao@email.com", Ativo = true },
            new ClienteDto { Id = 2, Nome = "Paulo Moura", Email = "paulo@email.com", Ativo = true },
            new ClienteDto { Id = 3, Nome = "Maria Souza", Email = "maria@email.com", Ativo = false },
            new ClienteDto { Id = 4, Nome = "Carlos Pereira", Email = "carlos@email.com", Ativo = true },
            new ClienteDto { Id = 5, Nome = "Marcela Costa", Email = "marcela@email.com", Ativo = true },
            new ClienteDto { Id = 6, Nome = "Teste 6", Email = "teste6@email.com", Ativo = true },
            new ClienteDto { Id = 7, Nome = "Teste 7", Email = "teste7@email.com", Ativo = true },
            new ClienteDto { Id = 8, Nome = "Teste 8", Email = "teste8@email.com", Ativo = false },
            new ClienteDto { Id = 9, Nome = "Teste 9", Email = "teste9@email.com", Ativo = true },
            new ClienteDto { Id = 10, Nome = "Teste 10", Email = "teste10@email.com", Ativo = true },
            new ClienteDto { Id = 11, Nome = "Teste 11", Email = "teste11@email.com", Ativo = true },
            new ClienteDto { Id = 12, Nome = "Teste 12", Email = "teste12@email.com", Ativo = true },
        };

                // 🔑 AQUI É O CORAÇÃO DA PAGINAÇÃO
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

        /// <summary>
        /// Ação "Novo".
        /// 
        /// DEMO:
        /// - Apenas exibe mensagem
        /// </summary>
        protected override void OnNovoClick(object sender, EventArgs e)
        {
            FormMsg.Info("Novo cliente (exemplo)");
        }

        /// <summary>
        /// Ação "Editar".
        /// </summary>
        protected override void OnEditarClick(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            FormMsg.Info("Editar cliente selecionado");
        }

        /// <summary>
        /// Ação "Excluir".
        /// </summary>
        protected override void OnExcluirClick(object sender, EventArgs e)
        {
            base.OnExcluirClick(sender, e);

            if (Grid.CurrentRow == null)
                return;

            // Exclusão mock
            FormMsg.Success("Cliente excluído (exemplo)");
        }

        // =====================================================
        // DTO
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
    

