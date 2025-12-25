using System.Drawing;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsErrorModalForm
    ///
    /// Modal de erro oficial do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Comunicar falha consumada ao usuário
    /// - Exibir mensagem de erro de forma clara e objetiva
    ///
    /// NÃO FAZ:
    /// - Não tenta corrigir o erro
    /// - Não executa regra de negócio
    /// - Não decide fluxo
    /// - Não retorna intenção semântica além de Closed
    /// </summary>
    public sealed class GsErrorModalForm : GsModalBaseForm
    {
        private readonly string _message;

        public GsErrorModalForm(string message)
        {
            _message = message;
            Text = "Erro";
        }

        // =====================================================
        // LIFECYCLE
        // =====================================================

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SetMessage(_message);

            // Ícone de erro (provisório, tema soberano no futuro)
            SetIcon(SystemIcons.Error.ToBitmap());

            ConfigurePrimaryButton(
                text: "OK",
                result: GsFormResult.Closed()
            );
        }
    }
}
