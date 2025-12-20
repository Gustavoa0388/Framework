using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// TextBox de senha com botão de visibilidade (ícone de olho).
    /// Totalmente compatível com:
    /// - GsInputBase
    /// - Validação Required
    /// - Tema
    /// </summary>
    public class GsPasswordTextBox : GsInputBase
    {
        // ======================================================
        // CAMPOS PRIVADOS
        // ======================================================

        // Ícone do olho
        private PictureBox _eyeIcon;

        // Estado atual da senha
        private bool _showPassword = false;

        // Dimensões fixas do ícone
        private const int EyeIconSize = 20;
        private const int EyeIconPadding = 6;

        // ======================================================
        // CRIAÇÃO DO TEXTBOX INTERNO
        // ======================================================

        protected override TextBox CreateInnerTextBox()
        {
            return new TextBox
            {
                UseSystemPasswordChar = true
            };
        }

        // ======================================================
        // INICIALIZAÇÃO DO CONTROLE
        // ======================================================

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Cria o ícone do olho
            _eyeIcon = new PictureBox
            {
                Size = new Size(EyeIconSize, EyeIconSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_closed,
                BackColor = Color.Transparent,
                TabStop = false
            };

            // Clique alterna visibilidade
            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);
            _eyeIcon.BringToFront();

            UpdateLayout();
        }

        // ======================================================
        // LAYOUT
        // ======================================================

        /// <summary>
        /// Ajusta:
        /// - Largura do TextBox (para não passar por baixo do olho)
        /// - Posição do ícone do olho
        /// </summary>
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (InnerTextBox == null || _eyeIcon == null)
                return;

            // Reserva espaço do olho
            InnerTextBox.Width =
                Width
                - Padding.Horizontal
                - EyeIconSize
                - EyeIconPadding;

            // Posiciona o ícone do olho à direita
            _eyeIcon.Location = new Point(
                Width - Padding.Right - EyeIconSize - EyeIconPadding,
                (Height - EyeIconSize) / 2
            );
        }

        // ======================================================
        // LÓGICA DO OLHO
        // ======================================================

        private void TogglePassword()
        {
            _showPassword = !_showPassword;

            InnerTextBox.UseSystemPasswordChar = !_showPassword;

            _eyeIcon.Image = _showPassword
                ? Properties.Resources.eye_open
                : Properties.Resources.eye_closed;
        }
    }
}
