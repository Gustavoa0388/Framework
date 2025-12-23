using System;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsSampleInput
    ///
    /// DESCRIÇÃO:
    /// Input de exemplo do GS Core.
    ///
    /// RESPONSABILIDADES:
    /// - Encapsular um controle nativo (TextBox, MaskedTextBox, etc.)
    /// - Aplicar tema via GsInputBase
    /// - Repassar eventos essenciais
    /// - Executar validação local (Required + regras próprias)
    ///
    /// NÃO FAZ:
    /// - Não acessa banco
    /// - Não valida regra de negócio
    /// - Não exibe MessageBox
    /// </summary>
    public class GsSampleInput : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        private TextBox _inner;

        /// <summary>
        /// Cria o controle interno real.
        /// Este método é obrigatório em todo input.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _inner = new TextBox();

            // =============================
            // PROPAGA EVENTOS ESSENCIAIS
            // =============================

            _inner.TextChanged += (s, e) => OnTextChanged(e);
            _inner.KeyDown += (s, e) => OnKeyDown(e);
            _inner.KeyPress += (s, e) => OnKeyPress(e);
            _inner.KeyUp += (s, e) => OnKeyUp(e);

            return _inner;
        }

        // ==========================================================
        // PROPRIEDADES
        // ==========================================================

        /// <summary>
        /// Propagação da propriedade Text.
        /// Nunca manter valor duplicado.
        /// </summary>
        public override string Text
        {
            get => _inner?.Text ?? string.Empty;
            set
            {
                if (_inner != null)
                    _inner.Text = value;
            }
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Validação local do input.
        /// Sempre chamar base.ValidateInput().
        /// </summary>
        public override void ValidateInput()

        {
            // Validação Required
            base.ValidateInput();

            // Regras adicionais SOMENTE se ainda não houver erro
            if (!HasError && !IsValorValido())
            {
                ShowError("Valor inválido");
            }
        }

        /// <summary>
        /// Regra local fictícia (exemplo).
        /// </summary>
        private bool IsValorValido()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }
    }
}
