using GS.Core.UI.Controls.States;
using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsTitleLabel
    /// 
    /// Label semântico para hierarquia textual do GS Core UI.
    /// 
    /// REPRESENTA:
    /// - Título de página
    /// - Subtítulo
    /// - Título de seção
    /// 
    /// NÃO FAZ:
    /// - Decidir cores por tema (dark/light)
    /// - Criar fontes manualmente
    /// - Usar tamanhos hardcoded
    /// </summary>
    public class GsTitleLabel : Label, IThemedControl
    {
        public GsTitleLabel()
        {
            AutoSize = true;
            TextAlign = ContentAlignment.MiddleLeft;
            BackColor = Color.Transparent;
            UseMnemonic = false;

            TitleLevel = GsTitleLevel.Title;
        }

        // =====================================================
        // PROPRIEDADES
        // =====================================================

        private GsTitleLevel _titleLevel;

        /// <summary>
        /// Define o nível hierárquico do título.
        /// </summary>
        [Category("GS Core")]
        [DefaultValue(GsTitleLevel.Title)]
        public GsTitleLevel TitleLevel
        {
            get => _titleLevel;
            set
            {
                _titleLevel = value;
                ApplyTheme(ThemeManager.Current);
            }
        }

        // =====================================================
        // THEME
        // =====================================================

        /// <summary>
        /// Aplica o tema visual ao título.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            BackColor = Color.Transparent;

            switch (TitleLevel)
            {
                case GsTitleLevel.Title:
                    Font = theme.TitleFont;
                    ForeColor = theme.TitleText;
                    break;

                case GsTitleLevel.Subtitle:
                    Font = theme.SubtitleFont;
                    ForeColor = theme.SubtitleText;
                    break;

                case GsTitleLevel.Section:
                    Font = theme.SectionFont;
                    ForeColor = theme.SectionText;
                    break;
            }

            Invalidate();
        }
    }
}
