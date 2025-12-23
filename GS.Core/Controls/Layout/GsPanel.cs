using System.ComponentModel;
using System.Windows.Forms;
using GS.Core.UI.Controls.States;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls.Layout

{
    /// <summary>
    /// GsPanel
    /// 
    /// Container estrutural do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Organizar layout
    /// - Aplicar superfície temática
    /// - Opcionalmente exibir borda
    /// 
    /// NÃO FAZ:
    /// - Sombra
    /// - Arredondamento customizado
    /// - Renderização manual
    /// - Efeitos visuais complexos
    /// </summary>
    public class GsPanel : Panel, IThemedControl
    {
        public GsPanel()
        {
            DoubleBuffered = true;

            Surface = GsPanelSurface.Default;
            ShowBorder = false;
            ContentPadding = Padding.Empty;

            Padding = ContentPadding;
        }

        // =====================================================
        // PROPRIEDADES PÚBLICAS
        // =====================================================

        /// <summary>
        /// Define o tipo de superfície visual do painel.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(GsPanelSurface.Default)]
        public GsPanelSurface Surface { get; set; }

        /// <summary>
        /// Indica se o painel deve exibir borda.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(false)]
        public bool ShowBorder { get; set; }

        /// <summary>
        /// Padding interno do conteúdo.
        /// </summary>
        [Category("GS Core")]
        public Padding ContentPadding { get; set; }

        // =====================================================
        // THEME
        // =====================================================

        /// <summary>
        /// Aplica o tema visual ao painel.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            BackColor = ResolveSurfaceColor(theme);
            ForeColor = theme.TextPrimary;

            Padding = ContentPadding;

            Invalidate();
        }

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!ShowBorder)
                return;

            var theme = ThemeManager.Current;

            using var pen = new System.Drawing.Pen(theme.Border);
            e.Graphics.DrawRectangle(
                pen,
                0,
                0,
                Width - 1,
                Height - 1
            );
        }

        // =====================================================
        // SUPORTE
        // =====================================================

        private System.Drawing.Color ResolveSurfaceColor(GsTheme theme)
        {
            return Surface switch
            {
                GsPanelSurface.Secondary => theme.SurfaceAlt,
                GsPanelSurface.Header => theme.PrimaryDark,
                GsPanelSurface.Highlight => theme.PrimaryLight,
                _ => theme.Surface
            };
        }
    }
}
