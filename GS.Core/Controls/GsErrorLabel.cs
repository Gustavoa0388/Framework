using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsErrorLabel
    /// 
    /// Label específico para exibição de mensagens de erro.
    /// 
    /// RESPONSABILIDADE:
    /// - Exibir erro de validação
    /// - Controlar visibilidade do erro
    /// 
    /// STATUS:
    /// - GS Core
    /// 
    /// OBSERVAÇÕES:
    /// - Inicia invisível
    /// - Exibição controlada via ShowError / ClearError
    /// </summary>
    public class GsErrorLabel : Label, IThemedControl
    {
        public GsErrorLabel()
        {
            AutoSize = true;
            Visible = false;
        }

        /// <summary>
        /// Aplica o tema visual ao label de erro.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            Font = theme.DefaultFont;
            ForeColor = theme.Error;
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// Exibe uma mensagem de erro.
        /// </summary>
        public void ShowError(string message)
        {
            Text = message;
            Visible = true;
        }

        /// <summary>
        /// Limpa e oculta o erro.
        /// </summary>
        public void ClearError()
        {
            Text = string.Empty;
            Visible = false;
        }
    }
}
