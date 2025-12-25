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
    /// - Retry opcional (explicitamente controlado)
    /// - Detalhe técnico opcional para diagnóstico visual
    ///
    /// NÃO FAZ:
    /// - Não captura exceções
    /// - Não executa retry
    /// - Não conhece serviços, logger ou regras de negócio
    /// - Não decide visibilidade automaticamente
    /// </summary>
    public sealed class GsErrorState
    {
        /// <summary>
        /// Título do erro (contexto resumido).
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Mensagem amigável ao usuário final.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Detalhe técnico opcional para diagnóstico visual.
        /// Pode conter stacktrace, mensagem técnica ou contexto.
        /// </summary>
        public string TechnicalDetails { get; }

        /// <summary>
        /// Indica explicitamente se o diagnóstico técnico
        /// pode ser exibido visualmente.
        ///
        /// REGRA:
        /// - Se false, detalhes técnicos NUNCA são exibidos,
        ///   mesmo que existam.
        /// - Não há inferência automática.
        /// </summary>
        public bool AllowDiagnostics { get; }

        /// <summary>
        /// Texto do botão de retry (opcional).
        /// </summary>
        public string RetryText { get; }

        /// <summary>
        /// Ação de retry (opcional).
        /// </summary>
        public Action RetryAction { get; }

        /// <summary>
        /// Construtor completo do estado de erro semântico.
        /// </summary>
        public GsErrorState(
            string title,
            string message,
            string technicalDetails = null,
            bool allowDiagnostics = false,
            string retryText = null,
            Action retryAction = null)
        {
            Title = title;
            Message = message;
            TechnicalDetails = technicalDetails;
            AllowDiagnostics = allowDiagnostics;
            RetryText = retryText;
            RetryAction = retryAction;
        }
    }
}
