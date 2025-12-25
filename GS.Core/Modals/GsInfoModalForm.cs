using System.Drawing;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsInfoModalForm
    ///
    /// Modal informativo oficial do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Exibir mensagem informativa ao usuário
    /// - Comunicar estado neutro (informação)
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não executa regra de negócio
    /// - Não retorna intenção semântica além de Closed
    /// </summary>
    public sealed class GsInfoModalForm : GsModalBaseForm
    {
        private readonly string _message;

        public GsInfoModalForm(string message)
        {
            _message = message;
            Text = "Informação";
        }

        // =====================================================
        // LIFECYCLE
        // =====================================================

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetMessage(_message);

            // Ícone informativo (provisório, tema soberano no futuro)
            SetIcon(SystemIcons.Information.ToBitmap());

            ConfigurePrimaryButton(
                text: "OK",
                result: GsFormResult.Closed()
            );
        }
    }
}
