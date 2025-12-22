using System;
using System.ComponentModel;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Input com máscara seguindo o padrão GS Core.
    /// </summary>
    public partial class GsMaskedInput : GsInputBase
    {
        private MaskedTextBox _masked;
        private string _mask;

        [Category("GS Core")]
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                if (_masked != null)
                    _masked.Mask = value;
            }
        }

        [Category("GS Core")]
        public bool SalvarMascara
        {
            get => _masked?.TextMaskFormat == MaskFormat.IncludeLiterals;
            set
            {
                if (_masked != null)
                    _masked.TextMaskFormat = value
                        ? MaskFormat.IncludeLiterals
                        : MaskFormat.ExcludePromptAndLiterals;
            }
        }

        protected override TextBoxBase CreateInnerTextBox()
        {
            _masked = new MaskedTextBox
            {
                BorderStyle = BorderStyle.None,
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                Culture = System.Globalization.CultureInfo.InvariantCulture,
                PromptChar = '_',
                ResetOnPrompt = false,
                ResetOnSpace = false,
                SkipLiterals = true
            };

            if (!string.IsNullOrEmpty(_mask))
                _masked.Mask = _mask;

            _masked.Leave += (_, _) => Validate();

            return _masked;
        }

        /// <summary>
        /// Validação completa do campo (Required + Máscara)
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (HasError)
                return;

            if (_masked == null || string.IsNullOrEmpty(_masked.Mask))
                return;

            if (string.IsNullOrWhiteSpace(_masked.Text))
                return;

            if (!_masked.MaskCompleted)
            {
                ShowError("Preenchimento incompleto");
            }
        }

        public override string Text
        {
            get => _masked?.Text ?? string.Empty;
            set
            {
                if (_masked != null)
                    _masked.Text = value;
            }
        }
    }
}
