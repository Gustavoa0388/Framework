using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Representa uma coluna padronizada para o GsDataGridView.
    /// Centraliza tipo, alinhamento, largura e formatação.
    /// </summary>
    public class GsGridColumn
    {
        // =============================
        // PROPRIEDADES
        // =============================
        public string Name { get; }
        public string Header { get; }
        public string DataProperty { get; }

        public int Width { get; set; } = 100;
        public bool AutoSize { get; set; } = false;
        public bool Visible { get; set; } = true;
        public DataGridViewContentAlignment Alignment { get; set; } = DataGridViewContentAlignment.MiddleLeft;
        public string Format { get; set; }

        // =============================
        // CONSTRUTOR
        // =============================
        public GsGridColumn(string name, string header, string dataProperty)
        {
            Name = name;
            Header = header;
            DataProperty = dataProperty;
        }

        // =============================
        // CRIA A COLUNA REAL
        // =============================
        public DataGridViewColumn Build()
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = Name,
                HeaderText = Header,
                DataPropertyName = DataProperty,
                Visible = Visible,
                ReadOnly = true
            };

            if (AutoSize)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                col.Width = Width;
            }

            col.DefaultCellStyle.Alignment = Alignment;

            if (!string.IsNullOrWhiteSpace(Format))
                col.DefaultCellStyle.Format = Format;

            return col;
        }

        // =============================
        // FACTORIES (atalhos)
        // =============================

        public static GsGridColumn Id(string dataProperty)
        {
            return new GsGridColumn("Id", "Código", dataProperty)
            {
                Width = 80,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
        }

        public static GsGridColumn Text(string name, string header, string dataProperty)
        {
            return new GsGridColumn(name, header, dataProperty)
            {
                AutoSize = true
            };
        }

        public static GsGridColumn Email(string dataProperty)
        {
            return new GsGridColumn("Email", "E-mail", dataProperty)
            {
                Width = 220
            };
        }

        public static GsGridColumn Money(string name, string header, string dataProperty)
        {
            return new GsGridColumn(name, header, dataProperty)
            {
                Width = 120,
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "C2"
            };
        }

        public static GsGridColumn Date(string name, string header, string dataProperty)
        {
            return new GsGridColumn(name, header, dataProperty)
            {
                Width = 110,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Format = "dd/MM/yyyy"
            };
        }
    }
}
