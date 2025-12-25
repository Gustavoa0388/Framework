using System.Collections.Generic;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsSkeletonLayout
    ///
    /// Descreve semanticamente o layout de um Skeleton.
    ///
    /// REPRESENTA:
    /// - Quantidade de linhas
    /// - Altura das linhas
    /// - Colunas (largura relativa)
    ///
    /// NÃO FAZ:
    /// - Não renderiza
    /// - Não conhece dados
    /// - Não controla tempo
    /// </summary>
    public sealed class GsSkeletonLayout
    {
        /// <summary>
        /// Quantidade de linhas do skeleton.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Altura de cada linha.
        /// </summary>
        public int RowHeight { get; }

        /// <summary>
        /// Larguras relativas das colunas (percentual).
        /// Exemplo: [30, 40, 30]
        /// </summary>
        public IReadOnlyList<int> Columns { get; }

        public GsSkeletonLayout(int rows, int rowHeight, IReadOnlyList<int> columns)
        {
            Rows = rows;
            RowHeight = rowHeight;
            Columns = columns;
        }
    }
}
