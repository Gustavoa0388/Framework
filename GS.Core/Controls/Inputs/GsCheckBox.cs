using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsCheckBox
    ///
    /// Input composto do GS Core UI para seleção booleana.
    ///
    /// CLASSIFICAÇÃO:
    /// - Input COMPOSTO (não baseado em TextBox)
    ///
    /// RESPONSABILIDADE:
    /// - Exibir e controlar estado Checked
    /// - Integrar Required, Theme e UX
    ///
    /// NÃO FAZ:
    /// - Não cria InnerTextBox
    /// - Não sobrescreve CreateInnerTextBox
    /// </summary>
    public class GsCheckBox : GsInputBase
    {
        private readonly CheckBox _checkBox;

        // ==========================================================
        // PROPRIEDADES
        // ==========================================================

        [Category("GS Core")]
        public bool Checked
        {
            get => _checkBox.Checked;
            set => _checkBox.Checked = value;
        }

        // ==========================================================
        // CONSTRUTOR
        // ==========================================================

        public GsCheckBox()
        {
            Height = 28;

            _checkBox = new CheckBox
            {
                AutoSize = true,
                Location = new Point(0, 4)
            };

            _checkBox.CheckedChanged += (_, _) =>
            {
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            };

            Controls.Add(_checkBox);
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

        public override string Text
        {
            get => _checkBox.Text;
            set => _checkBox.Text = value;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        public override void ValidateInput()
        {
            ClearError();

            if (Required && !_checkBox.Checked)
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

            _checkBox.Font = theme.DefaultFont;
            _checkBox.ForeColor = theme.TextPrimary;
            _checkBox.BackColor = Color.Transparent;
        }
    }
}
