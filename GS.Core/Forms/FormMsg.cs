using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormMsg
    /// 
    /// Classe utilitária para padronização de mensagens do sistema.
    /// 
    /// RESPONSABILIDADES:
    /// - Centralizar uso de MessageBox
    /// - Evitar chamadas diretas espalhadas pelo sistema
    /// 
    /// STATUS:
    /// - GS Core (estável)
    /// 
    /// IMPORTANTE:
    /// - Mantém métodos legacy (Success) para compatibilidade
    /// - Refatoração semântica fica para FASE EXTRA
    /// </summary>
    public static class FormMsg
    {
        /// <summary>
        /// Mensagem de sucesso (LEGACY).
        /// Mantida por compatibilidade com código existente.
        /// </summary>
        public static void Success(string message, string title = "Sucesso")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Mensagem informativa.
        /// </summary>
        public static void Info(string message, string title = "Informação")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Mensagem de erro.
        /// </summary>
        public static void Error(string message, string title = "Erro")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Mensagem de aviso.
        /// </summary>
        public static void Warning(string message, string title = "Atenção")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Mensagem de confirmação (Sim / Não).
        /// Retorna true se o usuário confirmar.
        /// </summary>
        public static bool Confirm(string message, string title = "Confirmação")
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }
    }
}
