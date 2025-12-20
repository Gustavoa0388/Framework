using GS.Core.UI.Theming;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Classe base para TODOS os inputs GS
    /// Centraliza:
    /// - Layout
    /// - Borda
    /// - Validação Required
    /// - Mensagem de erro
    /// - Animações sutis (foco / erro)
    /// </summary>
    public abstract class GsInputBase : UserControl, IThemedControl, IGsRequiredAware
    {
        // ===============================
        // CONTROLE INTERNO (TextBox real)
        // ===============================
        protected TextBox InnerTextBox;

        // ===============================
        // ESTADOS VISUAIS
        // ===============================
        protected bool IsHovered;
        protected bool IsFocused;
        private bool _touched;

        // ===============================
        // VALIDAÇÃO
        // ===============================
        public bool Required { get; set; }
        public string RequiredMessage { get; set; } = "Campo obrigatório";

        protected GsErrorLabel errorLabel;

        // ===============================
        // ANIMAÇÃO DE BORDA
        // ===============================
        private readonly System.Windows.Forms.Timer _animTimer;
        private float _animProgress;
        private Color _fromBorder;
        private Color _toBorder;

        // ===============================
        // CONSTRUTOR
        // ===============================
        protected GsInputBase()
        {
            // Ativa pintura manual e evita flicker
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            Height = 36;
            Padding = new Padding(8, 6, 8, 6);
            BackColor = Color.Transparent;

            // Timer da animação (leve, ~60fps)
            _animTimer = new System.Windows.Forms.Timer { Interval = 15 };
            _animTimer.Tick += AnimateTick;
        }

        // ===============================
        // CRIAÇÃO DO TEXTBOX INTERNO
        // (cada filho define o tipo)
        // ===============================
        protected abstract TextBox CreateInnerTextBox();

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox != null)
                return;

            InnerTextBox = CreateInnerTextBox();
            InnerTextBox.BorderStyle = BorderStyle.None;

            // Posicionamento ECturbo (pixel perfeito)
            InnerTextBox.Location = new Point(Padding.Left, Padding.Top);
            InnerTextBox.Width = Width - Padding.Horizontal;
            InnerTextBox.Height = Height - Padding.Vertical;

            // ===============================
            // EVENTOS DE FOCO
            // ===============================
            InnerTextBox.GotFocus += (_, _) =>
            {
                IsFocused = true;
                _touched = true;
                StartBorderAnimation(ThemeManager.Current.InputFocus);
            };

            InnerTextBox.LostFocus += (_, _) =>
            {
                IsFocused = false;
                ValidateRequired();
                StartBorderAnimation(ThemeManager.Current.InputBorder);
            };

            Controls.Add(InnerTextBox);

            // ===============================
            // LABEL DE ERRO (fora do input)
            // ===============================
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

        // ===============================
        // LAYOUT INTERNO
        // ===============================
        protected virtual void UpdateLayout()
        {
            if (InnerTextBox == null)
                return;

            int rightPadding = Padding.Right + GetRightPadding();

            InnerTextBox.Location = new Point(
                Padding.Left,
                Padding.Top
            );

            InnerTextBox.Width = Width - Padding.Left - rightPadding;
            InnerTextBox.Height = Height - Padding.Vertical;
        }

        private void UpdateErrorPosition()
        {
            if (errorLabel == null) return;

            errorLabel.Location = new Point(Left, Bottom + 4);
            errorLabel.Width = Width;
        }

        // ===============================
        // ANIMAÇÃO
        // ===============================
        private void StartBorderAnimation(Color target)
        {
            _fromBorder = _toBorder.IsEmpty
                ? ThemeManager.Current.InputBorder
                : _toBorder;

            _toBorder = target;
            _animProgress = 0f;
            _animTimer.Start();
        }

        private void AnimateTick(object sender, EventArgs e)
        {
            _animProgress += 0.15f;

            if (_animProgress >= 1f)
            {
                _animProgress = 1f;
                _animTimer.Stop();
            }

            Invalidate();
        }

        private static Color Lerp(Color a, Color b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);

            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t)
            );
        }

        // ===============================
        // PINTURA
        // ===============================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None; // borda nítida

            var theme = ThemeManager.Current;

            Color baseBorder =
                errorLabel?.Visible == true ? theme.Error :
                IsFocused ? theme.InputFocus :
                IsHovered ? theme.InputHover :
                theme.InputBorder;

            if (_animTimer.Enabled)
            {
                baseBorder = Lerp(_fromBorder, _toBorder, _animProgress);
            }

            using var pen = new Pen(baseBorder, 1f);
            g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

            if (HasErrorIcon)
            {
                int x = Width - ErrorIconSize - ErrorIconPadding;
                int y = (Height - ErrorIconSize) / 2;

                g.DrawImage(
                    Properties.Resources.error, // sua imagem
                    new Rectangle(x, y, ErrorIconSize, ErrorIconSize)
                );
            }
        }

        // ===============================
        // VALIDAÇÃO REQUIRED
        // ===============================
        protected void ValidateRequired()
        {
            if (!Required || !_touched)
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
            HasErrorIcon = true;
            errorLabel?.ShowError(message);
            UpdateLayout();
            Invalidate();
        }


        protected void ClearError()
        {
            HasErrorIcon = false;
            errorLabel?.ClearError();
            UpdateLayout();
            Invalidate();
        }

        // ===============================
        // TEMA
        // ===============================
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

        // ===============================
        // TEXTO
        // ===============================
        public override string Text
        {
            get => InnerTextBox?.Text ?? string.Empty;
            set
            {
                if (InnerTextBox != null)
                    InnerTextBox.Text = value;
            }
        }

        // ===============================
        // PLACEHOLDER
        // ===============================
        public string Placeholder
        {
            get => InnerTextBox?.PlaceholderText ?? string.Empty;
            set
            {
                if (InnerTextBox != null)
                    InnerTextBox.PlaceholderText = value;
            }
        }

        // Espaço reservado à direita para ícones (erro, botão, etc.)
        protected virtual int GetRightPadding()
        {
            return HasErrorIcon ? (ErrorIconSize + ErrorIconPadding) : 0;
        }
        protected bool HasErrorIcon = false;
        protected const int ErrorIconSize = 16;
        protected const int ErrorIconPadding = 6;

    }
}
