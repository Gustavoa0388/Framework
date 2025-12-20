using System.Drawing;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    public class GsPasswordTextBox : GsInputBase
    {
        private bool _showPassword;
        private bool _hoverEye;

        public GsPasswordTextBox()
        {
            InnerTextBox.UseSystemPasswordChar = true;
            Padding = new Padding(8, 6, 32, 6);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool hoverNow = GetEyeRect().Contains(e.Location);
            if (hoverNow != _hoverEye)
            {
                _hoverEye = hoverNow;
                Cursor = _hoverEye ? Cursors.Hand : Cursors.IBeam;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (GetEyeRect().Contains(e.Location))
            {
                TogglePassword();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawEye(e.Graphics);
        }

        private void TogglePassword()
        {
            _showPassword = !_showPassword;
            InnerTextBox.UseSystemPasswordChar = !_showPassword;
            Invalidate();
        }

        private Rectangle GetEyeRect()
        {
            return new Rectangle(Width - 24, (Height - 16) / 2, 16, 16);
        }

        private void DrawEye(Graphics g)
        {
            var rect = GetEyeRect();
            var color =
                _hoverEye ? Theme.InputFocus :
                _showPassword ? Theme.TextPrimary :
                Theme.TextPlaceholder;

            using var pen = new Pen(color, 1.5f);

            g.DrawEllipse(pen, rect);
            g.DrawEllipse(
                pen,
                rect.X + 5,
                rect.Y + 5,
                rect.Width - 10,
                rect.Height - 10
            );
        }
    }
}
