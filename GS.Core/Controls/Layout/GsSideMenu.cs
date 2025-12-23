using System;
using System.ComponentModel;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Layout
{
    /// <summary>
    /// GsSideMenu
    /// 
    /// Container de navegação lateral do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Organizar itens verticalmente
    /// - Garantir seleção única
    /// - Disparar evento de navegação
    /// - Integrar com o sistema de temas
    /// 
    /// NÃO FAZ:
    /// - Desenho gráfico complexo
    /// - Gradientes ou efeitos visuais
    /// - Navegação direta entre telas
    /// </summary>
    public class GsSideMenu : Panel, IThemedControl
    {
        private GsSideMenuItem _selectedItem;

        public GsSideMenu()
        {
            AutoScroll = true;
            Dock = DockStyle.Left;
            Width = 220;

            ItemSpacing = 4;
            Padding = new Padding(8);

            ControlAdded += OnControlAdded;
        }

        // =====================================================
        // PROPRIEDADES
        // =====================================================

        /// <summary>
        /// Item atualmente selecionado no menu.
        /// </summary>
        [Browsable(false)]
        public GsSideMenuItem SelectedItem => _selectedItem;

        /// <summary>
        /// Espaçamento vertical entre os itens do menu.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(4)]
        public int ItemSpacing { get; set; }

        /// <summary>
        /// Evento disparado quando um item é selecionado.
        /// </summary>
        public event EventHandler<GsSideMenuItem> ItemSelected;

        // =====================================================
        // REGISTRO DE ITENS
        // =====================================================

        /// <summary>
        /// Adiciona um item ao menu.
        /// </summary>
        public void AddItem(GsSideMenuItem item)
        {
            Controls.Add(item);
            LayoutItems();
        }

        private void OnControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is not GsSideMenuItem item)
                return;

            item.Clicked += (_, _) => SelectItem(item);
            LayoutItems();
        }

        // =====================================================
        // SELEÇÃO
        // =====================================================

        private void SelectItem(GsSideMenuItem item)
        {
            if (_selectedItem == item)
                return;

            if (_selectedItem != null)
                _selectedItem.IsSelected = false;

            _selectedItem = item;
            _selectedItem.IsSelected = true;

            ItemSelected?.Invoke(this, item);
        }

        // =====================================================
        // LAYOUT
        // =====================================================

        private void LayoutItems()
        {
            int y = Padding.Top;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is not GsSideMenuItem item)
                    continue;

                item.Width = ClientSize.Width - Padding.Horizontal;
                item.Location = new System.Drawing.Point(Padding.Left, y);

                y += item.Height + ItemSpacing;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutItems();
        }

        // =====================================================
        // THEME
        // =====================================================

        /// <summary>
        /// Aplica o tema visual ao menu e seus itens.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            BackColor = theme.PrimaryDark;
            ForeColor = theme.TextOnPrimary;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is IThemedControl themed)
                    themed.ApplyTheme(theme);
            }

            Invalidate();
        }
    }
}
