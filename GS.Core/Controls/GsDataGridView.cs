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

        public void ApplyTheme(GsTheme theme)
        {
            // Fundo geral
            BackgroundColor = theme.BackColor;
            GridColor = theme.BackColor;

            // Células
            DefaultCellStyle.BackColor = theme.BackColor;
            DefaultCellStyle.ForeColor = theme.ForeColor;
            DefaultCellStyle.Font = theme.DefaultFont;

            // Linhas alternadas (leve contraste)
            AlternatingRowsDefaultCellStyle.BackColor =
                ControlPaint.Light(theme.BackColor, 0.04f);

            // Seleção (usa ForeColor para manter contraste)
            DefaultCellStyle.SelectionBackColor =
                ControlPaint.Dark(theme.BackColor, 0.15f);
            DefaultCellStyle.SelectionForeColor = theme.ForeColor;

            // Cabeçalho
            ColumnHeadersDefaultCellStyle.BackColor = theme.BackColor;
            ColumnHeadersDefaultCellStyle.ForeColor = theme.ForeColor;
            ColumnHeadersDefaultCellStyle.Font =
                new Font(theme.DefaultFont, FontStyle.Bold);
        }
    }
}
