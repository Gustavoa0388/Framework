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

            CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex < 0)
                    return;

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
            BackgroundColor = theme.Surface;
            GridColor = theme.Border;
            BorderStyle = BorderStyle.None;

            // =============================
            // LINHAS
            // =============================
            DefaultCellStyle.BackColor = theme.Surface;
            DefaultCellStyle.ForeColor = theme.TextPrimary;
            DefaultCellStyle.SelectionBackColor = theme.GridSelection;
            DefaultCellStyle.SelectionForeColor = theme.GridSelectionText;

            AlternatingRowsDefaultCellStyle.BackColor = theme.SurfaceAlt;
            AlternatingRowsDefaultCellStyle.ForeColor = theme.TextPrimary;
            AlternatingRowsDefaultCellStyle.SelectionBackColor = theme.GridSelection;
            AlternatingRowsDefaultCellStyle.SelectionForeColor = theme.GridSelectionText;

            // =============================
            // CABEÇALHO
            // =============================
            ColumnHeadersDefaultCellStyle.BackColor = theme.GridHeaderBackground;
            ColumnHeadersDefaultCellStyle.ForeColor = theme.GridHeaderText;
            ColumnHeadersDefaultCellStyle.Font = new Font(
                theme.DefaultFont,
                FontStyle.Bold
            );
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ColumnHeadersHeight = 36;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // =============================
            // CÉLULAS
            // =============================
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // =============================
            // ROW TEMPLATE
            // =============================
            RowTemplate.Height = 32;
        }
    }
}
