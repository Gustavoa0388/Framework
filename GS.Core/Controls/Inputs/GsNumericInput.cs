using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Input numérico padrão do GS Core.
    /// </summary>
    public partial class GsNumericInput : GsInputBase
    {
        private TextBox _textBox;

        [Category("GS Core")]
        public bool AllowDecimal { get; set; }

        [Category("GS Core")]
        public int DecimalPlaces { get; set; } = 2;

        [Category("GS Core")]
        public decimal? MinValue { get; set; }

        [Category("GS Core")]
        public decimal? MaxValue { get; set; }

        protected override TextBoxBase CreateInnerTextBox()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None
            };

            _textBox.KeyPress += OnKeyPress;
            _textBox.Leave += (_, _) => Validate();

            return _textBox;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalSeparator = '.';

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (AllowDecimal && e.KeyChar == decimalSeparator)
            {
                if (_textBox.Text.Contains(decimalSeparator))
                    e.Handled = true;
                return;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Validação completa (Required + Numérica)
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (HasError)
                return;

            if (string.IsNullOrWhiteSpace(Text))
                return;

            if (!decimal.TryParse(
                Text,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal value))
            {
                ShowError("Valor numérico inválido");
                return;
            }

            if (MinValue.HasValue && value < MinValue.Value)
            {
                ShowError($"Valor mínimo: {MinValue.Value}");
                return;
            }

            if (MaxValue.HasValue && value > MaxValue.Value)
            {
                ShowError($"Valor máximo: {MaxValue.Value}");
                return;
            }

            if (AllowDecimal)
            {
                value = Math.Round(value, DecimalPlaces);
                Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public override string Text
        {
            get => _textBox?.Text ?? string.Empty;
            set
            {
                if (_textBox != null)
                    _textBox.Text = value;
            }
        }
    }
}
