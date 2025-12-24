using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsCheckBox
    ///
    /// CheckBox padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Representar um input booleano (true/false),
    /// integrado ao sistema de validação, tema e UX do GS Core.
    ///
    /// ESTE CONTROLE:
    /// - Herda de <see cref="GsInputBase"/> como container de UX
    /// - Usa <see cref="CheckBox"/> como controle real
    /// - Integra-se ao fluxo de validação (Required)
    /// - Propaga eventos para formulários e containers
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não trabalha com texto como valor
    /// - Não aplica regra de negócio
    /// - Não acessa dados externos
    ///
    /// REGRA DE REQUIRED:
    /// - Required = true → CheckBox deve estar marcado
    /// </summary>
    public class GsCheckBox : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        private CheckBox _checkBox;

        // ==========================================================
        // PROPRIEDADES PÚBLICAS
        // ==========================================================

        /// <summary>
        /// Define se o CheckBox está marcado.
        /// </summary>
        [Category("GS Core")]
        public bool Checked
        {
            get => _checkBox?.Checked ?? false;
            set
            {
                if (_checkBox != null)
                    _checkBox.Checked = value;
            }
        }

        /// <summary>
        /// Texto exibido ao lado do CheckBox.
        /// </summary>
        [Category("GS Core")]
        public override string Text
        {
            get => _checkBox?.Text ?? string.Empty;
            set
            {
                if (_checkBox != null)
                    _checkBox.Text = value;
            }
        }

        /// <summary>
        /// Permite estado indeterminado (true / false / indeterminate).
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get => _checkBox?.ThreeState ?? false;
            set
            {
                if (_checkBox != null)
                    _checkBox.ThreeState = value;
            }
        }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE
        // ==========================================================

        /// <summary>
        /// Cria o controle interno real.
        ///
        /// OBS:
        /// O GsCheckBox NÃO utiliza TextBox interno.
        /// O GsInputBase é usado apenas como container
        /// de validação, layout e tema.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _checkBox = new CheckBox
            {
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            // ============================
            // PROPAGA EVENTOS ESSENCIAIS
            // ============================

            _checkBox.CheckedChanged += (_, _) =>
            {
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            };

            _checkBox.KeyDown += (s, e) => OnKeyDown(e);
            _checkBox.KeyUp += (s, e) => OnKeyUp(e);

            Controls.Add(_checkBox);
            _checkBox.BringToFront();

            UpdateLayout();

            // Não há TextBox real neste controle
            return null;
        }

        // ==========================================================
        // LAYOUT
        // ==========================================================

        /// <summary>
        /// Atualiza o layout do CheckBox interno.
        /// </summary>
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_checkBox == null)
                return;

            _checkBox.Location = new Point(
                Padding.Left,
                Padding.Top
            );
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Valida o estado do CheckBox conforme regras do GS Core.
        /// </summary>
        public override void ValidateInput()
        {
            ClearError();

            if (Required && !_checkBox.Checked)
            {
                ShowError(RequiredMessage);
                return;
            }
        }
    }
}
