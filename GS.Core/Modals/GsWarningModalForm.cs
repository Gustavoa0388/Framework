using System.Drawing;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsWarningModalForm
    ///
    /// Modal de alerta oficial do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Alertar o usuário sobre uma condição relevante
    /// - Comunicar risco ou atenção necessária sem bloquear fluxo
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não executa regra de negócio
    /// - Não retorna intenção semântica além de Closed
    /// </summary>
    public sealed class GsWarningModalForm : GsModalBaseForm
    {
        private readonly string _message;

        public GsWarningModalForm(string message)
        {
            _message = message;
            Text = "Atenção";
        }

        // =====================================================
        // LIFECYCLE
        // =====================================================

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetMessage(_message);

            // Ícone de alerta (provisório, tema soberano no futuro)
            SetIcon(SystemIcons.Warning.ToBitmap());

            ConfigurePrimaryButton(
                text: "OK",
                result: GsFormResult.Closed()
            );
        }
    }
}
