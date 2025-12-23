using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Layout
{
    /// <summary>
    /// GsSideMenuItem
    /// 
    /// Representa um item clicável dentro do GsSideMenu.
    /// 
    /// RESPONSABILIDADES:
    /// - Exibir texto e ícone
    /// - Reagir a hover e seleção
    /// - Disparar evento de clique
    /// - Aplicar tema visual
    /// </summary>
    public class GsSideMenuItem : UserControl, IThemedControl
    {
        private bool _hovered;

        public GsSideMenuItem()
        {
            Height = 40;
            Cursor = Cursors.Hand;

            Padding = new Padding(12, 0, 12, 0);

            MouseEnter += (_, _) => { _hovered = true; Invalidate(); };
            MouseLeave += (_, _) => { _hovered = false; Invalidate(); };
            Click += (_, _) => Clicked?.Invoke(this, EventArgs.Empty);
        }

        // =====================================================
        // PROPRIEDADES
        // =====================================================

        /// <summary>
        /// Texto exibido no item.
        /// </summary>
        [Category("GS Core")]
        public override string Text { get; set; }

        /// <summary>
        /// Ícone opcional do item.
        /// </summary>
        [Category("GS Core")]
        public Image Icon { get; set; }

        /// <summary>
        /// Indica se o item está selecionado.
        /// </summary>
        [Browsable(false)]
        private bool _isSelected;

        [Browsable(false)]
        public bool IsSelected
        {
            get => _isSelected;
            internal set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                Invalidate(); // 👈 ESSENCIAL
            }
        }


        /// <summary>
        /// Evento disparado ao clicar no item.
        /// </summary>
        public event EventHandler Clicked;

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var theme = ThemeManager.Current;

            Color background =
                IsSelected ? theme.Primary :
                _hovered ? theme.PrimaryLight :
                Color.Transparent;

            Color textColor =
                IsSelected ? theme.TextOnPrimary :
                theme.TextSecondary;

            using (var bg = new SolidBrush(background))
                g.FillRectangle(bg, ClientRectangle);

            int x = Padding.Left;

            if (Icon != null)
            {
                int iconSize = 20;
                int y = (Height - iconSize) / 2;
                g.DrawImage(Icon, x, y, iconSize, iconSize);
                x += iconSize + 8;
            }

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                new Rectangle(x, 0, Width - x, Height),
                textColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            Font = theme.DefaultFont;
            Invalidate();
        }
    }
}
