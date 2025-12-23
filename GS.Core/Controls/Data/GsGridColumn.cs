using System.Windows.Forms;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// Representa a definição SEMÂNTICA de uma coluna do grid.
    /// 
    /// RESPONSABILIDADE:
    /// - Descrever a coluna (header, binding, alinhamento, largura).
    /// - NÃO renderiza.
    /// - NÃO conhece tema.
    /// - NÃO conhece DataGridView diretamente.
    /// 
    /// O GsDataGridView é responsável por converter
    /// esta definição em uma coluna real.
    /// </summary>
    public class GsGridColumn
    {
        /// <summary>
        /// Texto exibido no cabeçalho da coluna.
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// Nome da propriedade usada para binding.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Largura da coluna em pixels.
        /// </summary>
        public int Width { get; set; } = 120;

        /// <summary>
        /// Alinhamento do conteúdo da célula.
        /// </summary>
        public DataGridViewContentAlignment Alignment { get; set; }
            = DataGridViewContentAlignment.MiddleLeft;

        /// <summary>
        /// Indica se a coluna está visível.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Indica se a coluna é somente leitura.
        /// </summary>
        public bool ReadOnly { get; set; } = true;

        /// <summary>
        /// Cria uma nova definição de coluna.
        /// </summary>
        /// <param name="header">Texto do cabeçalho.</param>
        /// <param name="propertyName">Nome da propriedade de binding.</param>
        public GsGridColumn(string header, string propertyName)
        {
            Header = header;
            PropertyName = propertyName;
        }
    }
}
