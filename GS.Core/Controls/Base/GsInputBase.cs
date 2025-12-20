using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Base
{
    public abstract class GsInputBase : UserControl, IThemedControl
    {
        protected TextBox InnerTextBox;
        protected bool IsHovered;
        protected bool IsFocused;
        private bool _touched;

        private GsErrorLabel errorLabel;

        public bool Required { get; set; }
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        protected GsInputBase()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            Height = 36;
            Padding = new Padding(8, 6, 8, 6);

            BackColor = ThemeManager.Current.InputBackground;

        }

        protected abstract TextBox CreateInnerTextBox();

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;

            // 🔥 PIXEL PERFEITO (estilo ECturbo)
            InnerTextBox.Location = new Point(8, 7);
            InnerTextBox.Width = Width - 16;
            InnerTextBox.Height = 18;

            InnerTextBox.GotFocus += (_, _) =>
            {
                IsFocused = true;
                _touched = true;
                Invalidate();
            };

            InnerTextBox.LostFocus += (_, _) =>
            {
                IsFocused = false;
                ValidateRequired();
                Invalidate();
            };

            Controls.Add(InnerTextBox);

            errorLabel = new GsErrorLabel { Visible = false };

            ParentChanged += (_, _) =>
            {
                if (Parent != null && !Parent.Controls.Contains(errorLabel))
                    Parent.Controls.Add(errorLabel);

                UpdateErrorPosition();
            };

            Resize += (_, _) => UpdateLayout();
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (InnerTextBox == null)
                return;

            InnerTextBox.Location = new Point(
                Padding.Left,
                Padding.Top
            );

            InnerTextBox.Width = Width - Padding.Horizontal;
            InnerTextBox.Height = Height - Padding.Vertical;

            UpdateErrorPosition();
        }


        private void UpdateErrorPosition()
        {
            if (errorLabel == null) return;
            errorLabel.Location = new Point(Left, Bottom + 4);
            errorLabel.Width = Width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.Clear(Color.Transparent);

            var theme = ThemeManager.Current;

            // 🔹 FUNDO DO INPUT (ESSENCIAL NO DARK)
            using (var bg = new SolidBrush(theme.InputBackground))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            // 🔹 COR DA BORDA
            Color borderColor =
                errorLabel?.Visible == true ? theme.Error :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            var rect = new Rectangle(
                0,
                0,
                Width - 1,
                Height - 1
            );

            using var pen = new Pen(borderColor, 1f);
            g.DrawRectangle(pen, rect);
        }




        protected void ValidateRequired()
        {
            if (!Required || InnerTextBox == null)
            {
                ClearError();
                return;
            }

            if (string.IsNullOrWhiteSpace(InnerTextBox.Text))
                ShowError(RequiredMessage);
            else
                ClearError();
        }

        protected void ShowError(string message)
        {
            errorLabel?.ShowError(message);
            Invalidate();
        }

        protected void ClearError()
        {
            errorLabel?.ClearError();
            Invalidate();
        }

        public virtual void ApplyTheme(GsTheme theme)
        {
            Font = theme.DefaultFont;

            if (InnerTextBox != null)
            {
                InnerTextBox.BackColor = theme.InputBackground;
                InnerTextBox.ForeColor = theme.TextPrimary;
            }

            Invalidate();
        }

        public override string Text
        {
            get => InnerTextBox?.Text ?? string.Empty;
            set { if (InnerTextBox != null) InnerTextBox.Text = value; }
        }

        public string Placeholder
        {
            get => InnerTextBox?.PlaceholderText ?? string.Empty;
            set
            {
                if (InnerTextBox != null)
                    InnerTextBox.PlaceholderText = value;
            }
        }

    }
}
