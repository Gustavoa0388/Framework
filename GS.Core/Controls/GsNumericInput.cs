using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Input numérico padrão do GS Core.
    /// Suporta validação de valor mínimo, máximo e decimais.
    /// </summary>
    public partial class GsNumericInput : GsInputBase

    {
        private TextBox _textBox;

        private decimal? _minValue;
        private decimal? _maxValue;

        // ============================
        // PROPRIEDADES PÚBLICAS
        // ============================

        [Category("GS Core")]
        public bool AllowDecimal { get; set; } = false;

        [Category("GS Core")]
        public int DecimalPlaces { get; set; } = 2;

        [Category("GS Core")]
        public decimal? MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        [Category("GS Core")]
        public decimal? MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        // ============================
        // CRIAÇÃO DO INNER CONTROL
        // ============================

        protected override TextBoxBase CreateInnerTextBox()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None
            };

            _textBox.KeyPress += OnKeyPress;
            _textBox.Leave += (_, _) => ValidateNumeric();

            return _textBox;
        }

        // ============================
        // RESTRIÇÃO DE DIGITAÇÃO
        // ============================

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalSeparator = '.';

            // Permite controle (backspace, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Permite dígitos
            if (char.IsDigit(e.KeyChar))
                return;

            // Permite separador decimal (se habilitado)
            if (AllowDecimal && e.KeyChar == decimalSeparator)
            {
                if (_textBox.Text.Contains(decimalSeparator))
                    e.Handled = true;

                return;
            }

            // Bloqueia qualquer outra coisa
            e.Handled = true;
        }

        // ============================
        // VALIDAÇÃO NUMÉRICA
        // ============================

        private void ValidateNumeric()
        {
            ValidateRequired();

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

            // Ajusta casas decimais
            if (AllowDecimal)
            {
                value = Math.Round(value, DecimalPlaces);
                Text = value.ToString(CultureInfo.InvariantCulture);
            }

            ClearError();
        }

        // ============================
        // TEXTO
        // ============================

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
