using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Base
{
    public abstract class GsInputBase : UserControl
    {
        protected TextBox InnerTextBox;

        protected bool IsHovered;
        protected bool IsFocused;

        public string Placeholder { get; set; } = "";
        public bool Required { get; set; }

        protected GsTheme Theme => GsThemes.Light;

        protected GsInputBase()
        {
            DoubleBuffered = true;
            Height = 32;
            Padding = new Padding(8, 6, 8, 6);
            BackColor = Color.Transparent;

            InnerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(6, 7),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            Controls.Add(InnerTextBox);

            InnerTextBox.GotFocus += (_, _) => { IsFocused = true; Invalidate(); };
            InnerTextBox.LostFocus += (_, _) => { IsFocused = false; Invalidate(); };

            MouseEnter += (_, _) => { IsHovered = true; Invalidate(); };
            MouseLeave += (_, _) => { IsHovered = false; Invalidate(); };
        }

        private ToolTip _toolTip;

        public void SetToolTip(string text)
        {
            if (_toolTip == null)
                _toolTip = new ToolTip();

            _toolTip.SetToolTip(InnerTextBox, text);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (InnerTextBox == null)
                return;

            InnerTextBox.Width = Width - Padding.Horizontal;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var borderColor =
                IsFocused ? Theme.InputFocus :
                IsHovered ? Theme.InputHover :
                Theme.InputBorder;

            using var pen = new Pen(borderColor, 1.5f);
            var rect = ClientRectangle;
            rect.Inflate(-1, -1);

            g.DrawRectangle(pen, rect);
        }

        public override string Text
        {
            get => InnerTextBox.Text;
            set => InnerTextBox.Text = value;
        }
    }
}
