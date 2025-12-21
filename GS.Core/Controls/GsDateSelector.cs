using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Seletor de data padrão do GS Core.
    /// Valida data real, mínima e máxima.
    /// </summary>
    public partial class GsDateSelector : GsInputBase
    {
        private TextBox _textBox;

        // ============================
        // PROPRIEDADES
        // ============================

        [Category("GS Core")]
        public bool AllowEmpty { get; set; } = true;

        [Category("GS Core")]
        public DateTime? MinDate { get; set; }

        [Category("GS Core")]
        public DateTime? MaxDate { get; set; }

        [Category("GS Core")]
        public bool StartWithToday { get; set; } = false;

        // ============================
        // CRIAÇÃO DO INNER CONTROL
        // ============================

        protected override TextBoxBase CreateInnerTextBox()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                TextAlign = HorizontalAlignment.Center,
                PlaceholderText = "dd/MM/yyyy"
            };

            if (StartWithToday)
                _textBox.Text = DateTime.Today.ToString("dd/MM/yyyy");

            _textBox.Leave += (_, _) => ValidateDate();

            return _textBox;
        }

        // ============================
        // VALIDAÇÃO DE DATA
        // ============================

        private void ValidateDate()
        {
            ValidateRequired();

            if (string.IsNullOrWhiteSpace(Text))
            {
                if (!AllowEmpty)
                    ShowError("Data obrigatória");

                return;
            }

            if (!DateTime.TryParseExact(
                Text,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime date))
            {
                ShowError("Data inválida");
                return;
            }

            if (MinDate.HasValue && date < MinDate.Value)
            {
                ShowError($"Data mínima: {MinDate:dd/MM/yyyy}");
                return;
            }

            if (MaxDate.HasValue && date > MaxDate.Value)
            {
                ShowError($"Data máxima: {MaxDate:dd/MM/yyyy}");
                return;
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
