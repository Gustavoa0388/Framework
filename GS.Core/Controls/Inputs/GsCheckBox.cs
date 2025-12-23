using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// CheckBox padrão do GS Core.
    /// Representa um input booleano com suporte a Required
    /// e integração com o fluxo padrão de validação.
    /// </summary>
    public partial class GsCheckBox : GsInputBase
    {
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
        /// Permite estado indeterminado.
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
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o CheckBox interno.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _checkBox = new CheckBox
            {
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            _checkBox.CheckedChanged += (_, _) =>
            {
                ValidateValue();
            };

            Controls.Add(_checkBox);
            _checkBox.BringToFront();

            UpdateLayout();

            // Dummy exigido pelo contrato do GsInputBase
            return new TextBox();
        }

        // ==========================================================
        // LAYOUT
        // ==========================================================

        /// <summary>
        /// Atualiza o layout do CheckBox interno.
        /// </summary>
        protected override void UpdateLayout()
        {
            if (_checkBox == null)
                return;

            _checkBox.Location = new Point(Padding.Left, Padding.Top);
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Valida o estado do CheckBox conforme regras do GS Core.
        /// </summary>
        private void ValidateValue()
        {
            if (Required && !_checkBox.Checked)
            {
                ShowError(RequiredMessage);
                return;
            }

            ClearError();
        }
    }
}
