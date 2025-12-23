using System;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// Argumentos disparados quando o grid solicita uma ação.
    /// </summary>
    public sealed class GsGridActionEventArgs : EventArgs
    {
        public GsGridActionEventArgs(
            GsGridAction action,
            int rowIndex,
            object item)
        {
            Action = action;
            RowIndex = rowIndex;
            Item = item;
        }

        /// <summary>
        /// Ação solicitada.
        /// </summary>
        public GsGridAction Action { get; }

        /// <summary>
        /// Índice da linha selecionada.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Item associado à linha (DataBoundItem).
        /// </summary>
        public object Item { get; }
    }
}
