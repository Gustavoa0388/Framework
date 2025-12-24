using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsToggleSwitch
    ///
    /// Controle de alternância (On/Off) do GS Core UI.
    ///
    /// CLASSIFICAÇÃO:
    /// - Input de ESTADO
    /// - Input COMPOSTO (não baseado em TextBox)
    ///
    /// RESPONSABILIDADES:
    /// - Representar estado booleano
    /// - Integrar Required, Theme e UX
    ///
    /// NÃO FAZ:
    /// - Não cria InnerTextBox
    /// - Não executa lógica de negócio
    /// </summary>
    public class GsToggleSwitch : GsInputBase
    {
        private bool _checked;

        // ==========================================================
        // PROPRIEDADES
        // ==========================================================

        [Category("GS Core")]
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked == value)
                    return;

                _checked = value;
                Invalidate();
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            }
        }

        [Category("GS Core")]
        public string OnText { get; set; } = "On";

        [Category("GS Core")]
        public string OffText { get; set; } = "Off";

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public GsToggleSwitch()
        {
            Height = 24;
            Width = 50;
            Cursor = Cursors.Hand;
            TabStop = true;
        }

        // ==========================================================
        // INPUT
        // ==========================================================

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = !Checked;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                Checked = !Checked;
                e.Handled = true;
            }
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        public override void ValidateInput()
        {
            ClearError();

            if (Required && !Checked)
            {
                ShowError(RequiredMessage);
            }
        }

        // ==========================================================
        // THEME
        // ==========================================================

        public override void ApplyTheme(GsTheme theme)
        {
            base.ApplyTheme(theme);
            Font = theme.DefaultFont;
        }

        // ==========================================================
        // RENDERIZAÇÃO
        // ==========================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.Transparent);

            var theme = ThemeManager.Current;

            var track = new Rectangle(0, 0, Width - 1, Height - 1);
            var thumb = new Rectangle(
                Checked ? Width - Height : 0,
                0,
                Height,
                Height
            );

            using (var bg = new SolidBrush(Checked
                ? theme.ToggleOnBackground
                : theme.ToggleOffBackground))
            {
                g.FillEllipse(bg, track);
            }

            using (var thumbBrush = new SolidBrush(theme.ToggleThumb))
            {
                g.FillEllipse(thumbBrush, thumb);
            }

            string text = Checked ? OnText : OffText;

            if (!string.IsNullOrWhiteSpace(text))
            {
                TextRenderer.DrawText(
                    g,
                    text,
                    Font,
                    new Rectangle(Width + 6, 0, 200, Height),
                    theme.ToggleText,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
            }
        }
    }
}
