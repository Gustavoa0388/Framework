using System.Drawing;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Define todos os tokens visuais do GS Core.
    /// NÃO contém lógica. Apenas valores.
    /// </summary>
    public class GsTheme
    {
        // =============================
        // BASE DO APP
        // =============================
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Font DefaultFont { get; set; }

        // =============================
        // BRANDING
        // =============================
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Color Border { get; set; }
        public Color Error { get; set; }

        // =============================
        // TEXTOS
        // =============================
        public Color TextPrimary { get; set; }
        public Color TextSecondary { get; set; }
        public Color TextPlaceholder { get; set; }

        // =============================
        // INPUTS
        // =============================
        public Color InputBackground { get; set; }
        public Color InputBorder { get; set; }
        public Color InputHover { get; set; }
        public Color InputFocus { get; set; }
        public Color InputError { get; set; }

        // =============================
        // GRID / TABELAS ⭐ NOVO
        // =============================
        public Color GridBackground { get; set; }        // fundo geral
        public Color GridSurface { get; set; }           // linha normal
        public Color GridSurfaceAlt { get; set; }        // linha alternada
        public Color GridHeaderBackground { get; set; }  // cabeçalho
        public Color GridHeaderText { get; set; }
        public Color GridRowSelected { get; set; }       // seleção
        public Color GridRowSelectedText { get; set; }
        public Color GridBorder { get; set; }

        // =============================
        // FLAGS
        // =============================
        public bool IsDark { get; set; }
    }
}
