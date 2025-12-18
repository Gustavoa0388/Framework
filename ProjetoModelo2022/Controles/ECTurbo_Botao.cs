using System.ComponentModel;
using System.Drawing.Drawing2D;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Botao : Button
    {
        public ECTurbo_Botao()
        {
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        private int vTamBorda = 1;
        [DisplayName("_Largura da Borda")]
        public int TamBorda
        {
            get { return vTamBorda; }
            set
            {

                if (value < 0) value = 0;
                if (value > 3) value = 3;

                vTamBorda = value; Invalidate();
            }
        }

        private int vDistIcone = 5;
        [DisplayName("_Distancia do Icone")]
        public int DistIcone
        {
            get { return vDistIcone; }
            set
            {

                if (value < 0) value = 0;
                vDistIcone = value; Invalidate();
            }
        }

        private int vArred = 20;
        [DisplayName("_Arredondamento")]
        public int Arred
        {
            get { return vArred; }
            set { vArred = value; Invalidate(); }
        }

        private Color vCorBorda = Color.Gray;
        [DisplayName("_Cor da Borda")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vCor1 = Color.Purple;
        [DisplayName("_Cor de Fundo 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private int vAngulo = 1;
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


        private Color vCor2 = Color.Pink;
        [DisplayName("_Cor de Fundo 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        public enum Posicoes
        {
            Esquerda,
            Direita,
            Inferior,
            Superior
        }

        private Posicoes vPosicaoImagem = Posicoes.Esquerda;
        [DisplayName("_Posição da Imagem")]
        public Posicoes PosicaoImagem
        {
            get { return vPosicaoImagem; }
            set { vPosicaoImagem = value; Invalidate(); }
        }

        private int vTamanhoIcone = 16;
        [DisplayName("_Tamanho Icone")]
        public int TamanhoIcone
        {
            get { return vTamanhoIcone; }
            set { vTamanhoIcone = value; Invalidate(); }
        }



        private bool vAtivarSombra = false;
        [DisplayName("_Sombra Inferior - Ativar")]
        public bool AtivarSombra
        {
            get { return vAtivarSombra; }
            set { vAtivarSombra = value; Invalidate(); }
        }

        private Color vCorSombra = Color.Black;
        [DisplayName("_Sombra Inferior - Cor")]
        public Color CorSombra
        {
            get { return vCorSombra; }
            set { vCorSombra = value; Invalidate(); }
        }


        private int vTamanhoSombra = 3;
        [DisplayName("_Sombra Inferior - Cor")]
        public int TamanhoSombra
        {
            get { return vTamanhoSombra; }
            set
            {

                if (value < 1) value = 1;
                if (value > 5) value = 5;

                vTamanhoSombra = value;
                Invalidate();

            }
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            RectangleF Base = ClientRectangle;
            if (TamBorda > 1)
                Base.Inflate(-(TamBorda / 2), -(TamBorda / 2));

            Base.Width--;
            Base.Height--;

            if (AtivarSombra == true)
            {
                using (GraphicsPath path = Funcoes.CriarPath(Base, Arred))
                using (SolidBrush Pincel = new SolidBrush(CorSombra))
                {
                    g.FillPath(Pincel, path);
                }

                Base.Height = Base.Height - TamanhoSombra;
            }


            using (GraphicsPath path = Funcoes.CriarPath(Base, Arred))
            using (LinearGradientBrush PincelFundo =
            new LinearGradientBrush(ClientRectangle, Cor1, Cor2, Angulo))
            using (Pen Caneta = new Pen(CorBorda, TamBorda))
            using (SolidBrush PincelTexto = new SolidBrush(ForeColor))
            {

                g.FillPath(PincelFundo, path);

                if (TamBorda > 0)
                    g.DrawPath(Caneta, path);

                SizeF TamanhoTexto = g.MeasureString(Text, Font);
                SizeF TamanhoImagem = new SizeF(0, 0);

                if (Image != null)
                {
                    TamanhoImagem = new SizeF(TamanhoIcone, TamanhoIcone); // Supondo que a imagem tenha tamanho 32x32
                }

                // Calculando a posição do texto e da imagem
                float tX = 0, tY = 0, iX = 0, iY = 0;

                switch (PosicaoImagem)
                {
                    case Posicoes.Esquerda:
                        tX = Base.X + (Base.Width - TamanhoTexto.Width - TamanhoImagem.Width) / 2 + TamanhoImagem.Width;
                        tY = Base.Y + (Base.Height - TamanhoTexto.Height) / 2;
                        iX = tX - TamanhoImagem.Width;
                        iY = Base.Y + (Base.Height - TamanhoImagem.Height) / 2;
                        break;
                    case Posicoes.Direita:
                        tX = Base.X + (Base.Width - TamanhoTexto.Width - TamanhoImagem.Width) / 2;
                        tY = Base.Y + (Base.Height - TamanhoTexto.Height) / 2;
                        iX = tX + TamanhoTexto.Width;
                        iY = Base.Y + (Base.Height - TamanhoImagem.Height) / 2;
                        break;
                    case Posicoes.Inferior:
                        tX = Base.X + (Base.Width - TamanhoTexto.Width) / 2;
                        tY = Base.Y + (Base.Height - TamanhoTexto.Height - TamanhoImagem.Height) / 2;
                        iX = Base.X + (Base.Width - TamanhoImagem.Width) / 2;
                        iY = tY + TamanhoTexto.Height;
                        break;
                    case Posicoes.Superior:
                        tX = Base.X + (Base.Width - TamanhoTexto.Width) / 2;
                        tY = Base.Y + (Base.Height - TamanhoTexto.Height - TamanhoImagem.Height) / 2 + TamanhoImagem.Height;
                        iX = Base.X + (Base.Width - TamanhoImagem.Width) / 2;
                        iY = tY - TamanhoImagem.Height;
                        break;
                }

                if (TextAlign == ContentAlignment.MiddleLeft)
                {
                    int d = Arred > 10 ? 10 : Arred / 2;
                    iX = Base.Left + d;
                    iY = Base.Top + (Base.Height - TamanhoImagem.Height) / 2;

                    tX = Base.X + TamanhoImagem.Width + d + 5;
                    tY = Base.Y + (Base.Height - TamanhoTexto.Height) / 2;
                }

                int di = 0;
                if (Image != null)
                    di = DistIcone;

                g.DrawString(Text, Font, PincelTexto, tX + di, tY);

                if (Image != null)
                {
                    g.DrawImage(Image, iX + di, iY, TamanhoIcone, TamanhoIcone);
                }
            }

        }

        private Color Cor1Original = default;
        private Color Cor2Original = default;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (Cor1Original == default)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1, 40);
            Cor2 = Funcoes.CorTransparente(Cor2, 40);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Cor1 = Cor1Original;
            Cor2 = Cor2Original;
        }



        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Cor1Original == default)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1, 20);
            Cor2 = Funcoes.CorTransparente(Cor2, 20);
        }


        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            Cor1 = Cor1Original;
            Cor2 = Cor2Original;

        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (Cor1Original == default)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1, 90);
            Cor2 = Funcoes.CorTransparente(Cor2, 90);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            OnMouseLeave(e);
        }
    }
}
