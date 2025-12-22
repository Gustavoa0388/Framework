using System.ComponentModel;
using System.Drawing.Drawing2D;
using GS.Core.UI.Utils.Legacy;



namespace GS.Core.UI.Controls
{
    public class GsRadioButton : RadioButton
    {
        private System.Windows.Forms.Timer animationTimer;
        private float currentMarkerSize;
        private bool isAnimating;
        private bool expanding; // Controla se o marcador está aumentando ou diminuindo

        public GsRadioButton()
        {
            Tag = "";

            DoubleBuffered = true;
            Cursor = Cursors.Hand;

            // Inicializar o temporizador de animação
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 15; // Intervalo para a animação, ajustável
            animationTimer.Tick += AnimationTimer_Tick;

            // Definir o tamanho inicial do marcador
            currentMarkerSize = 0; // Começa com o marcador invisível
            expanding = false;
            isAnimating = false;
        }

        private bool vValorPadrao = false;
        [DisplayName("_Definir Valor Padrão")]
        public bool valorPadrao
        {
            get { return vValorPadrao; }
            set
            {

                vValorPadrao = value;

                if (value == true)
                    Tag += "|valor_padrao";
                else
                    Tag = Tag.ToString().Replace("|valor_padrao", "");

            }
        }

        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set
            {

                vColuna = value;

                string valor = FuncoesLegacy.PegarTag(this, "col");

                Tag = Tag.ToString().Replace("col=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag = "col=" + value + Tag.ToString();

            }
        }

        private bool vObgt = false;
        [DisplayName("(DB.2 Campo Obrigatório)")]
        public bool Obgt
        {
            get { return vObgt; }
            set
            {

                vObgt = value;

                if (value == false)
                    Tag = Tag.ToString().Replace("|obgt", "");
                else
                    Tag += "|obgt";
            }
        }

        private bool vMarcarX = false;
        [DisplayName("(DB.3 Marcar Campo Obrigatório)")]
        public bool MarcarX
        {
            get { return vMarcarX; }
            set { 
                
                vMarcarX = value;

                if (value == false)
                    Tag = Tag.ToString().Replace("|x", "");
                else
                    Tag += "|x";

            }
        }

        private string vValorV;
        [DisplayName("(DB.4 Salvar este valor quando marcado)")]
        public string ValorV
        {
            get { return vValorV; }
            set
            {

                vValorV = value;

                string vAntigo = FuncoesLegacy.PegarTag(this, "valor");

                Tag = Tag.ToString().Replace("|valor=" + vAntigo, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|valor=" + value;

            }
        }



        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AjustarTamanho();
        }

        private void AjustarTamanho()
        {
            using (Graphics g = CreateGraphics())
            {
                // Tamanho do texto
                SizeF tamanhoTexto = g.MeasureString(Text, Font);

                // Calcule a largura necessária para a base e a bolinha
                float larguraNecessaria = Height * 1.4f + tamanhoTexto.Width + 12; // ajuste o multiplicador para outros modelos se necessário

                // Ajuste a largura do controle
                if (AutoSize)
                    MaximumSize = new Size((int)larguraNecessaria, (int)tamanhoTexto.Height - 4);

            }
        }
        protected override void OnCheckedChanged(EventArgs e)
        {
            // Iniciar a animação quando o estado mudar
            expanding = Checked; // Se estiver checado, expandir; caso contrário, contrair
            isAnimating = true;
            animationTimer.Start();

            if (Checked)
            {
                if (Parent != null)
                {
                    foreach (Control Ctr in Parent.Controls)
                    {
                        if (Ctr.Tag != null)
                        {
                            if (Ctr.Tag.ToString().Contains("|x"))
                            {
                                if (FuncoesLegacy.PegarTag(Ctr, "col") == FuncoesLegacy.PegarTag(this, "col"))
                                    FuncoesLegacy.RemoverLabel(Ctr);
                            }
                        }
                    }
                }
            }

            base.OnCheckedChanged(e);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Atualizar o tamanho da bolinha gradualmente
            if (expanding)
            {
                int TamanhoIcone = Height - 1;

                currentMarkerSize += 1; // Aumentar a bolinha

                if (currentMarkerSize >= TamanhoIcone - TamanhoMarcador - 3)
                {
                    currentMarkerSize = TamanhoIcone - TamanhoMarcador - 3; // Limitar ao tamanho máximo
                    isAnimating = false;
                    animationTimer.Stop(); // Parar a animação
                }
            }
            else
            {
                currentMarkerSize -= 1; // Diminuir a bolinha
                if (currentMarkerSize <= 0)
                {
                    currentMarkerSize = 0; // Limitar ao tamanho mínimo
                    isAnimating = false;
                    animationTimer.Stop(); // Parar a animação
                }
            }

            Invalidate(); // Redesenhar o controle
        }

        #region Propriedades

        private int vArredondamento = 50;

        [DisplayName("_Arredondamento")]
        [Category("_ECTurbo")]
        [Description("Define o nível arredondamento do controle")]
        public int Arredondamento
        {
            get { return vArredondamento; }
            set
            {

                if (value <= 0) value = 1;

                if (value > Height) value = Height;

                vArredondamento = value;
                Invalidate();
            }
        }


        private Color vCorBorda = Color.Green;
        [DisplayName("_Marcado Cor Borda")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vCorBordaDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor Borda")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorBordaDesmarcado
        {
            get { return vCorBordaDesmarcado; }
            set { vCorBordaDesmarcado = value; Invalidate(); }
        }


        private Color vCorFundo = Color.MediumAquamarine;
        [DisplayName("_Marcado Cor 1")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private Color vCorFundo2 = Color.DarkGreen;
        [DisplayName("_Marcado Cor 2")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorFundo2
        {
            get { return vCorFundo2; }
            set { vCorFundo2 = value; Invalidate(); }
        }

        //private Color vCorFundoDesmarcado = Color.Transparent;
        //[DisplayName("_Desmarcado Cor")]
        //[Category("_ECTurbo")]
        //[Description("???")]
        //public Color CorFundoDesmarcado
        //{
        //    get { return vCorFundoDesmarcado; }
        //    set { vCorFundoDesmarcado = value; Invalidate(); }
        //}


        private Color vCorTextoDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor Texto")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorTextoDesmarcado
        {
            get { return vCorTextoDesmarcado; }
            set { vCorTextoDesmarcado = value; Invalidate(); }
        }

        //private int vTamanhoIcone = 16;

        //[DisplayName("_Tamanho do Icone")]
        //[Category("_ECTurbo")]
        //[Description("Define o Tamanho Icone do controle")]
        //public int TamanhoIcone
        //{
        //    get { return vTamanhoIcone; }
        //    set
        //    {
        //        if (value < 14) value = 14;

        //        if (value > Height - 1) value = Height - 1;

        //        vTamanhoIcone = value;
        //        Invalidate();
        //    }
        //}

        private int vTamanhoMarcador = 4;

        [DisplayName("_Tamanho do Marcador")]
        [Category("_ECTurbo")]
        [Description("Define o Tamanho Marcador do controle")]
        public int TamanhoMarcador
        {
            get { return vTamanhoMarcador; }
            set
            {
                if (value < 2) value = 2;

                if (value > 4) value = 4;

                vTamanhoMarcador = value;

                Invalidate();
            }
        }

        #endregion


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            int TamanhoIcone = Height - 1;
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            RectangleF Base = new RectangleF(0, 0, TamanhoIcone, TamanhoIcone);
            Color CorB = Checked ? CorBorda : CorBordaDesmarcado;
            Color CorT = Checked ? ForeColor : CorTextoDesmarcado;

            using (Pen Caneta = new Pen(CorB, 1))
            using (GraphicsPath path = FuncoesLegacy.CriarPath(Base, Arredondamento))
            using (SolidBrush Pincel = new SolidBrush(CorT))
            {
                g.DrawPath(Caneta, path);

                SizeF TamanhoTexto = g.MeasureString(Text, Font);
                g.DrawString(Text, Font, Pincel, Base.X + Base.Width + 4, Base.Y + (Base.Height - TamanhoTexto.Height) / 2);
            }

            if (Checked || isAnimating)
            {
                // Inflar o marcador baseado no tamanho especificado em TamanhoMarcador
                Base.Inflate(-TamanhoMarcador, -TamanhoMarcador);
                Base.Inflate(-((TamanhoIcone - TamanhoMarcador * 2) - currentMarkerSize) / 2, -((TamanhoIcone - TamanhoMarcador * 2) - currentMarkerSize) / 2);

                if (Base.Width > 0)
                {
                    using (GraphicsPath path = FuncoesLegacy.CriarPath(Base, Arredondamento / 2))
                    using (LinearGradientBrush Pincel = new LinearGradientBrush(Base, CorFundo, CorFundo2, 90))
                    {
                        g.FillPath(Pincel, path);
                    }
                }
            }
        }
    }
}
