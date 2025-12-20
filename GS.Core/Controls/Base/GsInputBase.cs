using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Classe base para TODOS os inputs do GS Core.
    /// Responsável por:
    /// - Fundo
    /// - Borda
    /// - Estado de foco/hover
    /// - Validação Required
    /// - Ícone de erro interno
    /// </summary>
    public abstract class GsInputBase : UserControl, IThemedControl
    {
        // ======================================================
        // CAMPOS BASE
        // ======================================================

        protected TextBox InnerTextBox;

        protected bool IsFocused;
        protected bool IsHovered;

        // Controle de erro
        private GsErrorLabel errorLabel;

        public bool Required { get; set; }
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        /// <summary>
        /// Indica se o input está atualmente em erro
        /// </summary>
        public bool HasError { get; private set; }

        // Ícone de erro
        protected const int ErrorIconSize = 14;
        protected const int ErrorIconSpacing = 6;

        // ======================================================
        // CONSTRUTOR
        // ======================================================
        protected GsInputBase()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            Height = 36;

            // Padding PADRÃO (não mexer sem motivo)
            Padding = new Padding(8, 6, 8, 6);

            BackColor = Color.Transparent;
        }

        // ======================================================
        // CONTRATO PARA FILHOS
        // ======================================================
        protected abstract TextBox CreateInnerTextBox();

        // ======================================================
        // INICIALIZAÇÃO
        // ======================================================
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;

            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);
            InnerTextBox.Width = Width - Padding.Horizontal;
            InnerTextBox.Height = 20;

            InnerTextBox.GotFocus += (_, _) =>
            {
                IsFocused = true;
                Invalidate();
            };

            InnerTextBox.LostFocus += (_, _) =>
            {
                IsFocused = false;
                ValidateRequired();
                Invalidate();
            };

            Controls.Add(InnerTextBox);

            // Label de erro (texto abaixo do input)
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

        // ======================================================
        // LAYOUT
        // ======================================================
        protected virtual void UpdateLayout()
        {
            if (InnerTextBox == null)
                return;

            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);

            // Reserva espaço do ícone de erro à direita
            int rightPadding = Padding.Right +
                               (HasError ? ErrorIconSize + ErrorIconSpacing : 0);

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

        // ======================================================
        // PINTURA
        // ======================================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // REGRA: SEM anti-alias em retângulo
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            var theme = ThemeManager.Current;

            // Fundo
            using (var bg = new SolidBrush(theme.InputBackground))
                g.FillRectangle(bg, ClientRectangle);

            // Cor da borda
            Color borderColor =
                HasError ? theme.Error :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            using (var pen = new Pen(borderColor, 1f))
            {
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }

            // ÍCONE DE ERRO (interno)
            if (HasError)
            {
                int x = Width - Padding.Right - ErrorIconSize;
                int y = (Height - ErrorIconSize) / 2;

                g.DrawImage(
                    Properties.Resources.error,
                    new Rectangle(x, y, ErrorIconSize, ErrorIconSize)
                );
            }
        }

        // ======================================================
        // VALIDAÇÃO
        // ======================================================
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
            HasError = true;
            errorLabel?.ShowError(message);
            UpdateLayout();
            Invalidate();
        }

        protected void ClearError()
        {
            HasError = false;
            errorLabel?.ClearError();
            UpdateLayout();
            Invalidate();
        }

        // ======================================================
        // TEMA
        // ======================================================
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

        // ======================================================
        // TEXTO / PLACEHOLDER
        // ======================================================
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
