using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ECTurbo.Codigos;
using System.ComponentModel;

namespace GS.Core.UI.Controls
{
    public class GsImageBox : PictureBox
    {
        public GsImageBox()
        {
            DoubleBuffered = true;
            Size = new Size(50, 50);

            Tag = "";
        }

        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set
            {

                vColuna = value;

                string valor = Funcoes.PegarTag(this, "col");

                Tag = Tag.ToString().Replace("col=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag = "col=" + value + Tag.ToString();

            }
        }


        private string vPadrao;
        [DisplayName("(DB.2 Foto Padrão)")]
        public string Padrao
        {
            get { return vPadrao; }
            set {   

                vPadrao = value;

                string valor = Funcoes.PegarTag(this, "padrao");

                Tag = Tag.ToString().Replace("|padrao=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|padrao=" + value;
            }
        }



        private int vRaio1 = 10;
        [DisplayName("_Canto Superior Esquerdo")]
        public int Raio1
        {
            get { return vRaio1; }
            set { vRaio1 = value; Invalidate(); }
        }

        private int vRaio2 = 10;
        [DisplayName("_Canto Superior Direito")]
        public int Raio2
        {
            get { return vRaio2; }
            set { vRaio2 = value; Invalidate(); }
        }


        private int vRaio3 = 10;
        [DisplayName("_Canto Inferior Direito")]
        public int Raio3
        {
            get { return vRaio3; }
            set { vRaio3 = value; Invalidate(); }
        }

        private int vRaio4 = 10;
        [DisplayName("_Canto Inferior Esquerdo")]
        public int Raio4
        {
            get { return vRaio4; }
            set { vRaio4 = value; Invalidate(); }
        }

        private int vTamanhoBorda = 1;
        [DisplayName("_Tamanho da Borda")]
        public int TamanhoBorda
        {
            get { return vTamanhoBorda; }
            set { vTamanhoBorda = value; Invalidate(); }
        }

        private Color vCor1 = Color.Red;
        [DisplayName("_Borda Cor 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.Blue;
        [DisplayName("_Borda Cor 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            RectangleF Base = ClientRectangle;

            int t = TamanhoBorda / 2 == 0 ? 1 : TamanhoBorda / 2;

            Base.Inflate(-t, -t);
            Base.Width--;
            Base.Height--;

            if (Base.Width < 1) Base.Width = 1;
            if (Base.Height < 1) Base.Height = 1;

            using (GraphicsPath path = Funcoes.CriarPath(Base, 1, Raio1, Raio2, Raio3, Raio4))
            using (LinearGradientBrush Pincel = 
                new LinearGradientBrush(ClientRectangle, Cor1, Cor2, 135))
            using (Pen Caneta = new Pen(Pincel, TamanhoBorda))
            {
                if (Image != null)
                {
                    g.SetClip(path);
                    g.DrawImage(Image, Base.X, Base.Y, Base.Width, Base.Height);
                    g.ResetClip();
                }

                g.DrawPath(Caneta, path);
            }

        }

    }
}
