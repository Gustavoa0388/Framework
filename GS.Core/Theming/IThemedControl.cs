using GS.Core.UI.Theming;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Contrato base para controles que suportam Theme.
    /// 
    /// REGRAS:
    /// - O controle DEVE aplicar SOMENTE tokens do GsTheme.
    /// - É PROIBIDO usar cores hardcoded.
    /// - A aplicação do theme deve ser idempotente
    ///   (pode ser chamada várias vezes sem efeitos colaterais).
    /// </summary>
    public interface IThemedControl
    {
        /// <summary>
        /// Aplica o tema visual ao controle.
        /// </summary>
        void ApplyTheme(GsTheme theme);
    }
}
