using System.Drawing;

namespace GS.Core.UI.Theming
{
    public class GsTheme
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public Color Primary { get; set; }
        public Color Secondary { get; set; }

        public Color InputBack { get; set; }
        public Color Border { get; set; }

        public Font DefaultFont { get; set; }
        public Color InputBackground { get; set; }
        public Color InputHover { get; set; }
        public Color InputFocus { get; set; }
        public Color TextPrimary { get; set; }
        public Color InputError { get; set; }
        public Color TextPlaceholder { get; set; }
        public Color InputBorder { get; set; }   
        public Color TextSecondary { get; set; }    // texto claro p/ dark
        public bool IsDark { get; set; }






    }
}
