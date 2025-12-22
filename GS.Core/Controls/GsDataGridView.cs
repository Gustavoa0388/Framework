using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// DataGridView padrão do GS Core.
    /// Compatível com o ThemeManager atual.
    /// </summary>
    public class GsDataGridView : DataGridView, IThemedControl
    {
        public void ApplyTheme(GsTheme theme)
        {
            if (theme == null)
                return;

            ApplyThemeInternal(theme);
        }
        public event EventHandler EditarSolicitado;

        public GsDataGridView()
        {
            ReadOnly = true;
            MultiSelect = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;

            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            AutoGenerateColumns = false;

            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            EnableHeadersVisualStyles = false;

            RowTemplate.Height = 28;

            DoubleClick += (_, _) =>
            {
                if (CurrentRow != null)
                    EditarSolicitado?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ApplyThemeInternal(GsTheme theme)
        {
            // =============================
            // COMPORTAMENTO (CRÍTICO)
            // =============================
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = false;
            EnableHeadersVisualStyles = false;
            ReadOnly = true;

            // =============================
            // GRID GERAL
            // =============================
            BackgroundColor = theme.BackColor;
            GridColor = theme.Border;
            BorderStyle = BorderStyle.None;

            // =============================
            // HEADER
            // =============================
            ColumnHeadersDefaultCellStyle.BackColor = theme.Primary;
            ColumnHeadersDefaultCellStyle.ForeColor = theme.ForeColor;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = theme.Primary;
            ColumnHeadersDefaultCellStyle.SelectionForeColor = theme.ForeColor;

            ColumnHeadersHeight = 32;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // =============================
            // LINHAS NORMAIS
            // =============================
            DefaultCellStyle.BackColor = theme.BackColor;
            DefaultCellStyle.ForeColor = theme.ForeColor;
            DefaultCellStyle.SelectionBackColor = theme.Primary;
            DefaultCellStyle.SelectionForeColor = theme.ForeColor;

            // =============================
            // LINHAS ALTERNADAS
            // =============================
            AlternatingRowsDefaultCellStyle.BackColor = theme.InputBackground;
            AlternatingRowsDefaultCellStyle.ForeColor = theme.ForeColor;
            AlternatingRowsDefaultCellStyle.SelectionBackColor = theme.Primary;
            AlternatingRowsDefaultCellStyle.SelectionForeColor = theme.ForeColor;

            // =============================
            // OUTROS AJUSTES VISUAIS
            // =============================
            RowHeadersVisible = false;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }
    }
}


    

