using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GS.Core.UI.Controls
{
    public class GsLabel : Label, IThemedControl
    {
        public GsLabel()
        {
            DoubleBuffered = true;
            AutoSize = true;
            TextAlign = ContentAlignment.MiddleLeft;
            BackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleRight;

        }

        // ===============================
        // PROPRIEDADES CUSTOM
        // ===============================

        [DisplayName("_Quebra de Texto")]
        public bool QuebraTexto { get; set; } = false;

        [DisplayName("_Usar Gradiente no Texto")]
        public bool UsarGradienteTexto { get; set; } = true;

        [DisplayName("_Cor Gradiente 1")]
        public Color Cor1 { get; set; } = Color.SteelBlue;

        [DisplayName("_Cor Gradiente 2")]
        public Color Cor2 { get; set; } = Color.MidnightBlue;

        [DisplayName("_Ângulo do Gradiente")]
        public int Angulo { get; set; } = 90;

        [DisplayName("_Ativar Sombra")]
        public bool AtivarSombra { get; set; } = false;

        [DisplayName("_Sombra X")]
        public int SombraX { get; set; } = 1;

        [DisplayName("_Sombra Y")]
        public int SombraY { get; set; } = 1;

        [DisplayName("_Cor da Sombra")]
        public Color CorSombra { get; set; } = Color.Black;

        [DisplayName("_Espaço Imagem x Texto")]
        public int EspacoTexto { get; set; } = 5;

        [DisplayName("_Require")]
        public Control TargetControl { get; set; }


        // ===============================
        // PINTURA
        // ===============================

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // fundo
            using (var bg = new SolidBrush(BackColor))
                g.FillRectangle(bg, ClientRectangle);

            if (string.IsNullOrEmpty(Text))
                return;

            SizeF textSize = g.MeasureString(Text, Font);
            float textX = 0;
            float textY = (Height - textSize.Height) / 2;

            bool isRequired = false;

            if (TargetControl is IGsRequiredAware req)
                isRequired = req.Required;
                      
            // ===============================
            // SOMBRA (APENAS SE GRADIENTE)
            // ===============================
            if (AtivarSombra && UsarGradienteTexto)
            {
                using var shadowBrush = new SolidBrush(Color.FromArgb(60, CorSombra));
                g.DrawString(Text, Font, shadowBrush, textX + SombraX, textY + SombraY);
            }

            // ===============================
            // ESCOLHA DO PINCEL
            // ===============================
            Brush textBrush;

            if (UsarGradienteTexto)
            {
                textBrush = new LinearGradientBrush(
                    ClientRectangle,
                    Cor1,
                    Cor2,
                    Angulo
                );
            }
            else
            {
                textBrush = new SolidBrush(ForeColor);
            }

            // ===============================
            // DESENHO DO TEXTO
            // ===============================
            if (AutoEllipsis && textSize.Width > Width)
            {
                string ellipsed = Text;
                while (ellipsed.Length > 0 &&
                       g.MeasureString(ellipsed + "...", Font).Width > Width)
                {
                    ellipsed = ellipsed[..^1];
                }

                g.DrawString(ellipsed + "...", Font, textBrush, textX, textY);
            }
            else if (QuebraTexto)
            {
                RectangleF rect = new RectangleF(0, 0, Width, Height);
                g.DrawString(Text, Font, textBrush, rect);
            }
            else
            {
                g.DrawString(Text, Font, textBrush, textX, textY);
            }

            if (isRequired)
            {
                SizeF baseSize = g.MeasureString(Text, Font);

                using var starBrush = new SolidBrush(
                ThemeManager.Current.Error
                );
                using var starFont = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);

                g.DrawString(
                    "*",
                    starFont,
                    starBrush,
                    textX + baseSize.Width + 2,
                    textY
                );
            }

        }

        // ===============================
        // THEME
        // ===============================

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = theme.IsDark
                ? theme.BackColor
                : Color.White;

            ForeColor = theme.IsDark
                ? theme.TextSecondary   // branco / cinza claro
                : theme.Primary;        // azul no light

            Font = theme.DefaultFont;

            // REGRA-CHAVE
            UsarGradienteTexto = !theme.IsDark;

            Invalidate();
        }

        // ===============================
        // COMPATIBILIDADE COM VERSÕES ANTIGAS
        // (Designer.cs legado)
        // ===============================

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int X
        {
            get => SombraX;
            set => SombraX = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Y
        {
            get => SombraY;
            set => SombraY = value;
        }

    }
}
