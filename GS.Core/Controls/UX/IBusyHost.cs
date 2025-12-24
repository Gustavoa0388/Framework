namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// IBusyHost
    ///
    /// Contrato explícito para containers que suportam
    /// exibição de Busy Overlay.
    ///
    /// RESPONSABILIDADE:
    /// - Expor controle explícito de Busy (show / hide)
    ///
    /// NÃO FAZ:
    /// - Não decide quando exibir
    /// - Não executa lógica assíncrona
    /// - Não conhece regra de negócio
    /// </summary>
    public interface IBusyHost
    {
        /// <summary>
        /// Exibe o overlay de Busy.
        /// </summary>
        /// <param name="message">Mensagem opcional ao usuário.</param>
        void ShowBusy(string message = null);

        /// <summary>
        /// Oculta o overlay de Busy.
        /// </summary>
        void HideBusy();
    }
}
