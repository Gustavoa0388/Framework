namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// Estados visuais possíveis do grid.
    /// O grid NÃO decide o estado.
    /// O container (form, presenter, viewmodel) controla.
    /// </summary>
    public enum GsGridState
    {
        /// <summary>
        /// Dados estão sendo carregados.
        /// </summary>
        Loading,

        /// <summary>
        /// Nenhum dado disponível.
        /// </summary>
        Empty,

        /// <summary>
        /// Dados carregados e exibidos normalmente.
        /// </summary>
        Ready,

        /// <summary>
        /// Ocorreu erro no carregamento.
        /// </summary>
        Error
    }
}
