using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GS.Core.UI.Controls.Data
{
    public class GsChart2 : Control
    {

        private System.Windows.Forms.Timer timer;
        private float targetPercentual;
        private float step = 1;

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementa ou decrementa o valor de Percentual gradualmente até atingir o targetPercentual
            if (vPercentual < targetPercentual)
            {
                vPercentual += step;
                if (vPercentual > targetPercentual)
                    vPercentual = targetPercentual;
            }
            else if (vPercentual > targetPercentual)
            {
                vPercentual -= step;
                if (vPercentual < targetPercentual)
                    vPercentual = targetPercentual;
            }

            Invalidate(); // Redesenha o controle

            // Para o timer quando o valor desejado for atingido
            if (vPercentual == targetPercentual)
                timer.Stop();
        }
        public GsChart2()
        {
            DoubleBuffered = true;
            Size = new Size(100, 100);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
        }

        private int vLarguraFundo = 10;
        [DisplayName("_Largura da Barra de Fundo")]
        public int LarguraFundo
        {
            get { return vLarguraFundo; }
            set
            {

                if (value < 4) value = 4;
                if (value > Height / 2 - 2) value = Height / 2 - 2;

                vLarguraFundo = value; Invalidate();
            }
        }

        private int vLarguraBarra = 10;
        [DisplayName("_Largura da Barra com Valor")]
        public int LarguraBarra
        {
            get { return vLarguraBarra; }
            set
            {

                if (value < 1) value = 1;
                if (value > Height / 2 - 2) value = Height / 2 - 2;

                vLarguraBarra = value; Invalidate();
            }
        }

        private Color vCorFundoBarra = Color.Gainsboro;
        [DisplayName("_Cor de Fundo da Barra")]
        public Color CorFundoBarra
        {
            get { return vCorFundoBarra; }
            set { vCorFundoBarra = value; Invalidate(); }
        }


        private Color vCorBarra = Color.Purple;
        [DisplayName("_Cor da Barra")]
        public Color CorBarra
        {
            get { return vCorBarra; }
            set { vCorBarra = value; Invalidate(); }
        }

        private Color vCorFundo = Color.White;
        [DisplayName("_Cor de Fundo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private float vPercentual = 0;
        [DisplayName("_Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                targetPercentual = value; // Armazena o valor de destino para animação
                timer.Start(); // Inicia o timer para começar a animação
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = ClientRectangle;

            int t = LarguraFundo / 2;
            if (LarguraBarra > LarguraFundo)
                t += (LarguraBarra - LarguraFundo) / 2;

            Base.Inflate(-t, -t);
            Base.Width-=2;
            Base.Height-=2;

            using (Pen Caneta = new Pen(CorFundoBarra, LarguraFundo))
            using (SolidBrush Pincel = new SolidBrush(CorFundo))
            {
                g.FillEllipse(Pincel, Base);

                g.DrawArc(Caneta, Base, 0, 360);

                if (Percentual > 0)
                {
                    Caneta.StartCap = LineCap.Round;
                    Caneta.EndCap = LineCap.Round;
                    Caneta.Color = CorBarra;
                    Caneta.Width = LarguraBarra;

                    if (Percentual == 100)
                        g.DrawArc(Caneta, Base, 270, 360);
                    else
                        g.DrawArc(Caneta, Base, 270 + LarguraBarra / 2, (360 - LarguraBarra - 2) * (Percentual / 100));

                }

                SizeF tTexto = g.MeasureString(Percentual + "%", Font);

                Pincel.Color = ForeColor;
                g.DrawString(Percentual + "%", Font, Pincel, 
                    (Width - tTexto.Width) /2 , 
                    (Height - tTexto.Height) /2);
            }

        }
    }
}
