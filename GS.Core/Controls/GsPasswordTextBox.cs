using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// TextBox de senha com botão de visibilidade (olho).
    /// Compatível com validação, erro e temas.
    /// </summary>
    public class GsPasswordTextBox : GsInputBase
    {
        // Ícone do olho (mostrar/ocultar senha)
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

            // Criação do PictureBox do olho
            _eyeIcon = new PictureBox
            {
                Size = new Size(EyeIconSize, EyeIconSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_closed,
                BackColor = Color.Transparent,
                TabStop = false
            };

            // Clique alterna visibilidade da senha
            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);

            // Garante que o ícone fique acima do TextBox
            _eyeIcon.BringToFront();

            UpdateLayout();
        }

        // ======================================================
        // RESERVA DE ESPAÇO À DIREITA (OLHO + ERRO)
        // ======================================================
        protected override int GetRightPadding()
        {
            // Espaço do olho + espaço base (ícone de erro, se houver)
            return base.GetRightPadding() + EyeIconSize + EyeIconPadding;
        }

        // ======================================================
        // POSICIONAMENTO DO ÍCONE
        // ======================================================
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_eyeIcon == null)
                return;

            _eyeIcon.Location = new Point(
                Width - Padding.Right - EyeIconSize,
                (Height - EyeIconSize) / 2
            );
        }

        // ======================================================
        // LÓGICA DO BOTÃO DE VISIBILIDADE
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
