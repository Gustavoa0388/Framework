using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    public enum GsTitleSize
    {
        Title,
        Subtitle,
        Section
    }

    public class GsTitleLabel : Label, IThemedControl
    {
        public GsTitleLabel()
        {
            AutoSize = true;
            TextAlign = ContentAlignment.MiddleLeft;
            BackColor = Color.Transparent;
            UseMnemonic = false;

            TitleSize = GsTitleSize.Title;
        }

        private GsTitleSize _titleSize;
        [Category("GS")]
        public GsTitleSize TitleSize
        {
            get => _titleSize;
            set
            {
                _titleSize = value;
                ApplyFont();
                Invalidate();
            }
        }

        private void ApplyFont()
        {
            float size = _titleSize switch
            {
                GsTitleSize.Title => 18f,
                GsTitleSize.Subtitle => 14f,
                GsTitleSize.Section => 12f,
                _ => 14f
            };

            Font = new Font(
                Font.FontFamily,
                size,
                FontStyle.Bold,
                GraphicsUnit.Point
            );
        }

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = Color.Transparent;

            ForeColor = theme.IsDark
                ? theme.TextPrimary     // branco / cinza claro
                : theme.Primary;        // azul no light

            Invalidate();
        }
    }
}
