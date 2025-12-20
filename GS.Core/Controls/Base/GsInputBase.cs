using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Base
{
    public abstract class GsInputBase : UserControl, IThemedControl, IGsRequiredAware
    {
        protected TextBox InnerTextBox;

        protected bool IsHovered;
        protected bool IsFocused;

        private bool _touched;
        private bool _hasError;

        protected virtual bool SupportsExtraIcon => false;

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

            // ❗ NUNCA transparente
            BackColor = Color.White;
        }

        protected abstract TextBox CreateInnerTextBox();

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;
            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);
            InnerTextBox.Height = Height - Padding.Vertical;

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

            Resize += (_, _) => UpdateLayout();
            UpdateLayout();
        }

        protected virtual int GetRightIconsWidth()
        {
            int width = 0;

            if (_hasError)
                width += 24;

            if (SupportsExtraIcon)
                width += 24;

            return width;
        }

        protected virtual void UpdateLayout()
        {
            if (InnerTextBox == null)
                return;

            InnerTextBox.Width = Width - Padding.Horizontal - GetRightIconsWidth();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            var theme = ThemeManager.Current;

            // 🔹 FUNDO (ESSENCIAL)
            using (var bg = new SolidBrush(theme.InputBackground))
                g.FillRectangle(bg, ClientRectangle);

            Color borderColor =
                _hasError ? theme.Error :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            using var pen = new Pen(borderColor, 1f);

            g.DrawRectangle(
                pen,
                0,
                0,
                Width - 1,
                Height - 1
            );

            DrawErrorIcon(g);
        }

        protected virtual void DrawErrorIcon(Graphics g)
        {
            if (!_hasError)
                return;

            var rect = new Rectangle(
                Width - Padding.Right - 16,
                (Height - 16) / 2,
                16,
                16
            );

            g.DrawImage(Properties.Resources.error, rect);
        }

        protected void ValidateRequired()
        {
            if (!Required)
            {
                ClearError();
                return;
            }

            if (_touched && string.IsNullOrWhiteSpace(Text))
                ShowError();
            else
                ClearError();
        }

        protected void ShowError()
        {
            _hasError = true;
            UpdateLayout();
            Invalidate();
        }

        protected void ClearError()
        {
            _hasError = false;
            UpdateLayout();
            Invalidate();
        }

        public virtual void ApplyTheme(GsTheme theme)
        {
            Font = theme.DefaultFont;

            BackColor = theme.InputBackground;

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
            set
            {
                if (InnerTextBox != null)
                    InnerTextBox.Text = value;
            }
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
