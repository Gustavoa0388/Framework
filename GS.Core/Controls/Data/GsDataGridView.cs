using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// DataGridView padronizado do GS Core.
    /// Responsável apenas por aparência e comportamento visual.
    /// Não contém regra de negócio.
    /// </summary>
    public class GsDataGridView : DataGridView, IThemedControl
    {
        public event EventHandler EditarSolicitado;

        private Font _headerFont;

        public GsDataGridView()
        {
            InicializarComportamento();
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

            CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex >= 0)
                    EditarSolicitado?.Invoke(this, EventArgs.Empty);
            };
        }

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
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ColumnHeadersHeight = 36;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Células
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Template
            RowTemplate.Height = 32;
        }
    }
}
