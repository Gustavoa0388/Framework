using System;
using System.Windows.Forms;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Forms.Navigation
{
    /// <summary>
    /// GsNavigationService
    ///
    /// Infraestrutura oficial de navegação do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Abrir formulários modais
    /// - Capturar resultado semântico (GsFormResult)
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não conhece telas específicas
    /// - Não executa regras de negócio
    ///
    /// PRINCÍPIOS:
    /// - Explícito
    /// - Previsível
    /// - Sem automação implícita
    /// </summary>
    public static class GsNavigationService
    {
        // =====================================================
        // MODAL
        // =====================================================

        /// <summary>
        /// Abre um formulário modal GS Core UI.
        ///
        /// O formulário DEVE herdar de GsBaseForm.
        /// </summary>
        /// <typeparam name="TForm">Tipo do formulário</typeparam>
        /// <param name="owner">Form pai (opcional)</param>
        /// <returns>Resultado semântico da navegação</returns>
        public static GsFormResult OpenModal<TForm>(Form owner = null)
            where TForm : GsBaseForm, new()
        {
            using (var form = new TForm())
            {
                if (owner != null)
                {
                    form.ShowDialog(owner);
                }
                else
                {
                    form.ShowDialog();
                }

                return form.NavigationResult ?? GsFormResult.None();
            }
        }
    }
}
