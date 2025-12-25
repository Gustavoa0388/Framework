using System.Windows.Forms;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsModal
    ///
    /// Fachada pública oficial para abertura de modais
    /// corporativos do GS Core UI.
    /// </summary>
    public static class GsModal
    {
        // =====================================================
        // CONFIRM
        // =====================================================

        public static GsFormResult Confirm(string message, Form owner = null)
        {
            return OpenModal<GsConfirmModalForm>(owner, message);
        }

        // =====================================================
        // INFO
        // =====================================================

        public static GsFormResult Info(string message, Form owner = null)
        {
            return OpenModal<GsInfoModalForm>(owner, message);
        }

        // =====================================================
        // WARNING
        // =====================================================

        public static GsFormResult Warning(string message, Form owner = null)
        {
            return OpenModal<GsWarningModalForm>(owner, message);
        }

        // =====================================================
        // ERROR
        // =====================================================

        public static GsFormResult Error(string message, Form owner = null)
        {
            return OpenModal<GsErrorModalForm>(owner, message);
        }

        // =====================================================
        // CORE
        // =====================================================

        private static GsFormResult OpenModal<TModal>(Form owner, params object[] args)
            where TModal : GsModalBaseForm
        {
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
