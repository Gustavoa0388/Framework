using GS.Core.UI.Controls.Base;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsMaskedInput
    ///
    /// Input com máscara seguindo o padrão GS Core.
    ///
    /// OBJETIVO:
    /// Encapsular um <see cref="MaskedTextBox"/> nativo,
    /// fornecendo:
    /// - Integração com o sistema de tema do GS Core
    /// - Validação local padronizada
    /// - Propagação correta de eventos para UX avançada
    ///
    /// ESTE CONTROLE:
    /// - Suporta validação de campo obrigatório (Required)
    /// - Valida preenchimento completo da máscara
    /// - Não executa regra de negócio
    /// - Não acessa banco de dados
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não valida formato semântico (CPF, CNPJ, etc.)
    /// - Não exibe MessageBox
    /// - Não controla fluxo de formulário
    ///
    /// CASOS DE USO TÍPICOS:
    /// - CPF / CNPJ
    /// - Telefone
    /// - CEP
    /// - Datas formatadas (quando não há DatePicker)
    ///
    /// OBSERVAÇÃO IMPORTANTE:
    /// Este controle SEMPRE propaga eventos de teclado e texto.
    /// Caso contrário, recursos como busca incremental,
    /// debounce e atalhos deixam de funcionar.
    /// </summary>
    public class GsMaskedInput : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Instância interna do MaskedTextBox nativo.
        /// Toda interação real de texto ocorre aqui.
        /// </summary>
        private MaskedTextBox _masked;

        /// <summary>
        /// Máscara configurada antes ou depois
        /// da criação do controle interno.
        /// </summary>
        private string _mask;

        // ==========================================================
        // PROPRIEDADES PÚBLICAS
        // ==========================================================

        /// <summary>
        /// Define ou obtém a máscara do input.
        ///
        /// Exemplos:
        /// - "000.000.000-00" (CPF)
        /// - "(00) 00000-0000" (Telefone)
        ///
        /// Pode ser configurada antes ou depois
        /// da criação do controle.
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
        /// Define se o valor retornado pelo Text
        /// deve incluir os caracteres literais da máscara.
        ///
        /// TRUE:
        /// - Retorna máscara completa (ex: 123.456.789-00)
        ///
        /// FALSE:
        /// - Retorna apenas os dígitos (ex: 12345678900)
        ///
        /// OBS:
        /// Esta propriedade afeta SOMENTE a saída,
        /// não o comportamento visual do input.
        /// </summary>
        [Category("GS Core")]
        public bool SalvarMascara
        {
            get => _masked?.TextMaskFormat == MaskFormat.IncludeLiterals;
            set
            {
                if (_masked != null)
                {
                    _masked.TextMaskFormat = value
                        ? MaskFormat.IncludeLiterals
                        : MaskFormat.ExcludePromptAndLiterals;
                }
            }
        }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o controle interno real (MaskedTextBox).
        ///
        /// Este método é chamado automaticamente pela base
        /// quando o handle do controle é criado.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _masked = new MaskedTextBox
            {
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                Culture = CultureInfo.InvariantCulture,
                PromptChar = '_',
                ResetOnPrompt = false,
                ResetOnSpace = false,
                SkipLiterals = true
            };

            // Aplica máscara se já tiver sido definida
            if (!string.IsNullOrEmpty(_mask))
                _masked.Mask = _mask;

            // ============================
            // PROPAGA EVENTOS ESSENCIAIS
            // ============================

            _masked.TextChanged += (s, e) => OnTextChanged(e);
            _masked.KeyDown += (s, e) => OnKeyDown(e);
            _masked.KeyPress += (s, e) => OnKeyPress(e);
            _masked.KeyUp += (s, e) => OnKeyUp(e);

            return _masked;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Executa a validação local do input.
        ///
        /// FLUXO DE VALIDAÇÃO:
        /// 1) Validação Required (base)
        /// 2) Validação de máscara completa
        ///
        /// REGRAS:
        /// - Se o campo estiver vazio e não for Required, não valida máscara
        /// - Se a máscara não estiver completa, gera erro local
        /// </summary>
        public override void ValidateInput()
        {
            // Validação Required
            base.ValidateInput();

            // Se já houver erro, não continua
            if (HasError)
                return;

            // Se não houver máscara configurada, não valida
            if (_masked == null || string.IsNullOrEmpty(_masked.Mask))
                return;

            // Campo vazio não é erro se não for Required
            if (string.IsNullOrWhiteSpace(_masked.Text))
                return;

            // Máscara incompleta
            if (!_masked.MaskCompleted)
            {
                ShowError("Preenchimento incompleto");
            }
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

        /// <summary>
        /// Propagação da propriedade Text.
        ///
        /// Retorna ou define o valor do MaskedTextBox interno,
        /// respeitando a configuração de SalvarMascara.
        /// </summary>
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
