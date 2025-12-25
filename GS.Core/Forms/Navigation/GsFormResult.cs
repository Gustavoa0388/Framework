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
    public class GsFormResult
    {
        /// <summary>
        /// Tipo do resultado.
        /// </summary>
        public GsFormResultType ResultType { get; }

        protected GsFormResult(GsFormResultType resultType)
        {
            ResultType = resultType;
        }

        // ============================
        // FÁBRICAS SEMÂNTICAS
        // ============================

        public static GsFormResult None()
            => new(GsFormResultType.None);

        public static GsFormResult Saved()
            => new(GsFormResultType.Saved);

        public static GsFormResult Canceled()
            => new(GsFormResultType.Canceled);

        public static GsFormResult Closed()
            => new(GsFormResultType.Closed);

        // ============================
        // HELPERS DE LEITURA
        // ============================

        public bool IsSaved => ResultType == GsFormResultType.Saved;
        public bool IsCanceled => ResultType == GsFormResultType.Canceled;
        public bool IsClosed => ResultType == GsFormResultType.Closed;
        public bool HasResult => ResultType != GsFormResultType.None;
    }
}
