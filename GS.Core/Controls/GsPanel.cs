using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using GS.Core.UI.Utils.Legacy;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsPanel (LEGACY / ECTurbo)
    /// 
    /// Painel customizado com:
    /// - Bordas arredondadas independentes por canto
    /// - Suporte a imagem decorativa interna
    /// - Efeito de sombra configurável
    /// 
    /// ⚠️ IMPORTANTE:
    /// Este controle é oriundo do framework ECTurbo e
    /// AINDA NÃO FOI REFATORADO para o padrão GS Core.
    /// 
    /// Situação atual:
    /// - Usa cores hardcoded
    /// - Usa propriedades visuais diretas
    /// - Aplica theme de forma mínima
    /// 
    /// Planejamento futuro:
    /// - Migrar cores para tokens do GsTheme
    /// - Isolar completamente Utils.Legacy
    /// - Padronizar comportamento visual
    /// </summary>
    public class GsPanel : Panel, IThemedControl
    {
        public GsPanel()
        {
            // Evita flicker em renderizações customizadas
            DoubleBuffered = true;
        }

        // =====================================================
        // ARREDONDAMENTO DE CANTOS
        // =====================================================

        private int vRaio1 = 10;
        [DisplayName("_Raio Esquerda Superior")]
        public int Raio1
        {
            get => vRaio1;
            set
            {
                if (value < 1) value = 1;
                if (value > Height) value = Height;
                vRaio1 = value;
                Invalidate();
            }
        }

        private int vRaio2 = 10;
        [DisplayName("_Raio Direita Superior")]
        public int Raio2
        {
            get => vRaio2;
            set
            {
                if (value < 1) value = 1;
                if (value > Height) value = Height;
                vRaio2 = value;
                Invalidate();
            }
        }

        private int vRaio3 = 10;
        [DisplayName("_Raio Direita Inferior")]
        public int Raio3
        {
            get => vRaio3;
            set
            {
                if (value < 1) value = 1;
                if (value > Height) value = Height;
                vRaio3 = value;
                Invalidate();
            }
        }

        private int vRaio4 = 10;
        [DisplayName("_Raio Esquerda Inferior")]
        public int Raio4
        {
            get => vRaio4;
            set
            {
                if (value < 1) value = 1;
                if (value > Height) value = Height;
                vRaio4 = value;
                Invalidate();
            }
        }

        // =====================================================
        // BORDA
        // =====================================================

        private Color vCorBorda = Color.Gray; // LEGACY: hardcoded
        [DisplayName("_Borda Cor")]
        public Color CorBorda
        {
            get => vCorBorda;
            set
            {
                vCorBorda = value;
                Invalidate();
            }
        }

        private int vTamanhoBorda;
        [DisplayName("_Borda Tamanho")]
        public int TamanhoBorda
        {
            get => vTamanhoBorda;
            set
            {
                if (value < 0) value = 0;
                vTamanhoBorda = value;
                Invalidate();
            }
        }

        // =====================================================
        // IMAGEM DECORATIVA
        // =====================================================

        private Image vImagem;
        [DisplayName("_Imagem")]
        public Image Imagem
        {
            get => vImagem;
            set
            {
                vImagem = value;
                Invalidate();
            }
        }

        public enum PosImagem
        {
            Centro,
            Esquerda,
            Direita
        }

        private int vEspImagem = 10;
        [DisplayName("_Imagem Espaçamento")]
        public int EspImagem
        {
            get => vEspImagem;
            set
            {
                vEspImagem = value;
                Invalidate();
            }
        }

        private PosImagem vPosicaoImagem = PosImagem.Esquerda;
        [DisplayName("_Imagem Posição")]
        public PosImagem PosicaoImagem
        {
            get => vPosicaoImagem;
            set
            {
                vPosicaoImagem = value;
                Invalidate();
            }
        }

        // =====================================================
        // ESPAÇAMENTO INTERNO SUPERIOR
        // =====================================================

        private int vPanelEspacoSuperior = 0;
        [DisplayName("_Espaço Superior")]
        public int PanelEspacoSuperior
        {
            get => vPanelEspacoSuperior;
            set
            {
                vPanelEspacoSuperior = value;
                Invalidate();
            }
        }

        // =====================================================
        // SOMBRA (EFEITO VISUAL)
        // =====================================================

        private bool vAtivarSombra = false;
        [Category("_ECTurbo")]
        [DisplayName("_Sombra Ativar")]
        public bool AtivarSombra
        {
            get => vAtivarSombra;
            set
            {
                vAtivarSombra = value;
                Invalidate();
            }
        }

        private int vEspacamento = 0;
        [DisplayName("_Sombra Espaçamento")]
        public int Espacamento
        {
            get => vEspacamento;
            set
            {
                vEspacamento = value;
                Invalidate();
            }
        }

        private float vSombraForca = 8;
        [DisplayName("_Sombra Intensidade")]
        public float SombraForca
        {
            get => vSombraForca;
            set
            {
                vSombraForca = value;
                Invalidate();
            }
        }

        private Color vSombraCor = Color.Black; // LEGACY: hardcoded
        [DisplayName("_Sombra Cor")]
        public Color SombraCor
        {
            get => vSombraCor;
            set
            {
                vSombraCor = value;
                Invalidate();
            }
        }

        // =====================================================
        // RENDERIZAÇÃO
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Limpa usando a cor do container pai
            g.Clear(Parent.BackColor);

            RectangleF area = ClientRectangle;

            int ajuste = TamanhoBorda / 2 < 1 ? 1 : TamanhoBorda / 2;
            if (AtivarSombra)
                ajuste += Espacamento;

            area.Inflate(-ajuste, -ajuste);
            area.Width--;
            area.Height--;

            area.Height -= PanelEspacoSuperior;
            area.Y += PanelEspacoSuperior;

            // Desenha sombra (se ativada)
            EfeitoSombra(g, area);

            // Corpo do painel
            using (GraphicsPath path = FuncoesLegacy.CriarPath(
                area, 1, Raio1, Raio2, Raio3, Raio4))
            using (Pen pen = new Pen(CorBorda, TamanhoBorda))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);

                if (TamanhoBorda > 0)
                    g.DrawPath(pen, path);
            }

            // Desenha imagem decorativa (se existir)
            if (Imagem != null)
            {
                int x = EspImagem;
                float ratio = (float)Imagem.Width / Imagem.Height;
                int altura = Height - 10;
                int largura = (int)(altura * ratio);

                if (PosicaoImagem == PosImagem.Centro)
                    x = (Width - largura) / 2;
                else if (PosicaoImagem == PosImagem.Direita)
                    x = Width - largura - EspImagem;

                g.DrawImage(Imagem, x, 5, largura, altura);
            }
        }

        // =====================================================
        // SOMBRA - IMPLEMENTAÇÃO
        // =====================================================

        private void EfeitoSombra(Graphics g, RectangleF r)
        {
            if (!AtivarSombra)
                return;

            float blur = SombraForca;
            Color cor = Color.FromArgb(128, SombraCor);

            RectangleF sombra = new RectangleF(
                r.X - blur,
                r.Y - blur,
                r.Width + 2 * blur,
                r.Height + 2 * blur
            );

            using (GraphicsPath path = FuncoesLegacy.CriarPath(
                sombra, 1, Raio1, Raio2, Raio3, Raio4))
            using (PathGradientBrush brush = new PathGradientBrush(path))
            {
                brush.CenterColor = cor;
                brush.SurroundColors = new[] { Color.Transparent };
                brush.FocusScales = new PointF(0.5f, 0.5f);

                g.FillPath(brush, path);
            }
        }

        // =====================================================
        // THEME (APLICAÇÃO MÍNIMA)
        // =====================================================

        /// <summary>
        /// Aplica tema de forma mínima.
        /// 
        /// ⚠️ LEGACY:
        /// Ainda não consome corretamente todos os tokens do GsTheme.
        /// </summary>
        public void ApplyTheme(GsTheme theme)
        {
            BackColor = theme.Secondary;
        }
    }
}
