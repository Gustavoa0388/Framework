namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// Estados globais de UX do GS Core UI.
    ///
    /// REPRESENTA:
    /// - O estado VISUAL de uma tela ou container
    /// - Feedback global ao usuário
    ///
    /// NÃO REPRESENTA:
    /// - Estado funcional de controles (Grid, Paginator, etc.)
    /// - Estado local de componentes (hover, seleção, progresso)
    ///
    /// REGRAS:
    /// - O estado é CONTROLADO externamente (Form / Panel)
    /// - O estado é RENDERIZADO pelo GsStateView
    /// - Nunca deve conter lógica de negócio
    /// </summary>
    public enum GsUxState
    {
        /// <summary>
        /// Nenhum estado visível.
        /// O conteúdo normal está ativo.
        /// </summary>
        Hidden,

        /// <summary>
        /// Indica carregamento em andamento.
        /// Ex: busca de dados, inicialização de tela.
        /// </summary>
        Loading,

        /// <summary>
        /// Indica que não há dados para exibição.
        /// Ex: grid vazio, resultado de busca sem retorno.
        /// </summary>
        Empty,

        /// <summary>
        /// Indica erro global na tela ou operação.
        /// Ex: falha ao carregar dados.
        /// </summary>
        Error,

        /// <summary>
        /// Indica conclusão bem-sucedida de uma operação.
        /// Ex: dados salvos, ação finalizada.
        /// </summary>
        Success,

        /// <summary>
        /// Indica que a tela ou área está temporariamente indisponível.
        /// Ex: bloqueio de interação durante processo crítico.
        /// </summary>
        Disabled
    }
}
