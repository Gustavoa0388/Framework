using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Inputs
{
    /// <summary>
    /// GsComboBox
    ///
    /// ComboBox padrão do GS Core.
    ///
    /// OBJETIVO:
    /// Representar um input de seleção,
    /// integrado ao sistema de validação,
    /// tema e UX do GS Core.
    ///
    /// ESTE CONTROLE:
    /// - Usa ComboBox como controle real
    /// - Suporta placeholder VISUAL (não faz parte dos dados)
    /// - Integra-se ao fluxo de validação (Required)
    /// - Propaga eventos para UX avançada
    ///
    /// ESTE CONTROLE NÃO FAZ:
    /// - Não gerencia binding complexo
    /// - Não executa regra de negócio
    /// - Não acessa dados externos
    /// </summary>
    public partial class GsComboBox : GsInputBase
    {
        // ==========================================================
        // CONTROLE INTERNO
        // ==========================================================

        private ComboBox _combo;

        // ==========================================================
        // PLACEHOLDER
        // ==========================================================

        private string _placeholderText = "Selecione...";

        // ==========================================================
        // CONFIGURAÇÕES
        // ==========================================================

        /// <summary>
        /// Texto exibido quando nenhum item está selecionado.
        /// Não representa valor válido.
        /// </summary>
        [Category("GS Core")]
        public string Placeholder
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Restringe o valor a itens existentes na lista.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(true)]
        public bool ApenasItensLista { get; set; } = true;

        /// <summary>
        /// Coleção de itens do ComboBox.
        /// </summary>
        [Browsable(false)]
        public IList Items => _combo?.Items;

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
        /// Item selecionado.
        /// </summary>
        [Category("GS Core")]
        public object SelectedItem
        {
            get => _combo?.SelectedItem;
            set
            {
                if (_combo != null)
                    _combo.SelectedItem = value;
            }
        }

        // ==========================================================
        // CRIAÇÃO DO CONTROLE
        // ==========================================================

        protected override TextBoxBase CreateInnerTextBox()
        {
            _combo = new ComboBox
            {
                DropDownStyle = ApenasItensLista
                    ? ComboBoxStyle.DropDownList
                    : ComboBoxStyle.DropDown
            };

            // ============================
            // EVENTOS
            // ============================

            _combo.SelectedIndexChanged += (_, _) =>
            {
                OnTextChanged(EventArgs.Empty);
                ValidateInput();
            };

            _combo.TextChanged += (_, _) => OnTextChanged(EventArgs.Empty);
            _combo.KeyDown += (s, e) => OnKeyDown(e);
            _combo.KeyUp += (s, e) => OnKeyUp(e);

            Controls.Add(_combo);
            _combo.BringToFront();

            return null; // Não há TextBox interno
        }

        // ==========================================================
        // LAYOUT
        // ==========================================================

        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (_combo == null)
                return;

            _combo.Location = new Point(Padding.Left, Padding.Top);
            _combo.Size = new Size(
                Width - Padding.Horizontal,
                Height - Padding.Vertical
            );
        }

        // ==========================================================
        // PLACEHOLDER VISUAL
        // ==========================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_combo == null)
                return;

            // Só desenha placeholder se:
            // - Nada selecionado
            // - Texto vazio
            // - Não está focado
            if (_combo.SelectedIndex >= 0)
                return;

            if (!string.IsNullOrEmpty(_combo.Text))
                return;

            if (_combo.Focused)
                return;

            var theme = ThemeManager.Current;

            TextRenderer.DrawText(
                e.Graphics,
                _placeholderText,
                Font,
                new Rectangle(
                    Padding.Left + 4,
                    Padding.Top,
                    Width,
                    Height
                ),
                theme.TextSecondary,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );
        }

        // ==========================================================
        // VALIDAÇÃO
        // ==========================================================

        public override void ValidateInput()
        {
            ClearError();

            if (Required && _combo.SelectedIndex < 0)
            {
                ShowError(RequiredMessage);
                return;
            }

            if (ApenasItensLista && _combo.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(_combo.Text))
            {
                ShowError("Selecione um item válido da lista.");
                return;
            }
        }

        // ==========================================================
        // TEXTO
        // ==========================================================

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
