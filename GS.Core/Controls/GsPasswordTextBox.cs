using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    public class GsPasswordTextBox : GsInputBase
    {
        private PictureBox _eyeIcon;
        private bool _showPassword;

        protected override bool SupportsExtraIcon => true;

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

            _eyeIcon = new PictureBox
            {
                Size = new Size(20, 20),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_closed,
                BackColor = Color.Transparent
            };

            _eyeIcon.Click += (_, _) => TogglePassword();

            Controls.Add(_eyeIcon);
            BringToFront();

            UpdateLayout();
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_eyeIcon == null)
                return;

            int errorOffset = GetRightIconsWidth() - 24;

            _eyeIcon.Location = new Point(
                Width - Padding.Right - 20 - errorOffset,
                (Height - 20) / 2
            );
        }

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
