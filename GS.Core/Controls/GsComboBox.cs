using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using System.Collections;


namespace GS.Core.UI.Controls
{
    /// <summary>
    /// ComboBox padrão do GS Core.
    /// Suporta Required, placeholder fake e validação automática.
    /// </summary>
    public partial class GsComboBox : GsInputBase
    {
        private ComboBox _combo;

        private string _placeholderText = "Selecione...";
        private bool _placeholderActive = true;
        private readonly List<object> _pendingItems = new();


        // ============================
        // PROPRIEDADES PÚBLICAS
        // ============================

        [Category("GS Core")]
        public string Placeholder
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                ApplyPlaceholder();
            }
        }

        [Browsable(false)]
        public IList Items => _pendingItems;

        [Category("GS Core")]
        public object SelectedValue
        {
            get => _combo.SelectedValue;
            set => _combo.SelectedValue = value;
        }

        [Category("GS Core")]
        public int SelectedIndex
        {
            get => _combo.SelectedIndex;
            set => _combo.SelectedIndex = value;
        }

        // ============================
        // CRIAÇÃO DO INNER CONTROL
        // ============================

        protected override TextBoxBase CreateInnerTextBox()
        {
            // ComboBox não herda de TextBoxBase, então usamos um "dummy"
            // e ignoramos o TextBox interno do GsInputBase
            _combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            SyncItems();
            ApplyPlaceholder();

            _combo.GotFocus += (_, _) =>
            {
                RemovePlaceholder();
            };

            _combo.LostFocus += (_, _) =>
            {
                ValidateCombo();
            };

            Controls.Add(_combo);
            _combo.BringToFront();

            ApplyPlaceholder();
            UpdateLayout();

            return new TextBox(); // dummy, não utilizado
        }

        // ============================
        // LAYOUT
        // ============================

        protected override void UpdateLayout()
        {
            if (_combo == null)
                return;

            _combo.Location = new Point(Padding.Left, Padding.Top);
            _combo.Width = Width - Padding.Horizontal;
            _combo.Height = Height - Padding.Vertical;
        }

        // ============================
        // PLACEHOLDER FAKE
        // ============================

        private void ApplyPlaceholder()
        {
            if (_combo == null)
                return;

            if (_pendingItems.Count > 0)
                return;

            _placeholderActive = true;
            _combo.ForeColor = SystemColors.GrayText;
            _combo.Items.Clear();
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


        // ============================
        // VALIDAÇÃO
        // ============================

        private void ValidateCombo()
        {
            if (Required && (_placeholderActive || _combo.SelectedIndex < 0))
            {
                ShowError(RequiredMessage);
            }
            else
            {
                ClearError();
            }
        }

        // ============================
        // SINCRONIZAÇÃO DE ITENS
        // ============================

        private void SyncItems()
        {
            if (_combo == null)
                return;

            _combo.Items.Clear();

            foreach (var item in _pendingItems)
                _combo.Items.Add(item);
        }



        // ============================
        // TEXTO (override)
        // ============================

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
