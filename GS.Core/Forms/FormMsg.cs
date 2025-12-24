using System;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormMsg
    ///
    /// Centralizador oficial de mensagens do GS Core UI.
    ///
    /// RESPONSABILIDADES:
    /// - Exibir mensagens de Info, Success, Warning, Error e Confirm
    /// - Aplicar tema do GS Core
    /// - Padronizar UX de diálogo
    ///
    /// NÃO FAZ:
    /// - Não executa regra de negócio
    /// - Não substitui UX States (StateView)
    /// - Não decide fluxo de aplicação
    /// </summary>
    public static class FormMsg
    {
        // =====================================================
        // INFO
        // =====================================================

        public static void Info(string message, string title = "Informação")
        {
            Show(message, title, MessageBoxIcon.Information);
        }

        // =====================================================
        // SUCCESS
        // =====================================================

        public static void Success(string message, string title = "Sucesso")
        {
            Show(message, title, MessageBoxIcon.Information);
        }

        // =====================================================
        // WARNING
        // =====================================================

        public static void Warning(string message, string title = "Atenção")
        {
            Show(message, title, MessageBoxIcon.Warning);
        }

        // =====================================================
        // ERROR
        // =====================================================

        public static void Error(string message, string title = "Erro")
        {
            Show(message, title, MessageBoxIcon.Error);
        }

        // =====================================================
        // CONFIRM
        // =====================================================

        public static bool Confirm(
            string message,
            string title = "Confirmação",
            string confirmText = "Sim",
            string cancelText = "Não")
        {
            ApplyThemeIfNeeded();

            var result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            return result == DialogResult.Yes;
        }

        // =====================================================
        // CORE
        // =====================================================

        private static void Show(
            string message,
            string title,
            MessageBoxIcon icon)
        {
            ApplyThemeIfNeeded();

            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                icon
            );
        }

        // =====================================================
        // THEME
        // =====================================================

        private static void ApplyThemeIfNeeded()
        {
            // Ponto único para evoluir:
            // - Custom dialog
            // - Dark mode real
            // - Ícones próprios
            // 
            // Hoje mantém MessageBox,
            // mas centralizado e rastreável.
            var _ = ThemeManager.Current;
        }
    }
}
