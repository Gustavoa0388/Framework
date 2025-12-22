using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// Centraliza a exibição de mensagens do GS Core.
    /// Evita uso direto de MessageBox espalhado pelo sistema.
    /// </summary>
    public static class FormMsg
    {
        // ============================
        // SUCESSO
        // ============================

        public static void Success(string message, string title = "Sucesso")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ============================
        // ERRO
        // ============================

        public static void Error(string message, string title = "Erro")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        // ============================
        // ALERTA / WARNING
        // ============================

        public static void Warning(string message, string title = "Atenção")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        // ============================
        // INFORMAÇÃO
        // ============================

        public static void Info(string message, string title = "Informação")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ============================
        // CONFIRMAÇÃO
        // ============================

        public static bool Confirm(
            string message,
            string title = "Confirmação",
            MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                icon
            ) == DialogResult.Yes;
        }
    }
}
