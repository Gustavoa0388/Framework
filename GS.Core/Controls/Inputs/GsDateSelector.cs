using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsDateSelector
    ///
    /// Seletor de data padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Permitir entrada de datas em formato fixo (dd/MM/yyyy),
    /// com validação local padronizada e integração completa
    /// com o sistema de validação do GS Core.
    ///
    /// ESTE CONTROLE:
    /// - Valida formato de data
    /// - Suporta limites mínimo e máximo
    /// - Integra-se ao Required do GsInputBase
    /// - Propaga eventos para UX avançada
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não abre calendário visual (DatePicker)
    /// - Não executa regra de negócio
    /// - Não converte fuso horário
    ///
    /// CASOS DE USO TÍPICOS:
    /// - Datas simples de cadastro
    /// - Datas de validade
    /// - Datas de referência
    ///
    /// OBSERVAÇÃO IMPORTANTE:
    /// O formato aceito é SEMPRE "dd/MM/yyyy".
    /// A cultura utilizada é InvariantCulture
    /// para garantir consistência.
    /// </summary>
    public class GsDateSelector : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// TextBox interno responsável pela entrada textual da data.
        /// </summary>
        private TextBox _textBox;

        // ==========================================================
        // CONFIGURAÇÕES
        // ==========================================================

        /// <summary>
        /// Define se o campo pode permanecer vazio.
        ///
        /// IMPORTANTE:
        /// - Required (base) tem precedência global
        /// - AllowEmpty controla apenas o comportamento local
        /// </summary>
        [Category("GS Core")]
        public bool AllowEmpty { get; set; } = true;

        /// <summary>
        /// Data mínima permitida (opcional).
        /// </summary>
        [Category("GS Core")]
        public DateTime? MinDate { get; set; }

        /// <summary>
        /// Data máxima permitida (opcional).
        /// </summary>
        [Category("GS Core")]
        public DateTime? MaxDate { get; set; }

        /// <summary>
        /// Define se o campo deve iniciar preenchido
        /// com a data atual.
        /// </summary>
        [Category("GS Core")]
        public bool StartWithToday { get; set; }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o controle interno real (TextBox).
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _textBox = new TextBox
            {
                TextAlign = HorizontalAlignment.Center,
                PlaceholderText = "dd/MM/yyyy"
            };

            // Preenche com a data atual, se configurado
            if (StartWithToday)
                _textBox.Text = DateTime.Today.ToString("dd/MM/yyyy");

            // ============================
            // PROPAGA EVENTOS ESSENCIAIS
            // ============================

            _textBox.TextChanged += (s, e) => OnTextChanged(e);
            _textBox.KeyDown += (s, e) => OnKeyDown(e);
            _textBox.KeyPress += (s, e) => OnKeyPress(e);
            _textBox.KeyUp += (s, e) => OnKeyUp(e);

            return _textBox;
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Executa a validação local do seletor de data.
        ///
        /// FLUXO DE VALIDAÇÃO:
        /// 1) Validação Required (base)
        /// 2) Validação de campo vazio (AllowEmpty)
        /// 3) Validação de formato de data
        /// 4) Validação de limites mínimo e máximo
        /// </summary>
        public override void ValidateInput()
        {
            // Validação Required
            base.ValidateInput();

            if (HasError)
                return;

            // Campo vazio
            if (string.IsNullOrWhiteSpace(Text))
            {
                if (!AllowEmpty)
                    ShowError("Data obrigatória");

                return;
            }

            // Validação de formato
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

            // Validação de data mínima
            if (MinDate.HasValue && date < MinDate.Value)
            {
                ShowError($"Data mínima: {MinDate:dd/MM/yyyy}");
                return;
            }

            // Validação de data máxima
            if (MaxDate.HasValue && date > MaxDate.Value)
            {
                ShowError($"Data máxima: {MaxDate:dd/MM/yyyy}");
                return;
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
