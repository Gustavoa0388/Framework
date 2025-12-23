using System.Drawing;

namespace GS.Core.UI.Theming
{
    /// <summary>
    /// Tema base do GS Core UI.
    /// 
    /// REGRAS:
    /// - TODOS os controles devem consumir APENAS estes tokens.
    /// - É PROIBIDO usar cores hardcoded em controles.
    /// - Tokens são SEMÂNTICOS, não visuais.
    /// </summary>
    public class GsTheme
    {
        // =====================================================
        // IDENTIDADE PRINCIPAL
        // =====================================================

        /// <summary>
        /// Cor primária da aplicação.
        /// Usada em ações principais, seleções e elementos de destaque.
        /// </summary>
        public Color Primary { get; set; }

        /// <summary>
        /// Variação escura da Primary.
        /// Normalmente usada em headers, barras e áreas densas.
        /// </summary>
        public Color PrimaryDark { get; set; }

        /// <summary>
        /// Variação clara da Primary.
        /// Usada para hover, foco leve ou destaques sutis.
        /// </summary>
        public Color PrimaryLight { get; set; }

        /// <summary>
        /// Cor secundária da identidade visual.
        /// Deve ser usada com moderação (acentos).
        /// </summary>
        public Color Secondary { get; set; }

        // =====================================================
        // SUPERFÍCIES (LAYOUT)
        // =====================================================

        /// <summary>
        /// Fundo principal da aplicação (forms, telas).
        /// </summary>
        public Color Surface { get; set; }

        /// <summary>
        /// Fundo alternativo.
        /// Ex: linhas pares, painéis secundários.
        /// </summary>
        public Color SurfaceAlt { get; set; }

        /// <summary>
        /// Cor padrão de bordas e divisores.
        /// </summary>
        public Color Border { get; set; }

        // =====================================================
        // TEXTO
        // =====================================================

        /// <summary>
        /// Texto principal.
        /// </summary>
        public Color TextPrimary { get; set; }

        /// <summary>
        /// Texto secundário, auxiliar ou desabilitado.
        /// </summary>
        public Color TextSecondary { get; set; }

        /// <summary>
        /// Texto sobre fundos escuros ou Primary.
        /// </summary>
        public Color TextOnPrimary { get; set; }

        // =====================================================
        // TÍTULOS (HIERARQUIA TEXTUAL)
        // =====================================================

        /// <summary>
        /// Fonte para títulos principais de página.
        /// </summary>
        public Font TitleFont { get; set; }

        /// <summary>
        /// Fonte para subtítulos.
        /// </summary>
        public Font SubtitleFont { get; set; }

        /// <summary>
        /// Fonte para títulos de seção.
        /// </summary>
        public Font SectionFont { get; set; }

        /// <summary>
        /// Cor do texto de título principal.
        /// </summary>
        public Color TitleText { get; set; }

        /// <summary>
        /// Cor do texto de subtítulo.
        /// </summary>
        public Color SubtitleText { get; set; }

        /// <summary>
        /// Cor do texto de seção.
        /// </summary>
        public Color SectionText { get; set; }


        // =====================================================
        // INPUTS
        // =====================================================

        /// <summary>
        /// Fundo padrão de inputs.
        /// </summary>
        public Color InputBackground { get; set; }

        /// <summary>
        /// Borda padrão de inputs.
        /// </summary>
        public Color InputBorder { get; set; }

        /// <summary>
        /// Cor aplicada quando o input está em hover.
        /// </summary>
        public Color InputHover { get; set; }

        /// <summary>
        /// Cor aplicada quando o input está em foco.
        /// </summary>
        public Color InputFocus { get; set; }

        /// <summary>
        /// Cor de erro LOCAL aplicada a inputs
        /// (borda, underline, highlight).
        /// </summary>
        public Color InputError { get; set; }

        /// <summary>
        /// Cor de texto placeholder.
        /// </summary>
        public Color TextPlaceholder { get; set; }

        // =====================================================
        // TOGGLE SWITCH
        // =====================================================

        /// <summary>
        /// Cor de fundo do toggle desligado.
        /// </summary>
        public Color ToggleOffBackground { get; set; }

        /// <summary>
        /// Cor de fundo do toggle ligado.
        /// </summary>
        public Color ToggleOnBackground { get; set; }

        /// <summary>
        /// Cor do marcador (bolinha).
        /// </summary>
        public Color ToggleThumb { get; set; }

        /// <summary>
        /// Cor do texto do toggle.
        /// </summary>
        public Color ToggleText { get; set; }

        // =====================================================
        // RADIO BUTTON
        // =====================================================

        /// <summary>
        /// Cor da borda do radio.
        /// </summary>
        public Color RadioBorder { get; set; }

        /// <summary>
        /// Cor do preenchimento quando selecionado.
        /// </summary>
        public Color RadioFill { get; set; }

        /// <summary>
        /// Cor do texto do radio.
        /// </summary>
        public Color RadioText { get; set; }



        // =====================================================
        // GRID
        // =====================================================

        /// <summary>
        /// Fundo do header do grid.
        /// </summary>
        public Color GridHeaderBackground { get; set; }

        /// <summary>
        /// Texto do header do grid.
        /// </summary>
        public Color GridHeaderText { get; set; }

        /// <summary>
        /// Fundo da linha selecionada do grid.
        /// </summary>
        public Color GridSelection { get; set; }

        /// <summary>
        /// Texto da linha selecionada do grid.
        /// </summary>
        public Color GridSelectionText { get; set; }

        // =====================================================
        // ESTADOS (FEEDBACK VISUAL)
        // =====================================================

        /// <summary>
        /// Cor de erro GLOBAL (labels, mensagens, status).
        /// </summary>
        public Color Error { get; set; }

        /// <summary>
        /// Cor de aviso.
        /// </summary>
        public Color Warning { get; set; }

        /// <summary>
        /// Cor de sucesso.
        /// </summary>
        public Color Success { get; set; }

        /// <summary>
        /// Cor informativa/neutra.
        /// </summary>
        public Color Info { get; set; }

        // =====================================================
        // PROGRESS BAR
        // =====================================================

        /// <summary>
        /// Fundo da barra de progresso.
        /// </summary>
        public Color ProgressBackground { get; set; }

        /// <summary>
        /// Cor principal de preenchimento da barra de progresso.
        /// </summary>
        public Color ProgressFill { get; set; }

        /// <summary>
        /// Cor da borda da barra de progresso.
        /// </summary>
        public Color ProgressBorder { get; set; }

        /// <summary>
        /// Cor usada quando o progresso indica sucesso.
        /// </summary>
        public Color ProgressSuccess { get; set; }

        /// <summary>
        /// Cor usada quando o progresso indica erro.
        /// </summary>
        public Color ProgressError { get; set; }

        // =====================================================
        // OUTROS
        // =====================================================

        /// <summary>
        /// Fonte padrão da aplicação.
        /// </summary>
        public Font DefaultFont { get; set; }

        /// <summary>
        /// Indica se o tema é Dark.
        /// </summary>
        public bool IsDark { get; set; }
    }
}
