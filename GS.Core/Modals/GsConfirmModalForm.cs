using System.Drawing;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsConfirmModalForm
    ///
    /// Modal corporativo de confirmação do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Solicitar confirmação explícita do usuário
    /// - Retornar intenção semântica (Saved / Canceled)
    ///
    /// NÃO FAZ:
    /// - Não executa regra de negócio
    /// - Não decide fluxo
    /// - Não conhece quem chamou
    /// </summary>
    public sealed class GsConfirmModalForm : GsModalBaseForm
    {
        private readonly string _message;

        public GsConfirmModalForm(string message)
        {
            _message = message;
            Text = "Confirmação";
        }

        // =====================================================
        // LIFECYCLE
        // =====================================================

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetMessage(_message);

            // Ícone pode ser ajustado futuramente via Theme
            SetIcon(SystemIcons.Question.ToBitmap());

            ConfigurePrimaryButton(
                text: "Confirmar",
                result: GsFormResult.Saved()
            );

            ConfigureSecondaryButton(
                text: "Cancelar",
                result: GsFormResult.Canceled()
            );
        }
    }
}
