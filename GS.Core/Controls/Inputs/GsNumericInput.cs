using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsNumericInput
    ///
    /// Input numérico padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Encapsular um <see cref="TextBox"/> nativo,
    /// permitindo entrada controlada de valores numéricos,
    /// com validação local e integração completa com o GS Core.
    ///
    /// ESTE CONTROLE:
    /// - Suporta números inteiros e decimais
    /// - Permite configuração de casas decimais
    /// - Suporta valores mínimo e máximo
    /// - Integra-se ao sistema de validação (Required)
    /// - Propaga eventos para UX avançada
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não formata moeda
    /// - Não aplica regra de negócio
    /// - Não acessa banco de dados
    ///
    /// CASOS DE USO TÍPICOS:
    /// - Quantidade
    /// - Valores numéricos genéricos
    /// - Percentuais simples
    ///
    /// OBSERVAÇÃO:
    /// A cultura utilizada é sempre InvariantCulture,
    /// garantindo consistência independente da localidade.
    /// </summary>
    public class GsNumericInput : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// TextBox interno responsável pela entrada real de dados.
        /// </summary>
        private TextBox _textBox;

        // ==========================================================
        // CONFIGURAÇÕES NUMÉRICAS
        // ==========================================================

        /// <summary>
        /// Define se o input permite números decimais.
        /// Quando FALSE, apenas números inteiros são aceitos.
        /// </summary>
        [Category("GS Core")]
        public bool AllowDecimal { get; set; }

        /// <summary>
        /// Define a quantidade de casas decimais
        /// permitidas quando <see cref="AllowDecimal"/> for TRUE.
        /// </summary>
        [Category("GS Core")]
        public int DecimalPlaces { get; set; } = 2;

        /// <summary>
        /// Valor mínimo permitido (opcional).
        /// </summary>
        [Category("GS Core")]
        public decimal? MinValue { get; set; }

        /// <summary>
        /// Valor máximo permitido (opcional).
        /// </summary>
        [Category("GS Core")]
        public decimal? MaxValue { get; set; }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o controle interno real (TextBox).
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _textBox = new TextBox();

            // ============================
            // PROPAGA EVENTOS ESSENCIAIS
            // ============================

            _textBox.TextChanged += (s, e) => OnTextChanged(e);
            _textBox.KeyDown += (s, e) => OnKeyDown(e);
            _textBox.KeyPress += OnInnerKeyPress;
            _textBox.KeyUp += (s, e) => OnKeyUp(e);

            return _textBox;
        }

        // ==========================================================
        // CONTROLE DE DIGITAÇÃO
        // ==========================================================

        /// <summary>
        /// Controla quais caracteres podem ser digitados
        /// no input numérico.
        /// </summary>
        private void OnInnerKeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalSeparator = '.';

            // Permite teclas de controle (Backspace, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Permite dígitos
            if (char.IsDigit(e.KeyChar))
                return;

            // Permite separador decimal se habilitado
            if (AllowDecimal && e.KeyChar == decimalSeparator)
            {
                if (_textBox.Text.Contains(decimalSeparator))
                    e.Handled = true;

                return;
            }

            // Bloqueia qualquer outro caractere
            e.Handled = true;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Executa a validação local do input numérico.
        ///
        /// FLUXO DE VALIDAÇÃO:
        /// 1) Validação Required (base)
        /// 2) Validação de formato numérico
        /// 3) Validação de limites mínimo e máximo
        /// 4) Ajuste de casas decimais (se aplicável)
        /// </summary>
        public override void ValidateInput()
        {
            // Validação Required
            base.ValidateInput();

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

            // Normaliza casas decimais
            if (AllowDecimal)
            {
                value = Math.Round(value, DecimalPlaces);
                Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

        /// <summary>
        /// Propagação da propriedade Text.
        /// </summary>
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
