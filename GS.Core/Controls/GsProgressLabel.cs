using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using GS.Core.UI.Utils.Legacy;



namespace GS.Core.UI.Controls
{
    public class GsProgressLabel : Label
    {
        public GsProgressLabel()
        {
            DoubleBuffered = true;
        }

        private bool vImagemDireita = false;
        [DisplayName("_Alinhar Imagem à Direita")]
        public bool ImagemDireita
        {
            get { return vImagemDireita; }
            set { vImagemDireita = value; Invalidate(); }
        }

        private bool vTextoDireita = false;
        [DisplayName("_Alinhar Texto à Direita")]
        public bool TextoDireita
        {
            get { return vTextoDireita; }
            set { vTextoDireita = value; Invalidate(); }
        }

        private float vPercentual = 33;
        [DisplayName("_Valor Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                vPercentual = value; Invalidate(); 
            }
        }

        private int vEspaco = 1;
        [DisplayName("_Espaçamento Interno")]
        public int Espaco
        {
            get { return vEspaco; }
            set {
                if (value < 0) value = 0;
                vEspaco = value;
                Invalidate();
            }
        }

        private int vNivel1 = 25;
        [DisplayName("_Nivel 1 até ?")]
        public int Nivel1
        {
            get { return vNivel1; }
            set
            {
                if (value < 1) value = 1;
                vNivel1 = value;
                Invalidate();
            }
        }


        private int vNivel2 = 50;
        [DisplayName("_Nivel 2 até ?")]
        public int Nivel2
        {
            get { return vNivel2; }
            set
            {
                if (value < Nivel1 + 1) value = Nivel1 + 1;
                vNivel2 = value;
                Invalidate();
            }
        }


        private int vNivel3 = 75;
        [DisplayName("_Nivel 3 até ?")]
        public int Nivel3
        {
            get { return vNivel3; }
            set
            {
                if (value < Nivel2 + 1) value = Nivel2 + 1;
                vNivel3 = value;
                Invalidate();
            }
        }

        private Color vCor1 = Color.PaleGreen;
        [DisplayName("_Nivel 1 Cor")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.Green;
        [DisplayName("_Nivel 2 Cor")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }


        private Color vCor3 = Color.Orange;
        [DisplayName("_Nivel 3 Cor")]
        public Color Cor3
        {
            get { return vCor3; }
            set { vCor3 = value; Invalidate(); }
        }

        private Color vCor4 = Color.Red;
        [DisplayName("_Nivel 4 Cor Acima do Nivel 3")]
        public Color Cor4
        {
            get { return vCor4; }
            set { vCor4 = value; Invalidate(); }
        }


        private int vTamanhoIcone = 16;
        [DisplayName("_Tamanho do Icone")]
        public int TamanhoIcone
        {
            get { return vTamanhoIcone; }
            set {
                if (value < 10) value = 10;
                vTamanhoIcone = value; 
                Invalidate(); 
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Barra = ClientRectangle;
            Barra.Inflate(0, -Espaco);

            if (Barra.Height < 4)
                Barra.Height = 4;

            using (SolidBrush Pincel = new SolidBrush(Color.Salmon))
            {
                SizeF tTexto = g.MeasureString(Text, Font);
                float tImage = TamanhoIcone;

                float textoY = (Height - tTexto.Height) / 2;
                float textoX = 4;

                float imageY = (Height - tImage) / 2;
                float imageX = 0;

                if (TextoDireita == true)
                    textoX = Width - tTexto.Width - 4;

                if (Image != null)
                {
                    Barra.Width = Barra.Width - tImage - 8;

                    if (ImagemDireita == false)
                        Barra.X = tImage + 6;

                    if (ImagemDireita == false && TextoDireita == false)
                    {
                        imageX = 2;
                        textoX = imageX + tImage + 2;
                    }

                    if (ImagemDireita == true && TextoDireita == true)
                    {
                        imageX = Width - tImage - 4;
                        textoX = Width - tTexto.Width - tImage - 6;
                    }

                    if (ImagemDireita == true && TextoDireita == false)
                    {
                        imageX = Width - tImage - 4;
                    }

                    g.DrawImage(Image, imageX, imageY, tImage, tImage);
                    
                }

                if (Percentual > 0)
                    Barra.Width = Barra.Width * (Percentual / 100);

                if (TextoDireita == true)
                    Barra.X = textoX + tTexto.Width - Barra.Width;

                if (Percentual > 0)
                {

                    Color Cor = Cor1;

                    if (Percentual > Nivel1)
                        Cor = Cor2;

                    if (Percentual > Nivel2)
                        Cor = Cor3;

                    if (Percentual > Nivel3)
                        Cor = Cor4;


                    Pincel.Color = Cor;

                    using (GraphicsPath path = FuncoesLegacy.CriarPath(Barra, 6))
                    {
                        g.FillPath(Pincel, path);
                    }

                }

                Pincel.Color = ForeColor;

                g.DrawString(Text, Font, Pincel, textoX, textoY);
                
            }
        }
    }
}
