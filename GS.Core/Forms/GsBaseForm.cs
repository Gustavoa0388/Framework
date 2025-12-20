using System;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    public partial class GsBaseForm : Form
    {
        /// <summary>
        /// Define se o tema será aplicado automaticamente ao carregar o Form.
        /// </summary>
        protected bool AutoApplyTheme { get; set; } = true;

        /// <summary>
        /// Permite sobrescrever o tema padrão para este Form.
        /// Se null, usa o tema atual do ThemeManager.
        /// </summary>
        protected GsTheme CustomTheme { get; set; }

        public GsBaseForm()
        {
            InitializeComponent();

            // Boas práticas padrão
            StartPosition = FormStartPosition.CenterScreen;
            Font = SystemFonts.DefaultFont;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApplyThemeIfNeeded();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Gancho futuro (animações, logging, métricas)
        }

        protected virtual void ApplyThemeIfNeeded()
        {
            if (!AutoApplyTheme)
                return;

            var theme = CustomTheme ?? ThemeManager.Current;
            if (theme == null)
                return;

            ThemeManager.ApplyTheme(this, theme);
        }

        
    }
}
