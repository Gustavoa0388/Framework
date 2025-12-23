using GS.Core.UI.Theming;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Contrato base para controles que suportam
    /// aplicação de tema visual (GsTheme).
    ///
    /// OBJETIVO:
    /// Garantir que TODOS os controles do GS Core
    /// possam reagir de forma consistente a mudanças
    /// de tema (cores, fontes, estados).
    ///
    /// ESTE CONTRATO DEFINE:
    /// - Como o controle recebe o tema
    /// - Quando o tema deve ser aplicado
    ///
    /// ESTE CONTRATO NÃO DEFINE:
    /// - Qual tema será usado
    /// - Quando o tema será trocado
    /// - Regras de layout
    ///
    /// ESSAS DECISÕES SÃO DO ThemeManager.
    /// </summary>
    public interface IThemedControl
    {
        /// <summary>
        /// Aplica o tema visual ao controle.
        ///
        /// REGRAS OBRIGATÓRIAS:
        /// - Usar APENAS propriedades do GsTheme
        /// - NÃO usar cores hardcoded
        /// - NÃO criar efeitos colaterais
        /// - Método deve ser idempotente
        ///   (pode ser chamado várias vezes)
        ///
        /// BOAS PRÁTICAS:
        /// - Atualizar BackColor, ForeColor e Font
        /// - Reaplicar estilos visuais
        /// - Chamar Invalidate() se necessário
        ///
        /// QUANDO É CHAMADO:
        /// - Inicialização do controle
        /// - Troca global de tema
        /// </summary>
        void ApplyTheme(GsTheme theme);
    }
}
