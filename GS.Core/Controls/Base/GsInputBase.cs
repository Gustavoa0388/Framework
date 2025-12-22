using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;
using GS.Core.UI.Controls;

namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Classe base para TODOS os inputs do GS Core.
    /// Centraliza:
    /// - Tema
    /// - Borda
    /// - Hover / Focus
    /// - Required
    /// - Validação
    /// </summary>
    public abstract class GsInputBase : UserControl,
        IThemedControl,
        IGsValidatable,
        IGsRequiredAware
    {
        protected TextBoxBase InnerTextBox;

        protected bool IsFocused;
        protected bool IsHovered;

        private GsErrorLabel errorLabel;

        // =============================
        // REQUIRED
        // =============================
        public bool Required { get; set; }
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        // =============================
        // VALIDAÇÃO
        // =============================
        public bool IsValid => !HasError;
        public string ErrorMessage { get; private set; }

        public bool HasError { get; private set; }

        protected const int ErrorIconSize = 14;
        protected const int ErrorIconSpacing = 6;

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
            BackColor = Color.Transparent;

            MouseEnter += (_, _) => { IsHovered = true; Invalidate(); };
            MouseLeave += (_, _) => { IsHovered = false; Invalidate(); };
        }

        protected abstract TextBoxBase CreateInnerTextBox();

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;

            InnerTextBox.GotFocus += (_, _) =>
            {
                IsFocused = true;
                Invalidate();
            };

            InnerTextBox.LostFocus += (_, _) =>
            {
                IsFocused = false;
                Validate();
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

        protected virtual void UpdateLayout()
        {
            if (InnerTextBox == null)
                return;

            int rightPadding = Padding.Right +
                               (HasError ? ErrorIconSize + ErrorIconSpacing : 0);

            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);
            InnerTextBox.Width = Width - Padding.Left - rightPadding;

            UpdateErrorPosition();
        }

        private void UpdateErrorPosition()
        {
            if (errorLabel == null)
                return;

            errorLabel.Location = new Point(Left, Bottom + 4);
            errorLabel.Width = Width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            var theme = ThemeManager.Current;

            using (var bg = new SolidBrush(theme.InputBackground))
                g.FillRectangle(bg, ClientRectangle);

            Color borderColor =
                HasError ? theme.InputError :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            using (var pen = new Pen(borderColor))
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        // =============================
        // VALIDAÇÃO
        // =============================
        public virtual void Validate()
        {
            ClearError();

            if (Required && string.IsNullOrWhiteSpace(Text))
                ShowError(RequiredMessage);
        }

        protected void ShowError(string message)
        {
            HasError = true;
            ErrorMessage = message;

            errorLabel?.ShowError(message);
            UpdateLayout();
            Invalidate();
        }

        protected void ClearError()
        {
            HasError = false;
            ErrorMessage = null;

            errorLabel?.ClearError();
            UpdateLayout();
            Invalidate();
        }

        // =============================
        // THEME
        // =============================
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
    }
}
