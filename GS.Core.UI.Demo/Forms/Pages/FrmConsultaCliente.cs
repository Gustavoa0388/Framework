using GS.Core.UI.Controls.Data;
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
            // Inicialização do Designer (OBRIGATÓRIO)
            InitializeComponent();

            CarregarDados();
            ConfigurarGrid();
            
            
            Text = "Consulta de Clientes";
            
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
                    Width = 80,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                });

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("Nome", "Nome")
                {
                    Width = 250
                });

            Grid.ColumnsDefinition.Add(
                new GsGridColumn("E-mail", "Email")
                {
                    Width = 250
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
            var todos = new List<object>
    {
        new { Id = 1, Nome = "João Silva", Email = "joao@email.com" },
        new { Id = 2, Nome = "Paulo Moura", Email = "paulo@email.com" },
        new { Id = 3, Nome = "Maria Souza", Email = "maria@email.com" },
        new { Id = 4, Nome = "Carlos Pereira", Email = "carlos@email.com" },
        new { Id = 5, Nome = "Marcela Costa", Email = "marcela@email.com" },
        new { Id = 6, Nome = "Maria Tavares", Email = "mariat@email.com" },
        new { Id = 7, Nome = "Rogério Maia", Email = "rogerio@email.com" },
        new { Id = 8, Nome = "Ana Paula Rossi", Email = "anap@email.com" },
        new { Id = 9, Nome = "João Rocha", Email = "joaor@email.com" },
        new { Id = 10, Nome = "Marina Silva", Email = "marina@email.com" },
        new { Id = 11, Nome = "Carla Silva", Email = "carla@email.com" },
        new { Id = 12, Nome = "Paula Silva", Email = "paula@email.com" },
    };

            // 🔎 APLICA FILTRO
            if (!string.IsNullOrWhiteSpace(TextoFiltro))
            {
                var filtro = TextoFiltro.ToLower();

                todos = todos.FindAll(c =>
                    c.GetType().GetProperty("Nome")?
                     .GetValue(c)?
                     .ToString()?
                     .ToLower()
                     .Contains(filtro) == true
                );
            }

            AtualizarPaginacao(todos);
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
    }
}
