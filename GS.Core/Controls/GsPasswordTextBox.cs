using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// TextBox de senha com botão de visibilidade (olho).
    /// NÃO desenha erro. Apenas reserva espaço.
    /// </summary>
    public class GsPasswordTextBox : GsInputBase
    {
        private PictureBox _eyeIcon;
        private bool _showPassword;

        private const int EyeIconSize = 20;
        private const int EyeIconSpacing = 6;

        protected override TextBoxBase CreateInnerTextBox()
        {
            return new TextBox
            {
                UseSystemPasswordChar = true
            };
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();

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

        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_eyeIcon == null)
                return;

            // Posiciona o olho ANTES do ícone de erro
            int errorOffset = HasError
                ? ErrorIconSize + ErrorIconSpacing
                : 0;

            _eyeIcon.Location = new Point(
                Width - Padding.Right - EyeIconSize - errorOffset,
                (Height - EyeIconSize) / 2
            );
        }

        private void TogglePassword()
        {
            _showPassword = !_showPassword;

            if (InnerTextBox is TextBox tb)
                tb.UseSystemPasswordChar = !_showPassword;

            _eyeIcon.Image = _showPassword
                ? Properties.Resources.eye_open
                : Properties.Resources.eye_closed;
        }

    }
}

