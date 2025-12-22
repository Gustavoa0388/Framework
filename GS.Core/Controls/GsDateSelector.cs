using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Seletor de data padrão do GS Core.
    /// Valida formato, limites e obrigatoriedade.
    /// </summary>
    public partial class GsDateSelector : GsInputBase
    {
        private TextBox _textBox;

        [Category("GS Core")]
        public bool AllowEmpty { get; set; } = true;

        [Category("GS Core")]
        public DateTime? MinDate { get; set; }

        [Category("GS Core")]
        public DateTime? MaxDate { get; set; }

        [Category("GS Core")]
        public bool StartWithToday { get; set; }

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

            _textBox.Leave += (_, _) => Validate();

            return _textBox;
        }

        /// <summary>
        /// Validação completa (Required + Data)
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (HasError)
                return;

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
