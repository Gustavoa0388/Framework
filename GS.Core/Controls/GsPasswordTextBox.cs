using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;
using GS.Core.UI.Properties;

namespace GS.Core.UI.Controls
{
    public class GsPasswordTextBox : GsInputBase
    {
        private PictureBox _eyeIcon;
        private bool _showPassword;

        protected override TextBox CreateInnerTextBox()
        {
            return new TextBox
            {
                UseSystemPasswordChar = true
            };
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // espaço para o ícone
            InnerTextBox.Width -= 28;

            _eyeIcon = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(Width - 26, 6),
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Resources.eye_closed
            };

            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);

            Resize += (_, _) =>
            {
                _eyeIcon.Location = new Point(Width - 26, 6);
                InnerTextBox.Width = Width - 36;
            };
        }

        private void TogglePassword()
        {
            _showPassword = !_showPassword;

            InnerTextBox.UseSystemPasswordChar = !_showPassword;
            _eyeIcon.Image = _showPassword
                ? Resources.eye_open
                : Resources.eye_closed;
        }

        public override void ApplyTheme(GsTheme theme)
        {
            base.ApplyTheme(theme);

            // opcional: adaptar fundo do ícone
            _eyeIcon.BackColor = Color.Transparent;
        }
    }
}
