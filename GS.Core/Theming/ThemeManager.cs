using System.Windows.Forms;

namespace GS.Core.UI.Theming
{
    public static class ThemeManager
    {
        public static GsTheme Current { get; private set; }

        public static void ApplyTheme(Control root, GsTheme theme)
        {
            Current = theme;
            ApplyRecursive(root);
        }

        private static void ApplyRecursive(Control ctrl)
        {
            if (ctrl is IThemedControl themed)
            {
                themed.ApplyTheme(Current);
            }
            else
            {
                ctrl.BackColor = Current.BackColor;
                ctrl.ForeColor = Current.ForeColor;
                ctrl.Font = Current.DefaultFont;
            }

            foreach (Control child in ctrl.Controls)
                ApplyRecursive(child);
        }
    }
}
