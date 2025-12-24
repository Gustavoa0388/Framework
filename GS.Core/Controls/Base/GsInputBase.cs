using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;
using GS.Core.UI.Controls.Data;

namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Classe base para TODOS os inputs do GS Core.
    ///
    /// RESPONSABILIDADE PRINCIPAL:
    /// - Fornecer um "container inteligente" para inputs (TextBox, Masked, Numeric, etc.)
    ///
    /// ESTA CLASSE CENTRALIZA:
    /// - Aplicação de tema
    /// - Desenho de borda customizada
    /// - Estados visuais (hover, foco, erro)
    /// - Validação (required e erros)
    ///
    /// O QUE ESTA CLASSE **NÃO FAZ**:
    /// - Não conhece regra de negócio
    /// - Não conhece banco de dados
    /// - Não decide lógica de busca, cálculo ou persistência
    ///
    /// OBS:
    /// - Inputs concretos (GsTextBox, GsMaskedTextBox, etc.)
    ///   são responsáveis por propagar eventos do controle interno.
    /// </summary>
    public abstract class GsInputBase : UserControl,
        IThemedControl,
        IGsValidatable,
        IGsRequiredAware
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// TextBoxBase interno real (TextBox, MaskedTextBox, etc.).
        /// Ele é criado pela classe filha via CreateInnerTextBox().
        /// </summary>
        protected TextBoxBase InnerTextBox;

        // ==========================================================
        // ESTADOS VISUAIS
        // ==========================================================

        /// <summary>
        /// Indica se o input está com foco.
        /// Usado exclusivamente para decisão visual (borda).
        /// </summary>
        protected bool IsFocused;

        /// <summary>
        /// Indica se o mouse está sobre o input.
        /// Usado para hover visual.
        /// </summary>
        protected bool IsHovered;

        // ==========================================================
        // ERRO / VALIDAÇÃO VISUAL
        // ==========================================================

        /// <summary>
        /// Label auxiliar responsável por exibir mensagens de erro
        /// abaixo do input.
        /// </summary>
        private GsErrorLabel errorLabel;

        /// <summary>
        /// Ícone de erro reutilizável.
        /// Bitmap estático evita criação excessiva de objetos
        /// durante o repaint (performance).
        /// </summary>
        protected static readonly Bitmap ErrorIcon =
            Properties.Resources.error;

        // ==========================================================
        // REQUIRED (CONTRATO DE INPUT)
        // ==========================================================

        /// <summary>
        /// Indica se o campo é obrigatório.
        /// A validação ocorre automaticamente no LostFocus.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Mensagem exibida quando o campo obrigatório está vazio.
        /// </summary>
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        // ==========================================================
        // ESTADO DE VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Indica se o input está válido.
        /// Conveniência para telas e formulários.
        /// </summary>
        public bool IsValid => !HasError;

        /// <summary>
        /// Última mensagem de erro gerada pela validação.
        /// Exposta por contrato (IGsValidatable).
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Indica se o input está atualmente em estado de erro.
        /// </summary>
        public bool HasError { get; private set; }

        // ==========================================================
        // CONSTANTES DE LAYOUT
        // ==========================================================

        protected const int ErrorIconSize = 14;
        protected const int ErrorIconSpacing = 6;

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        /// <summary>
        /// Construtor base.
        /// Define estilo de pintura customizada e
        /// inicializa estados visuais.
        /// </summary>
        protected GsInputBase()
        {
            // Ativa pintura manual e double buffer
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            Height = 36;
            Padding = new Padding(8, 6, 8, 6);
            BackColor = Color.Transparent;

            // Controle de hover visual
            MouseEnter += (_, _) =>
            {
                IsHovered = true;
                Invalidate();
            };

            MouseLeave += (_, _) =>
            {
                IsHovered = false;
                Invalidate();
            };
        }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Método que DEVE ser implementado pelas classes filhas
        /// para fornecer o TextBox interno real.
        ///
        /// Ex:
        /// - TextBox
        /// - MaskedTextBox
        /// - NumericTextBox
        /// </summary>
        /// <summary>
        /// Cria o controle interno do input.
        /// Por padrão, inputs que NÃO usam TextBox podem retornar null.
        /// </summary>
        protected virtual TextBoxBase CreateInnerTextBox()
        {
            return null;
        }

        /// <summary>
        /// Criação tardia do controle interno.
        /// Garante que o controle só seja criado
        /// quando o handle existir.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Evita recriação
            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;

            // =============================
            // FOCO
            // =============================
            InnerTextBox.GotFocus += (_, _) =>
            {
                IsFocused = true;
                Invalidate();
            };

            InnerTextBox.LostFocus += (_, _) =>
            {
                IsFocused = false;
                ValidateInput();
                Invalidate();
            };

            Controls.Add(InnerTextBox);

            // =============================
            // LABEL DE ERRO
            // =============================
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

        // ==========================================================
        // LAYOUT
        // ==========================================================

        /// <summary>
        /// Atualiza layout interno considerando:
        /// - Padding
        /// - Presença de ícone de erro
        /// </summary>
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

        /// <summary>
        /// Posiciona o label de erro logo abaixo do input.
        /// </summary>
        private void UpdateErrorPosition()
        {
            if (errorLabel == null)
                return;

            errorLabel.Location = new Point(Left, Bottom + 4);
            errorLabel.Width = Width;
        }

        // ==========================================================
        // PINTURA CUSTOMIZADA
        // ==========================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var theme = ThemeManager.Current;

            // Fundo
            using (var bg = new SolidBrush(theme.InputBackground))
                g.FillRectangle(bg, ClientRectangle);

            // Borda conforme estado
            Color borderColor =
                HasError ? theme.InputError :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            using (var pen = new Pen(borderColor))
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

            // Ícone de erro
            if (HasError)
            {
                int x = Width - Padding.Right - ErrorIconSize;
                int y = (Height - ErrorIconSize) / 2;

                g.DrawImage(
                    ErrorIcon,
                    new Rectangle(x, y, ErrorIconSize, ErrorIconSize)
                );
            }
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Validação interna do input.
        /// Pode ser sobrescrita por inputs específicos.
        /// </summary>
        public virtual void ValidateInput()
        {
            ClearError();

            if (Required && string.IsNullOrWhiteSpace(Text))
                ShowError(RequiredMessage);
        }

        /// <summary>
        /// Implementação explícita da interface IGsValidatable.
        /// Evita conflito com Control.Validate().
        /// </summary>
        void IGsValidatable.Validate()
        {
            ValidateInput();
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

        // ==========================================================
        // TEMA
        // ==========================================================

        /// <summary>
        /// Aplica tema visual ao input.
        /// Chamado automaticamente pelo ThemeManager.
        /// </summary>
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

        // ==========================================================
        // TEXTO
        // ==========================================================

        /// <summary>
        /// Propagação da propriedade Text.
        /// Garante comportamento consistente com controles nativos.
        /// </summary>
        public override string Text
        {
            get => InnerTextBox?.Text ?? string.Empty;
            set
            {
                if (InnerTextBox != null)
                    InnerTextBox.Text = value;
            }
        }
    }
}
