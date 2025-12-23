using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// GsErrorLabel
    ///
    /// Label especializado para exibição de mensagens
    /// de erro de validação LOCAL.
    ///
    /// RESPONSABILIDADE:
    /// - Exibir mensagens de erro abaixo de inputs
    /// - Controlar visibilidade do erro
    ///
    /// O QUE ESTE CONTROLE NÃO FAZ:
    /// - Não valida dados
    /// - Não decide quando exibir erros
    /// - Não exibe mensagens globais
    ///
    /// Ele apenas REFLETE o estado de erro informado.
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
        ///
        /// Regra:
        /// - Sempre usar a cor Error do tema
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
