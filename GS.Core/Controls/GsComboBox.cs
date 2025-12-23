using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// ComboBox padrão do GS Core.
    /// Input de seleção com suporte a:
    /// - Placeholder visual
    /// - Validação Required
    /// - Restrição a itens da lista
    /// - AutoComplete configurável
    /// </summary>
    public partial class GsComboBox : GsInputBase
    {
        private ComboBox _combo;

        private readonly List<object> _itemsBuffer = new();

        private bool _placeholderActive = true;
        private string _placeholderText = "Selecione...";

        // ==========================================================
        // PROPRIEDADES PÚBLICAS
        // ==========================================================

        /// <summary>
        /// Texto exibido quando nenhum item está selecionado.
        /// Não representa valor válido.
        /// </summary>
        [Category("GS Core")]
        [Description("Texto exibido quando nenhum item está selecionado.")]
        public string Placeholder
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                ApplyPlaceholder();
            }
        }

        /// <summary>
        /// Restringe o valor a itens existentes na lista.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(true)]
        [Description("Impede valores que não estejam presentes na lista.")]
        public bool ApenasItensLista { get; set; } = true;

        /// <summary>
        /// Modo de autocomplete do ComboBox.
        /// </summary>
        [Category("GS Core")]
        public AutoCompleteMode AutoCompleteMode
        {
            get => _combo?.AutoCompleteMode ?? AutoCompleteMode.None;
            set
            {
                if (_combo != null)
                    _combo.AutoCompleteMode = value;
            }
        }

        /// <summary>
        /// Fonte de autocomplete do ComboBox.
        /// </summary>
        [Category("GS Core")]
        public AutoCompleteSource AutoCompleteSource
        {
            get => _combo?.AutoCompleteSource ?? AutoCompleteSource.None;
            set
            {
                if (_combo != null)
                    _combo.AutoCompleteSource = value;
            }
        }

        /// <summary>
        /// Lista de itens do ComboBox.
        /// </summary>
        [Browsable(false)]
        public IList Items => _itemsBuffer;

        /// <summary>
        /// Índice selecionado.
        /// </summary>
        [Category("GS Core")]
        public int SelectedIndex
        {
            get => _combo?.SelectedIndex ?? -1;
            set
            {
                if (_combo != null)
                    _combo.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Valor selecionado.
        /// </summary>
        [Category("GS Core")]
        public object SelectedValue
        {
            get => _combo?.SelectedItem;
            set
            {
                if (_combo != null)
                    _combo.SelectedItem = value;
            }
        }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE INTERNO
        // ==========================================================

        /// <summary>
        /// Cria o controle interno do GsComboBox.
        /// </summary>
        protected override TextBoxBase CreateInnerTextBox()
        {
            _combo = new ComboBox
            {
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ApenasItensLista
                    ? ComboBoxStyle.DropDownList
                    : ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.None,
                AutoCompleteSource = AutoCompleteSource.None
            };

            _combo.GotFocus += (_, _) => RemovePlaceholder();
            _combo.LostFocus += (_, _) => ValidateValue();
            _combo.SelectedIndexChanged += (_, _) => ClearError();

            Controls.Add(_combo);
            _combo.BringToFront();

            SyncItems();
            ApplyPlaceholder();
            UpdateLayout();

            // Dummy exigido pelo contrato do GsInputBase
            return new TextBox();
        }

        // ==========================================================
        // LAYOUT
        // ==========================================================

        /// <summary>
        /// Atualiza layout do controle interno.
        /// </summary>
        protected override void UpdateLayout()
        {
            if (_combo == null)
                return;

            _combo.Location = new Point(Padding.Left, Padding.Top);
            _combo.Size = new Size(
                Width - Padding.Horizontal,
                Height - Padding.Vertical
            );
        }

        // ==========================================================
        // PLACEHOLDER
        // ==========================================================

        private void ApplyPlaceholder()
        {
            if (_combo == null)
                return;

            if (_itemsBuffer.Count > 0)
                return;

            _placeholderActive = true;
            _combo.Items.Clear();
            _combo.ForeColor = SystemColors.GrayText;
            _combo.Items.Add(_placeholderText);
            _combo.SelectedIndex = 0;
        }

        private void RemovePlaceholder()
        {
            if (!_placeholderActive)
                return;

            _placeholderActive = false;
            _combo.ForeColor = SystemColors.WindowText;
            SyncItems();
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        /// <summary>
        /// Valida o valor selecionado conforme regras do GS Core.
        /// </summary>
        private void ValidateValue()
        {
            // Required
            if (Required && (_placeholderActive || _combo.SelectedIndex < 0))
            {
                ShowError(RequiredMessage);
                return;
            }

            // Apenas itens da lista
            if (ApenasItensLista && _combo.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(_combo.Text))
            {
                ShowError("Selecione um item válido da lista.");
                return;
            }

            ClearError();
        }

        // ==========================================================
        // SINCRONIZAÇÃO DE ITENS
        // ==========================================================

        private void SyncItems()
        {
            if (_combo == null)
                return;

            _combo.Items.Clear();

            foreach (var item in _itemsBuffer)
                _combo.Items.Add(item);
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

        /// <summary>
        /// Texto do item selecionado.
        /// </summary>
        public override string Text
        {
            get => _combo?.Text ?? string.Empty;
            set
            {
                if (_combo != null)
                    _combo.Text = value;
            }
        }
    }
}
