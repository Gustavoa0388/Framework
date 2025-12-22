using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// DataGridView padronizado do GS Core.
    /// Responsável apenas por comportamento e aparência.
    /// Não contém regra de negócio.
    /// </summary>
    public class GsDataGridView : DataGridView, IThemedControl
    {
        // =============================
        // EVENTOS PADRÃO
        // =============================
        public event EventHandler EditarSolicitado;

        public GsDataGridView()
        {
            InicializarComportamento();
        }

        // =============================
        // CONFIGURAÇÃO BASE
        // =============================
        private void InicializarComportamento()
        {
            // Comportamento geral
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            MultiSelect = false;
            ReadOnly = true;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EnableHeadersVisualStyles = false;

            // Performance / UX
            DoubleBuffered = true;

            // Evento de duplo clique
            CellDoubleClick += (_, __) =>
            {
                EditarSolicitado?.Invoke(this, EventArgs.Empty);
            };
        }

        // =============================
        // THEME
        // =============================
        public void ApplyTheme(GsTheme theme)
        {
            if (theme == null)
                return;

            Font = theme.DefaultFont;

            // Fundo geral
            BackgroundColor = theme.GridBackground;
            GridColor = theme.GridBorder;

            // =============================
            // LINHAS
            // =============================
            DefaultCellStyle.BackColor = theme.GridSurface;
            DefaultCellStyle.ForeColor = theme.TextPrimary;
            DefaultCellStyle.SelectionBackColor = theme.GridRowSelected;
            DefaultCellStyle.SelectionForeColor = theme.GridRowSelectedText;

            AlternatingRowsDefaultCellStyle.BackColor = theme.GridSurfaceAlt;
            AlternatingRowsDefaultCellStyle.ForeColor = theme.TextPrimary;
            AlternatingRowsDefaultCellStyle.SelectionBackColor = theme.GridRowSelected;
            AlternatingRowsDefaultCellStyle.SelectionForeColor = theme.GridRowSelectedText;

            // =============================
            // CABEÇALHO
            // =============================
            ColumnHeadersDefaultCellStyle.BackColor = theme.GridHeaderBackground;
            ColumnHeadersDefaultCellStyle.ForeColor = theme.GridHeaderText;
            ColumnHeadersDefaultCellStyle.Font = new Font(
                theme.DefaultFont,
                FontStyle.Bold
            );

            ColumnHeadersHeight = 36;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // =============================
            // CÉLULAS
            // =============================
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            BorderStyle = BorderStyle.None;

            // =============================
            // ROW TEMPLATE
            // =============================
            RowTemplate.Height = 32;
        }
    }
}
