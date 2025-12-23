namespace GS.Core.UI.Controls.States
{
    /// <summary>
    /// Define o tipo de superfície visual do GsPanel.
    /// 
    /// IMPORTANTE:
    /// - O enum é SEMÂNTICO, não visual.
    /// - Cada valor mapeia para tokens do GsTheme.
    /// </summary>
    public enum GsPanelSurface
    {
        /// <summary>
        /// Superfície padrão da aplicação.
        /// Normalmente usada como container principal.
        /// </summary>
        Default,

        /// <summary>
        /// Superfície alternativa.
        /// Usada para áreas secundárias ou agrupamentos.
        /// </summary>
        Secondary,

        /// <summary>
        /// Superfície de header.
        /// Usada para títulos, cabeçalhos e barras.
        /// </summary>
        Header,

        /// <summary>
        /// Superfície de destaque.
        /// Usada para áreas que precisam chamar atenção.
        /// </summary>
        Highlight
    }
}
