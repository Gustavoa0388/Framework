using System;
using System.Drawing;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsEmptyState
    ///
    /// Modelo semântico que descreve um estado de tela vazia.
    ///
    /// REPRESENTA:
    /// - Intenção do vazio (sem dados, filtro, primeiro acesso, etc.)
    ///
    /// NÃO FAZ:
    /// - Não renderiza UI
    /// - Não decide fluxo
    /// - Não acessa serviços
    /// </summary>
    public sealed class GsEmptyState
    {
        public string Title { get; }
        public string Message { get; }
        public Image Icon { get; }
        public string ActionText { get; }
        public Action Action { get; }

        public GsEmptyState(
            string title,
            string message,
            Image icon = null,
            string actionText = null,
            Action action = null)
        {
            Title = title;
            Message = message;
            Icon = icon;
            ActionText = actionText;
            Action = action;
        }
    }
}
