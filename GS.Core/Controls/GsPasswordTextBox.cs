using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsPasswordTextBox
    /// 
    /// Input de senha padrão do GS Core.
    /// 
    /// Características:
    /// - Herda de GsInputBase
    /// - Utiliza TextBox nativo com UseSystemPasswordChar
    /// - Possui botão de alternância de visibilidade (ícone de olho)
    /// - Participa do fluxo global de validação (IGsValidatable)
    /// 
    /// IMPORTANTE:
    /// - Não implementa validação própria
    /// - A regra de Required é tratada exclusivamente no GsInputBase
    /// - Este controle apenas complementa o layout
    /// </summary>
    public class GsPasswordTextBox : GsInputBase
    {
        private PictureBox _eyeIcon;
        private bool _showPassword;

        // Tamanho do ícone do "olho"
        private const int EyeIconSize = 20;

        // Espaçamento entre ícone de erro e ícone do olho
        private const int EyeIconSpacing = 6;

        /// <summary>
        /// Cria o TextBox interno utilizado pelo input.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            return new TextBox
            {
                BorderStyle = BorderStyle.None,
                UseSystemPasswordChar = true
            };
        }

        /// <summary>
        /// Inicialização do controle após criação.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Ícone de exibir/ocultar senha
            _eyeIcon = new PictureBox
            {
                Size = new Size(EyeIconSize, EyeIconSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_closed,
                BackColor = Color.Transparent,
                TabStop = false
            };

            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);
            _eyeIcon.BringToFront();

            UpdateLayout();
        }

        /// <summary>
        /// Atualiza o layout interno do controle.
        /// Ajusta a posição do ícone de olho respeitando:
        /// - Padding
        /// - Espaço reservado para ícone de erro
        /// </summary>
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_eyeIcon == null)
                return;

            // Se houver erro, desloca o ícone para a esquerda
            int errorOffset = HasError
                ? ErrorIconSize + ErrorIconSpacing
                : 0;

            _eyeIcon.Location = new Point(
                Width - Padding.Right - EyeIconSize - errorOffset,
                (Height - EyeIconSize) / 2
            );
        }

        /// <summary>
        /// Alterna a visibilidade da senha.
        /// </summary>
        private void TogglePassword()
        {
            _showPassword = !_showPassword;

            if (InnerTextBox is TextBox tb)
                tb.UseSystemPasswordChar = !_showPassword;

            _eyeIcon.Image = _showPassword
                ? Properties.Resources.eye_open
                : Properties.Resources.eye_closed;
        }

        /// <summary>
        /// Participa explicitamente do fluxo de validação do GS Core.
        /// 
        /// Não adiciona validação própria.
        /// A regra de Required é tratada no GsInputBase.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
