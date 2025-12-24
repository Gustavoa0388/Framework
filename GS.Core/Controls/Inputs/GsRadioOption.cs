using GS.Core.UI.Theming;
using GS.Core.UI.Controls.Base;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsRadioOption
    ///
    /// RadioButton padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Representar uma opção exclusiva dentro de um grupo,
    /// integrada ao sistema de tema, validação e UX do GS Core.
    ///
    /// ESTE CONTROLE:
    /// - Herda de <see cref="GsInputBase"/> (pipeline oficial de inputs)
    /// - Usa <see cref="RadioButton"/> como controle real
    /// - Possui renderização custom (owner draw)
    /// - Integra-se ao sistema de Theme
    /// - Participa do fluxo de validação (Required)
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não gerencia grupos
    /// - Não valida regra de negócio
    /// - Não acessa dados externos
    ///
    /// REGRA DE REQUIRED:
    /// - Required = true → opção deve estar marcada
    /// </summary>
    public class GsRadioOption : GsInputBase
    {
        // =====================================================
        // CONTROLE INTERNO
        // =====================================================

        private RadioButton _radio;
        private GsTheme _theme;

        private const int RadioSize = 14;

        // =====================================================
        // PROPRIEDADES PÚBLICAS
        // =====================================================

        /// <summary>
        /// Define se a opção está selecionada.
        /// </summary>
        [Category("GS Core")]
        public bool Checked
        {
            get => _radio?.Checked ?? false;
            set
            {
                if (_radio != null)
                    _radio.Checked = value;
            }
        }

        /// <summary>
        /// Texto exibido ao lado da opção.
        /// </summary>
        [Category("GS Core")]
        public override string Text
        {
            get => _radio?.Text ?? string.Empty;
            set
            {
                if (_radio != null)
                    _radio.Text = value;
            }
        }

        // =====================================================
        // CRIAÇÃO DO CONTROLE
        // =====================================================

        /// <summary>
        /// Cria o RadioButton interno real.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _radio = new RadioButton
            {
                AutoSize = false,
                Height = 22,
                Cursor = Cursors.Hand,
                Padding = new Padding(RadioSize + 6, 0, 0, 0)
            };

            // ============================
            // EVENTOS
            // ============================

            _radio.CheckedChanged += (_, _) =>
            {
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            };

            _radio.KeyDown += (s, e) => OnKeyDown(e);
            _radio.KeyUp += (s, e) => OnKeyUp(e);

            Controls.Add(_radio);
            _radio.BringToFront();

            return null; // Não há TextBox interno
        }

        // =====================================================
        // TEMA
        // =====================================================

        /// <summary>
        /// Aplica o tema visual ao controle.
        /// </summary>
        public override void ApplyTheme(GsTheme theme)
        {
            _theme = theme;

            Font = theme.DefaultFont;
            ForeColor = theme.RadioText;

            Invalidate();
        }

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_theme == null)
                return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? BackColor);

            Rectangle circle = new Rectangle(
                Padding.Left - RadioSize - 6,
                (Height - RadioSize) / 2,
                RadioSize,
                RadioSize
            );

            using (var pen = new Pen(_theme.RadioBorder, 1))
                g.DrawEllipse(pen, circle);

            if (Checked)
            {
                Rectangle inner = Rectangle.Inflate(circle, -4, -4);
                using (var brush = new SolidBrush(_theme.RadioFill))
                    g.FillEllipse(brush, inner);
            }

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                new Rectangle(Padding.Left, 0, Width, Height),
                ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );
        }

        // =====================================================
        // VALIDAÇÃO
        // =====================================================

        /// <summary>
        /// Valida o estado da opção conforme regras do GS Core.
        /// </summary>
        public override void ValidateInput()
        {
            ClearError();

            if (Required && !_radio.Checked)
            {
                ShowError(RequiredMessage);
                return;
            }
        }
    }
}
