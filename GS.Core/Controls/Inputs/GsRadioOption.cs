using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsRadioOption
    ///
    /// Opção de seleção única do GS Core UI.
    ///
    /// CLASSIFICAÇÃO:
    /// - Input COMPOSTO (não baseado em TextBox)
    ///
    /// RESPONSABILIDADE:
    /// - Representar uma opção de RadioButton
    /// - Integrar Required, Theme e UX
    ///
    /// NÃO FAZ:
    /// - Não cria InnerTextBox
    /// - Não gerencia grupos automaticamente
    /// </summary>
    public class GsRadioOption : GsInputBase
    {
        private readonly RadioButton _radio;

        // ==========================================================
        // PROPRIEDADES
        // ==========================================================

        [Category("GS Core")]
        public bool Checked
        {
            get => _radio.Checked;
            set => _radio.Checked = value;
        }

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public GsRadioOption()
        {
            Height = 28;

            _radio = new RadioButton
            {
                AutoSize = true,
                Location = new Point(0, 4)
            };

            _radio.CheckedChanged += (_, _) =>
            {
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            };

            Controls.Add(_radio);
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

        public override string Text
        {
            get => _radio.Text;
            set => _radio.Text = value;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        public override void ValidateInput()
        {
            ClearError();

            if (Required && !_radio.Checked)
            {
                ShowError(RequiredMessage);
            }
        }

        // ==========================================================
        // TEMA
        // ==========================================================

        public override void ApplyTheme(GsTheme theme)
        {
            base.ApplyTheme(theme);

            _radio.Font = theme.DefaultFont;
            _radio.ForeColor = theme.TextPrimary;
            _radio.BackColor = Color.Transparent;
        }
    }
}
