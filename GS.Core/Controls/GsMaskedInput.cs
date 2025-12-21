using System;
using System.ComponentModel;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Input com máscara seguindo o padrão GS Core.
    /// - Herda layout, foco, erro e tema do GsInputBase
    /// - Usa MaskedTextBox internamente
    /// - Suporta Required
    /// - Valida preenchimento completo da máscara
    /// </summary>
    public partial class GsMaskedInput : GsInputBase
    {
        private MaskedTextBox _masked;
        private string _mask;

        /// <summary>
        /// Define a máscara do campo (ex: 000.000.000-00)
        /// </summary>
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


        /// <summary>
        /// Define se o texto retornado inclui os literais da máscara
        /// </summary>
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

        // ======================================================
        // CRIAÇÃO DO INNER CONTROL
        // ======================================================
        protected override TextBoxBase CreateInnerTextBox()
        {
            _masked = new MaskedTextBox
            {
                BorderStyle = BorderStyle.None,
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,

                // 🔒 TRAVAS DE COMPORTAMENTO
                Culture = System.Globalization.CultureInfo.InvariantCulture,
                PromptChar = '_',
                ResetOnPrompt = false,
                ResetOnSpace = false,
                SkipLiterals = true
            };

            if (!string.IsNullOrEmpty(_mask))
                _masked.Mask = _mask;

            _masked.Leave += (_, _) => ValidateMask();

            return _masked;
        }

        // ======================================================
        // VALIDAÇÃO DE MÁSCARA
        // ======================================================
        private void ValidateMask()
        {
            // Primeiro valida Required (regra base)
            ValidateRequired();

            if (_masked == null || string.IsNullOrEmpty(_masked.Mask))
                return;

            if (string.IsNullOrWhiteSpace(_masked.Text))
                return;

            if (!_masked.MaskCompleted)
            {
                ShowError("Preenchimento incompleto");
            }
            else
            {
                ClearError();
            }
        }

        // ======================================================
        // TEXTO (override para garantir retorno correto)
        // ======================================================
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
