using System.Windows.Forms;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsModal
    ///
    /// Fachada pública oficial para abertura de modais
    /// corporativos do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Expor API única e explícita para modais
    /// - Delegar abertura para GsNavigationService
    /// - Retornar resultado semântico (GsFormResult)
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não executa regra de negócio
    /// - Não mantém estado
    /// - Não acessa UI diretamente
    /// </summary>
    public static class GsModal
    {
        // =====================================================
        // CONFIRM
        // =====================================================

        /// <summary>
        /// Exibe um modal de confirmação.
        /// </summary>
        /// <param name="message">Mensagem exibida ao usuário.</param>
        /// <param name="owner">Form pai opcional.</param>
        /// <returns>Resultado semântico da confirmação.</returns>
        public static GsFormResult Confirm(string message, Form owner = null)
        {
            return OpenModal<GsConfirmModalForm>(owner, message);
        }

        // =====================================================
        // CORE
        // =====================================================

        private static GsFormResult OpenModal<TModal>(Form owner, params object[] args)
            where TModal : GsModalBaseForm
        {
            // Criação explícita para permitir construtores com parâmetros
            var modal = (TModal)System.Activator.CreateInstance(typeof(TModal), args);

            using (modal)
            {
                return owner != null
                    ? GsNavigationService.OpenModal(() => modal, owner)
                    : GsNavigationService.OpenModal(() => modal);
            }
        }
    }
}
