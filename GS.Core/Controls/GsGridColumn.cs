using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Representa uma definição de coluna para o GsDataGridView.
    /// 
    /// IMPORTANTE:
    /// - Esta classe NÃO é um Control.
    /// - Atua apenas como builder/configurador de colunas.
    /// </summary>
    public class GsGridColumn
    {
        /// <summary>
        /// Nome interno da coluna (Name).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Texto exibido no cabeçalho.
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// Propriedade do objeto de dados vinculada à coluna.
        /// </summary>
        public string DataProperty { get; }

        public int Width { get; set; } = 100;
        public bool AutoSize { get; set; }
        public bool Visible { get; set; } = true;
        public DataGridViewContentAlignment Alignment { get; set; } = DataGridViewContentAlignment.MiddleLeft;
        public string Format { get; set; }

        public GsGridColumn(string name, string header, string dataProperty)
        {
            Name = name;
            Header = header;
            DataProperty = dataProperty;
        }

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
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            else
                col.Width = Width;

            col.DefaultCellStyle.Alignment = Alignment;

            if (!string.IsNullOrWhiteSpace(Format))
                col.DefaultCellStyle.Format = Format;

            return col;
        }

        // =============================
        // FACTORIES PADRÃO
        // =============================

        public static GsGridColumn Id(string dataProperty) =>
            new("Id", "Código", dataProperty)
            {
                Width = 80,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

        public static GsGridColumn Text(string name, string header, string dataProperty) =>
            new(name, header, dataProperty) { AutoSize = true };

        public static GsGridColumn Email(string dataProperty) =>
            new("Email", "E-mail", dataProperty) { Width = 220 };

        public static GsGridColumn Money(string name, string header, string dataProperty) =>
            new(name, header, dataProperty)
            {
                Width = 120,
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "C2"
            };

        public static GsGridColumn Date(string name, string header, string dataProperty) =>
            new(name, header, dataProperty)
            {
                Width = 110,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Format = "dd/MM/yyyy"
            };
    }
}
