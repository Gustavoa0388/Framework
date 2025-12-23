using System.Windows.Forms;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// ThemeManager
    ///
    /// Gerenciador central de aplicação de temas no GS Core UI.
    ///
    /// RESPONSABILIDADES:
    /// - Manter o tema atualmente ativo
    /// - Aplicar o tema a um controle raiz e todos os seus filhos
    /// - Respeitar controles que implementam IThemedControl
    ///
    /// O ThemeManager:
    /// - NÃO define cores
    /// - NÃO conhece layout
    /// - NÃO cria controles
    ///
    /// Ele apenas ORQUESTRA a aplicação do tema.
    /// </summary>
    public static class ThemeManager
    {
        /// <summary>
        /// Tema atualmente ativo no sistema.
        ///
        /// OBS:
        /// - Sempre aponta para um GsTheme válido
        /// - Pode ser trocado em runtime
        /// </summary>
        public static GsTheme Current { get; private set; } = GsThemes.Light;

        /// <summary>
        /// Aplica o tema informado a um controle raiz
        /// e toda a sua hierarquia visual.
        ///
        /// USO TÍPICO:
        /// ThemeManager.ApplyTheme(this, GsThemes.Light);
        /// </summary>
        public static void ApplyTheme(Control root, GsTheme theme)
        {
            Current = theme;
            ApplyRecursive(root);
        }

        /// <summary>
        /// Aplica o tema de forma recursiva.
        ///
        /// ORDEM DE APLICAÇÃO:
        /// 1) Se o controle implementa IThemedControl,
        ///    ele é responsável por aplicar seu próprio tema.
        /// 2) Caso contrário, aplica-se um fallback APENAS
        ///    para containers visuais básicos.
        /// </summary>
        private static void ApplyRecursive(Control ctrl)
        {
            // Controles conscientes de tema
            if (ctrl is IThemedControl themed)
            {
                themed.ApplyTheme(Current);
            }
            // Fallback apenas para containers genéricos
            else if (ctrl is Form || ctrl is Panel || ctrl is UserControl)
            {
                ctrl.BackColor = Current.Surface;
                ctrl.ForeColor = Current.TextPrimary;

                // Evita sobrescrever fontes customizadas
                if (ctrl.Font == Control.DefaultFont)
                    ctrl.Font = Current.DefaultFont;
            }

            // Aplica recursivamente nos filhos
            foreach (Control child in ctrl.Controls)
            {
                ApplyRecursive(child);
            }
        }
    }
}
