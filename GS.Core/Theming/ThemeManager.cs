using System.Windows.Forms;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Gerenciador central de aplicação de temas.
    /// </summary>
    public static class ThemeManager
    {
        /// <summary>
        /// Tema atualmente ativo.
        /// </summary>
        public static GsTheme Current { get; private set; } = GsThemes.Light;

        /// <summary>
        /// Aplica o tema informado ao controle raiz e seus filhos.
        /// </summary>
        public static void ApplyTheme(Control root, GsTheme theme)
        {
            Current = theme;
            ApplyRecursive(root);
        }

        /// <summary>
        /// Aplicação recursiva do tema.
        /// </summary>
        private static void ApplyRecursive(Control ctrl)
        {
            // Controles que conhecem o theme aplicam sua própria lógica
            if (ctrl is IThemedControl themed)
            {
                themed.ApplyTheme(Current);
            }
            // Fallback APENAS para containers visuais
            else if (ctrl is Form || ctrl is Panel || ctrl is UserControl)
            {
                ctrl.BackColor = Current.Surface;
                ctrl.ForeColor = Current.TextPrimary;

                // Evita sobrescrever fontes customizadas
                if (ctrl.Font == Control.DefaultFont)
                    ctrl.Font = Current.DefaultFont;
            }

            // Aplica nos filhos
            foreach (Control child in ctrl.Controls)
            {
                ApplyRecursive(child);
            }
        }
    }
}
