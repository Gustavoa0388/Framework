using System.ComponentModel;
using System.Drawing.Drawing2D;


using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_CheckBox : CheckBox
    {
        private System.Windows.Forms.Timer timer;
        private float animationProgress;
        private const float animationStep = 0.05f; // Define a velocidade da animação

        public ECTurbo_CheckBox()
        {
            Tag = "";

            DoubleBuffered = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1; // Intervalo da animação em milissegundos
            timer.Tick += Timer_Tick;
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

        private string vValorV;
        [DisplayName("(DB.2 Salvar este valor quando marcado)")]
        public string ValorV
        {
            get { return vValorV; }
            set
            {

                vValorV = value;

                string vAntigo = Funcoes.PegarTag(this, "valor");

                Tag = Tag.ToString().Replace("|valor=" + vAntigo, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|valor=" + value;

            }
        }


        private string vValorF;
        [DisplayName("(DB.3 Salvar este valor quando Desmarcado)")]
        public string ValorF
        {
            get { return vValorF; }
            set
            {

                vValorF = value;

                string vAntigo = Funcoes.PegarTag(this, "valorF");

                Tag = Tag.ToString().Replace("|valorF=" + vAntigo, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|valorF=" + value;

            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //ForeColor = Config.CorPrimaria;
            Cursor = Cursors.Hand;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementa o progresso da animação
            animationProgress += animationStep;

            if (animationProgress >= 1f)
            {
                animationProgress = 1f;
                timer.Stop(); // Para o timer ao finalizar a animação
            }

            Invalidate(); // Redesenha o controle
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);


            // Atualiza o texto baseado no estado do controle
            if (TextoMarcado != string.Empty && TextoDesMarcado != string.Empty)
                Text = Checked ? TextoMarcado : TextoDesMarcado;

            // Inicia a animação quando o estado é alterado
            if (Checked)
            {
                animationProgress = 0f;
                timer.Start();
            }

        }


        #region Propriedades

        private int vArredondamento = 5;

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
        [DisplayName("_Desmarcado Cor Borda ")]
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

        private Color vCorFundoDesmarcado = Color.Transparent;
        [DisplayName("_Desmarcado Cor")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorFundoDesmarcado
        {
            get { return vCorFundoDesmarcado; }
            set { vCorFundoDesmarcado = value; Invalidate(); }
        }

        private Color vCorIcone = Color.White;
        [DisplayName("_Marcado Cor do Ícone")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorIcone
        {
            get { return vCorIcone; }
            set { vCorIcone = value; Invalidate(); }
        }

        private string vTextoMarcado = string.Empty;
        [DisplayName("_Marcado Texto")]
        public string TextoMarcado
        {
            get { return vTextoMarcado; }
            set
            {
                vTextoMarcado = value;

                if (Checked && value != string.Empty)
                    Text = value; Invalidate();
            }
        }

        private string vTextoDesMarcado = string.Empty;
        [DisplayName("_Desmarcado Texto")]
        public string TextoDesMarcado
        {
            get { return vTextoDesMarcado; }
            set
            {
                vTextoDesMarcado = value;

                if (!Checked && value != string.Empty)
                {
                    Text = value;
                }
                else
                {
                    if (TextoMarcado != string.Empty)
                        Text = TextoMarcado;
                }

                Invalidate();
            }
        }


        //private Color vCorIconeDesmarcado = Color.Transparent;
        //[DisplayName("_Desmarcado Cor do Icone")]
        //[Category("_ECTurbo")]
        //[Description("???")]
        //public Color CorIconeDesmarcado
        //{
        //    get { return vCorIconeDesmarcado; }
        //    set { vCorIconeDesmarcado = value; Invalidate(); }
        //}

        private Color vCorTextoDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor do Texto")]
        [Category("_ECTurbo")]
        [Description("???")]
        public Color CorTextoDesmarcado
        {
            get { return vCorTextoDesmarcado; }
            set { vCorTextoDesmarcado = value; Invalidate(); }
        }

        //private int vTamanhoIcone = 16;

        //[DisplayName("(Tamanho do Icone)")]
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

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            RectangleF Base = new RectangleF(0, 0, Height, Height);

            int t = 4;
            Base.Inflate(-t, -t);

            Color CorB = Checked ? CorBorda : CorBordaDesmarcado;
            Color CorF = Checked ? CorFundo : CorFundoDesmarcado;
            //Color CorI = Checked ? CorIcone : CorIconeDesmarcado;
            Color CorI = CorIcone;

            using (GraphicsPath path = Funcoes.CriarPath(Base, Arredondamento))
            using (Pen Caneta = new Pen(CorI, 2))
            using (SolidBrush Pincel = new SolidBrush(Checked ? ForeColor : CorTextoDesmarcado))
            using (LinearGradientBrush PincelFundo = new LinearGradientBrush(Base, CorFundo, CorFundo2, 90))
            {
                SizeF TamanhoTexto = g.MeasureString(Text, Font);
                g.DrawString(Text, Font, Pincel, Base.X + Base.Width + 4, Base.Y + (Base.Height - TamanhoTexto.Height) / 2);

                Pincel.Color = CorF;
                g.FillPath(Checked ? PincelFundo : Pincel, path);

                // Desenho animado do ícone "v"
                if (Checked)
                {
                    DrawAnimatedCheckMark(g, Base, Caneta, animationProgress);
                }

                Caneta.Color = CorB;
                Caneta.Width = 1;
                g.DrawPath(Caneta, path);
            }
        }

        private void DrawAnimatedCheckMark(Graphics g, RectangleF baseRect, Pen pen, float progress)
        {
            // Desenha o "v" parcialmente, dependendo do progresso da animação

            float startX1 = baseRect.X + baseRect.Width * 0.2f;
            float startY1 = baseRect.Y + baseRect.Height * 0.5f;
            float endX1 = baseRect.X + baseRect.Width * 0.5f;
            float endY1 = baseRect.Y + baseRect.Height * 0.8f;

            float startX2 = endX1;
            float startY2 = endY1;
            float endX2 = baseRect.X + baseRect.Width * 0.85f;
            float endY2 = baseRect.Y + baseRect.Height * 0.2f;

            if (progress <= 0.5f)
            {
                // Desenha a primeira metade do "v"
                float partialProgress = progress * 2; // Ajusta o progresso para a primeira metade
                float currentX = startX1 + (endX1 - startX1) * partialProgress;
                float currentY = startY1 + (endY1 - startY1) * partialProgress;
                g.DrawLine(pen, startX1, startY1, currentX, currentY);
            }
            else
            {
                // Desenha a primeira parte completa
                g.DrawLine(pen, startX1, startY1, endX1, endY1);

                // Desenha a segunda metade do "v"
                float partialProgress = (progress - 0.5f) * 2; // Ajusta o progresso para a segunda metade
                float currentX = startX2 + (endX2 - startX2) * partialProgress;
                float currentY = startY2 + (endY2 - startY2) * partialProgress;
                g.DrawLine(pen, startX2, startY2, currentX, currentY);
            }
        }


        private bool vValorPadrao = false;
        [DisplayName("_Definir Valor Padrão")]
        public bool valorPadrao
        {
            get { return vValorPadrao; }
            set { 
                
                vValorPadrao = value;

                if (value == true)
                {
                    if (!Tag.ToString().Contains("|valor_padrao"))
                        Tag += "|valor_padrao";
                }
                else
                    Tag = Tag.ToString().Replace("|valor_padrao", "");
                
            }
        }



    }
}
