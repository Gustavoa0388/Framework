namespace GS.Core.UI.Forms.Navigation
{
    /// <summary>
    /// Tipos semânticos de resultado de um formulário.
    ///
    /// REPRESENTA:
    /// - A INTENÇÃO com que a tela foi encerrada
    ///
    /// NÃO REPRESENTA:
    /// - DialogResult
    /// - Estado visual
    /// - Estado de UX
    /// </summary>
    public enum GsFormResultType
    {
        /// <summary>
        /// Nenhum resultado relevante.
        /// </summary>
        None,

        /// <summary>
        /// Registro salvo com sucesso.
        /// </summary>
        Saved,

        /// <summary>
        /// Operação cancelada explicitamente pelo usuário.
        /// </summary>
        Canceled,

        /// <summary>
        /// Tela apenas foi fechada (X, ESC, etc).
        /// </summary>
        Closed,

        /// <summary>
        /// Um item foi selecionado e retornado.
        /// Usado em conjunto com FormResult&lt;T&gt;.
        /// </summary>
        Selected
    }
}
