using System;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsErrorState
    ///
    /// Modelo semântico que descreve um erro de UX.
    ///
    /// REPRESENTA:
    /// - Mensagem clara ao usuário
    /// - Retry opcional
    /// - Detalhe técnico opcional (debug / log)
    ///
    /// NÃO FAZ:
    /// - Não executa retry
    /// - Não captura exceções
    /// - Não conhece serviços ou regras de negócio
    /// </summary>
    public sealed class GsErrorState
    {
        public string Title { get; }
        public string Message { get; }
        public string TechnicalDetails { get; }
        public string RetryText { get; }
        public Action RetryAction { get; }

        public GsErrorState(
            string title,
            string message,
            string technicalDetails = null,
            string retryText = null,
            Action retryAction = null)
        {
            Title = title;
            Message = message;
            TechnicalDetails = technicalDetails;
            RetryText = retryText;
            RetryAction = retryAction;
        }
    }
}
