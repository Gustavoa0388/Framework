using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Classe base para todos os inputs do GS Core.
    /// Responsável por:
    /// - Renderização da borda
    /// - Validação Required
    /// - Placeholder
    /// - Integração com tema
    /// 
    /// IMPORTANTE:
    /// - NÃO conhece ícones
    /// - NÃO conhece Password / Date / Masked
    /// Cada input especializado resolve isso sozinho.
    /// </summary>
    public abstract class GsInputBase : UserControl, IThemedControl
    {
        // TextBox interno real
        protected TextBox InnerTextBox;

        // Estados visuais
        protected bool IsFocused;
        protected bool IsHovered;

        // Controle de validação
        public bool Required { get; set; }
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        private GsErrorLabel errorLabel;

        /// <summary>
        /// Indica se o controle está em estado de erro
        /// </summary>
        /// <summary>
        /// Indica se o input está atualmente em estado de erro.
        /// Esse estado é controlado internamente pelo próprio controle.
        /// </summary>
        public bool HasError { get; private set; }



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

            // Padding DEFINITIVO (não mexer sem critério)
            Padding = new Padding(8, 6, 8, 6);

            BackColor = Color.Transparent;
        }

        // ======================================================
        // MÉTODO OBRIGATÓRIO PARA DERIVADOS
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

            // Posicionamento seguro
            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);
            InnerTextBox.Width = Width - Padding.Horizontal;
            InnerTextBox.Height = 20; // FIXO → evita borda dupla

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

            // Label de erro (externo ao input)
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
            InnerTextBox.Width = Width - Padding.Horizontal;

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

            var g = e.Graphics;

            // ⚠️ REGRA DE OURO
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var theme = ThemeManager.Current;

            // Fundo do input (ESSENCIAL)
            using (var bg = new SolidBrush(theme.InputBackground))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            // Cor da borda conforme estado
            Color borderColor =
                errorLabel?.Visible == true ? theme.Error :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            using var pen = new Pen(borderColor, 1f);

            // Retângulo exato (sem borrar)
            g.DrawRectangle(
                pen,
                0,
                0,
                Width - 1,
                Height - 1
            );
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

        /// <summary>
        /// Ativa o estado de erro do input e exibe a mensagem.
        /// </summary>
        protected void ShowError(string message)
        {
            HasError = true;

            errorLabel?.ShowError(message);

            // Repaint do controle e dos filhos
            Invalidate();
            OnErrorStateChanged();
        }

        /// <summary>
        /// Limpa o estado de erro do input.
        /// </summary>
        protected void ClearError()
        {
            HasError = false;

            errorLabel?.ClearError();

            Invalidate();
            OnErrorStateChanged();
        }

   
        /// <summary>
        /// Notifica controles derivados que o estado de erro mudou
        /// </summary>
        protected virtual void OnErrorStateChanged()
        {
            // Força repaint dos filhos (GsTextBox, GsPasswordTextBox etc.)
            foreach (Control c in Controls)
                c.Invalidate();
        }


        // ======================================================
        // THEME
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
