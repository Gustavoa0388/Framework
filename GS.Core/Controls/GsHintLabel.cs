using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsHintLabel
    /// 
    /// Label de texto auxiliar (hint).
    /// 
    /// RESPONSABILIDADE:
    /// - Exibir instruções ou dicas ao usuário
    /// - Texto secundário, não interativo
    /// 
    /// STATUS:
    /// - GS Core
    /// 
    /// OBSERVAÇÕES:
    /// - Não participa de validação
    /// - Não exibe erro
    /// - Apenas informativo
    /// </summary>
    public class GsHintLabel : Label, IThemedControl
    {
        public GsHintLabel()
        {
            AutoSize = true;
        }

        /// <summary>
        /// Aplica o tema visual ao hint.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            Font = new Font(theme.DefaultFont.FontFamily, theme.DefaultFont.Size - 1);
            ForeColor = theme.TextSecondary;
            BackColor = Color.Transparent;
        }
    }
}
