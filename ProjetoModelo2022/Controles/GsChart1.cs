using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GS.Core.UI.Controls
{
    public class GsChart1 : Control
    {
        private System.Windows.Forms.Timer animationTimer;
        private float targetPercentual;

        public GsChart1()
        {
            DoubleBuffered = true;
            Size = new Size(100, 100);

            // Configurando o Timer para animação
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 5; // Intervalo em milissegundos
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private int vLargura = 10;
        [DisplayName("_Largura da Barra")]
        public int Largura
        {
            get { return vLargura; }
            set {
                
                if (value < 1) value = 1;
                if (value > Height / 2 - 2) value = Height / 2 - 2;

                vLargura = value; Invalidate(); 
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
        [DisplayName("_Cor de Fundo da Barra")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private float vPercentual = 0;
        [DisplayName("_Valor Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                targetPercentual = Math.Max(0, Math.Min(100, value)); // Limita o valor entre 0 e 100
                animationTimer.Start(); // Inicia a animação
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Anima o valor de vPercentual em direção ao targetPercentual
            if (vPercentual < targetPercentual)
            {
                vPercentual += 1; // Aumenta gradualmente
                if (vPercentual > targetPercentual)
                    vPercentual = targetPercentual; // Garante que não passe do alvo
            }
            else if (vPercentual > targetPercentual)
            {
                vPercentual -= 1; // Diminui gradualmente
                if (vPercentual < targetPercentual)
                    vPercentual = targetPercentual; // Garante que não passe do alvo
            }

            if (vPercentual == targetPercentual)
                animationTimer.Stop(); // Para o Timer ao atingir o valor desejado

            Invalidate(); // Atualiza o controle para redesenho
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = ClientRectangle;

            Base.Inflate(-1, -1);
            Base.Width--;
            Base.Height--;

            using(SolidBrush Pincel = new SolidBrush(CorFundoBarra))
            {
                g.FillEllipse(Pincel, Base);

                if (Percentual > 0)
                {
                    Pincel.Color = CorBarra;
                    Base.Inflate(1, 1);
                    g.FillPie(Pincel, Base, 270, 360 * (Percentual / 100));
                }

                Pincel.Color = CorFundo;
                Base.Inflate(-Largura, -Largura);
                g.FillEllipse(Pincel, Base);

                Pincel.Color = ForeColor;
                SizeF t = g.MeasureString(Percentual.ToString() + "%", Font);
                g.DrawString(Percentual.ToString() + "%", Font, Pincel, (Width - t.Width) / 2, (Height - t.Height) / 2);
            }

        }
    }
}
