namespace GS.Core.UI.Forms.Navigation
{
    /// <summary>
    /// Resultado semântico de navegação entre formulários.
    ///
    /// RESPONSABILIDADE:
    /// - Representar COMO uma tela foi encerrada
    ///
    /// NÃO FAZ:
    /// - Não fecha formulário
    /// - Não decide fluxo
    /// - Não executa ações
    /// </summary>
    public class FormResult
    {
        /// <summary>
        /// Tipo do resultado.
        /// </summary>
        public FormResultType ResultType { get; }

        protected FormResult(FormResultType resultType)
        {
            ResultType = resultType;
        }

        // ============================
        // FÁBRICAS SEMÂNTICAS
        // ============================

        public static FormResult None()
            => new(FormResultType.None);

        public static FormResult Saved()
            => new(FormResultType.Saved);

        public static FormResult Canceled()
            => new(FormResultType.Canceled);

        public static FormResult Closed()
            => new(FormResultType.Closed);

        // ============================
        // HELPERS DE LEITURA
        // ============================

        public bool IsSaved => ResultType == FormResultType.Saved;
        public bool IsCanceled => ResultType == FormResultType.Canceled;
        public bool IsClosed => ResultType == FormResultType.Closed;
        public bool HasResult => ResultType != FormResultType.None;
    }
}
