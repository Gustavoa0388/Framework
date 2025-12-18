using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ECTurbo.Controles
{
    public class ECTurbo_Label : Label
    {
        public ECTurbo_Label()
        {
            DoubleBuffered = true;
            TextAlign = ContentAlignment.MiddleLeft;
        }

        private bool vQuebraTexto = false;
        [DisplayName("_Quebra de Texto")]
        public bool QuebraTexto
        {
            get { return vQuebraTexto; }
            set { vQuebraTexto = value; Invalidate(); }
        }

        private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; Invalidate(); }
        }

        private Color vCor1 = Color.SteelBlue;
        [DisplayName("_Cor 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.MidnightBlue;
        [DisplayName("_Cor 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        private int vAngulo = 90;
        [DisplayName("_Angulo do Gradiente")]
        public int Angulo
        {
            get { return vAngulo; }
            set
            {
                if (value < 1) value = 1;
                vAngulo = value;
                Invalidate();
            }
        }

        private bool vAtivarSombra = false;
        [DisplayName("_Ativar Sombra")]
        public bool AtivarSombra
        {
            get { return vAtivarSombra; }
            set { vAtivarSombra = value; Invalidate(); }
        }

        private int vX = 2;
        [DisplayName("_Distancia Sombra Eixo X")]
        public int X
        {
            get { return vX; }
            set { vX = value; Invalidate(); }
        }

        private int vY = 2;
        [DisplayName("_Distancia Sombra Eixo Y")]
        public int Y
        {
            get { return vY; }
            set { vY = value; Invalidate(); }
        }

        private Color vCorSombra = Color.Black;
        [DisplayName("_Cor da Sombra")]
        public Color CorSombra
        {
            get { return vCorSombra; }
            set { vCorSombra = value; Invalidate(); }
        }

        private PointF GetTextAndImagePosition(Graphics g, int imageWidth, int totalWidth, int totalHeight, bool quebraTexto, string text, Font font, float textX, float textY)
        {
            float x = 0;
            float y = 0;

            if (quebraTexto)
            {
                // Calcula a altura do texto quebrado
                SizeF textSize = g.MeasureString(text, font, Width - (int)textX);
                totalHeight = (int)textSize.Height;
            }

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    x = 0;
                    y = 0;
                    break;
                case ContentAlignment.TopCenter:
                    x = (Width - totalWidth) / 2;
                    y = 0;
                    break;
                case ContentAlignment.TopRight:
                    x = Width - totalWidth;
                    y = 0;
                    break;
                case ContentAlignment.MiddleLeft:
                    x = 0;
                    y = (Height - totalHeight) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    x = (Width - totalWidth) / 2;
                    y = (Height - totalHeight) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    x = Width - totalWidth;
                    y = (Height - totalHeight) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    x = 0;
                    y = Height - totalHeight;
                    break;
                case ContentAlignment.BottomCenter:
                    x = (Width - totalWidth) / 2;
                    y = Height - totalHeight;
                    break;
                case ContentAlignment.BottomRight:
                    x = Width - totalWidth;
                    y = Height - totalHeight;
                    break;
            }

            return new PointF(x, y);
        }

        private int vEspacoTexto = 5;
        [DisplayName("_Espaçamento Imagem x Texto")]
        public int EspacoTexto
        {
            get { return vEspacoTexto; }
            set { vEspacoTexto = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            SizeF textSize = g.MeasureString(Text, Font);
            int imageWidth = 0, imageHeight = 0;
            int spacing = 0;

            if (Image != null)
            {
                imageHeight = (int)textSize.Height;
                imageWidth = (int)((float)Image.Width / Image.Height * imageHeight);
                spacing = EspacoTexto;
            }

            int totalWidth = imageWidth + spacing + (int)textSize.Width;
            int totalHeight = Math.Max(imageHeight, (int)textSize.Height);

            float textX = imageWidth + spacing;
            float textY = 0;

            // Calcula a posição do texto e da imagem
            PointF position = GetTextAndImagePosition(g, imageWidth, totalWidth, totalHeight, QuebraTexto, Text, Font, textX, textY);

            // Desenha a Image, se existir
            if (Image != null)
            {
                g.DrawImage(Image, new Rectangle((int)position.X, (int)position.Y + (totalHeight - imageHeight) / 2, imageWidth, imageHeight));
            }

            // Ajusta a posição do texto
            textX = position.X + imageWidth + spacing;
            textY = position.Y + (totalHeight - textSize.Height) / 2;

            using (LinearGradientBrush Pincel = new LinearGradientBrush(ClientRectangle, Cor1, Cor2, Angulo))
            {
                if (AtivarSombra)
                {
                    if (QuebraTexto)
                    {
                        RectangleF shadowRect = new RectangleF(textX + X, textY + Y, Width - textX, Height - textY);
                        g.DrawString(Text, Font, new SolidBrush(CorSombra), shadowRect);
                    }
                    else
                    {
                        g.DrawString(Text, Font, new SolidBrush(CorSombra), textX + X, textY + Y);
                    }
                }

                // Verifica se a propriedade AutoEllipse está ativada
                if (AutoEllipsis && totalWidth > Width)
                {
                    string ellipsedText = Text;
                    while (g.MeasureString(ellipsedText + "...", Font).Width > (Width - textX))
                    {
                        ellipsedText = ellipsedText.Substring(0, ellipsedText.Length - 1);
                    }
                    ellipsedText += "...";
                    g.DrawString(ellipsedText, Font, Pincel, new PointF(textX, textY));
                }
                else if (QuebraTexto)
                {
                    // Ajusta o alinhamento horizontal para o texto quebrado
                    RectangleF textRect = new RectangleF(0, textY, Width, Height - textY);
                    switch (TextAlign)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.BottomLeft:
                            textRect.X = textX;
                            break;
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.BottomCenter:
                            textRect.X = (Width - g.MeasureString(Text, Font, (int)textRect.Width).Width) / 2;
                            break;
                        case ContentAlignment.TopRight:
                        case ContentAlignment.MiddleRight:
                        case ContentAlignment.BottomRight:
                            textRect.X = Width - g.MeasureString(Text, Font, (int)textRect.Width).Width;
                            break;
                    }
                    g.DrawString(Text, Font, Pincel, textRect);
                }
                else
                {
                    g.DrawString(Text, Font, Pincel, new PointF(textX, textY));
                }
            }
        }
    }
}
