using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// DataGridView padronizado do GS Core UI.
    /// Responsável APENAS por exibição e interação visual.
    /// 
    /// NÃO conhece:
    /// - banco de dados
    /// - regras de negócio
    /// - serviços
    /// </summary>
    public class GsDataGridView : DataGridView, IThemedControl
    {
        // ==========================================================
        // EVENTOS
        // ==========================================================
        /// <summary>
        /// Definições semânticas das colunas do grid.
        /// </summary>
        public IList<GsGridColumn> ColumnsDefinition { get; }
            = new List<GsGridColumn>();

        /// <summary>
        /// Evento legado (mantido por compatibilidade).
        /// </summary>
        public event EventHandler EditarSolicitado;

        /// <summary>
        /// Evento moderno e semântico de ação solicitada.
        /// </summary>
        public event EventHandler<GsGridActionEventArgs> ActionRequested;

        // ==========================================================
        // CAMPOS PRIVADOS
        // ==========================================================

        private Font _headerFont;
        private GsGridState _state = GsGridState.Ready;

        // ==========================================================
        // PROPRIEDADES DE UX
        // ==========================================================

        /// <summary>
        /// Estado atual do grid.
        /// Deve ser controlado pelo container.
        /// </summary>
        public GsGridState State
        {
            get => _state;
            set
            {
                _state = value;
                Enabled = _state == GsGridState.Ready;
                Invalidate();
            }
        }

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public GsDataGridView()
        {
            InicializarComportamento();
        }

        // ==========================================================
        // CONFIGURAÇÃO BASE
        // ==========================================================
        /// <summary>
        /// Reconstrói as colunas reais do DataGridView
        /// a partir das definições semânticas.
        /// </summary>
        public void BuildColumns()
        {
            Columns.Clear();

            foreach (var def in ColumnsDefinition)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    HeaderText = def.Header,
                    DataPropertyName = def.PropertyName,
                    Width = def.Width,
                    Visible = def.Visible,
                    ReadOnly = def.ReadOnly,
                    DefaultCellStyle =
            {
                Alignment = def.Alignment
            }
                };

                Columns.Add(column);
            }
        }

        private void InicializarComportamento()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;

            MultiSelect = false;
            ReadOnly = true;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            RowHeadersVisible = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            EnableHeadersVisualStyles = false;

            DoubleBuffered = true;

            // Duplo clique → ação padrão (Edit)
            CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex >= 0)
                    DispararAcao(GsGridAction.Edit, e.RowIndex);
            };

            // Enter → ação padrão
            KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter && CurrentRow != null)
                {
                    e.Handled = true;
                    DispararAcao(
                        GsGridAction.Edit,
                        CurrentRow.Index
                    );
                }
            };
        }

        // ==========================================================
        // DISPARO DE AÇÃO
        // ==========================================================

        private void DispararAcao(GsGridAction action, int rowIndex)
        {
            var item = Rows[rowIndex]?.DataBoundItem;

            // Evento moderno
            ActionRequested?.Invoke(
                this,
                new GsGridActionEventArgs(action, rowIndex, item)
            );

            // Evento legado
            EditarSolicitado?.Invoke(this, EventArgs.Empty);
        }

        // ==========================================================
        // THEME
        // ==========================================================

        public void ApplyTheme(GsTheme theme)
        {
            if (theme == null)
                return;

            Font = theme.DefaultFont;
            _headerFont ??= new Font(theme.DefaultFont, FontStyle.Bold);

            BackgroundColor = theme.Surface;
            GridColor = theme.Border;
            BorderStyle = BorderStyle.None;

            // Linhas
            DefaultCellStyle.BackColor = theme.Surface;
            DefaultCellStyle.ForeColor = theme.TextPrimary;
            DefaultCellStyle.SelectionBackColor = theme.GridSelection;
            DefaultCellStyle.SelectionForeColor = theme.GridSelectionText;

            AlternatingRowsDefaultCellStyle.BackColor = theme.SurfaceAlt;
            AlternatingRowsDefaultCellStyle.ForeColor = theme.TextPrimary;
            AlternatingRowsDefaultCellStyle.SelectionBackColor = theme.GridSelection;
            AlternatingRowsDefaultCellStyle.SelectionForeColor = theme.GridSelectionText;

            // Cabeçalho
            ColumnHeadersDefaultCellStyle.BackColor = theme.GridHeaderBackground;
            ColumnHeadersDefaultCellStyle.ForeColor = theme.GridHeaderText;
            ColumnHeadersDefaultCellStyle.Font = _headerFont;
            ColumnHeadersHeight = 36;

            // Células
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            RowTemplate.Height = 32;
        }

        // ==========================================================
        // ESTADOS DE UX (RENDER)
        // ==========================================================

        
        }
    }


