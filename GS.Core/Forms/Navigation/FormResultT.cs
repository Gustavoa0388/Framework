namespace GS.Core.UI.Forms.Navigation
{
    /// <summary>
    /// Resultado semântico de navegação com retorno de valor.
    ///
    /// Usado para:
    /// - Seleção de registros
    /// - Lookups
    /// - Telas modais de escolha
    ///
    /// IMPORTANTE:
    /// - Não fecha formulário
    /// - Não decide fluxo
    /// - Apenas transporta intenção + valor
    /// </summary>
    /// <typeparam name="T">Tipo do valor retornado.</typeparam>
    public sealed class FormResultT<T> : FormResult
    {
        /// <summary>
        /// Valor retornado pela tela.
        /// </summary>
        public T Value { get; }

        private FormResultT(FormResultType type, T value)
            : base(type)
        {
            Value = value;
        }

        // =====================================================
        // FÁBRICAS SEMÂNTICAS
        // =====================================================

        /// <summary>
        /// Cria um resultado de seleção com valor.
        /// </summary>
        public static FormResultT<T> Selected(T value)
        {
            return new FormResultT<T>(FormResultType.Selected, value);
        }

        // =====================================================
        // HELPERS
        // =====================================================

        public bool HasValue => Value != null;
    }
}
