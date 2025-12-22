using GS.Core.UI.Theming;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GS.Core.UI.Utils.Legacy;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// GsButton
    /// 
    /// Botão customizado com visual rico (gradiente, borda, sombra, ícone).
    /// 
    /// STATUS ARQUITETURAL:
    /// - GS Core (HÍBRIDO)
    /// 
    /// Motivo:
    /// - Implementa IThemedControl
    /// - Participa do ThemeManager
    /// - Porém ainda possui:
    ///   - Cores hardcoded
    ///   - Gradiente custom
    ///   - Uso de FuncoesLegacy
    /// 
    /// PLANO FUTURO (FASE EXTRA):
    /// - Separar botão primário / secundário
    /// - Migrar cores para tokens do GsTheme
    /// - Padronizar estados (hover, pressed, disabled)
    /// </summary>
    public class GsButton : Button, IThemedControl
    {
        public GsButton()
        {
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        // =========================
        // PROPRIEDADES VISUAIS
        // =========================

        private int vTamBorda = 1;
        [DisplayName("_Largura da Borda")]
        public int TamBorda
        {
            get => vTamBorda;
            set
            {
                if (value < 0) value = 0;
                if (value > 3) value = 3;
                vTamBorda = value;
                Invalidate();
            }
        }

        private int vDistIcone = 5;
        [DisplayName("_Distancia do Icone")]
        public int DistIcone
        {
            get => vDistIcone;
            set { vDistIcone = value; Invalidate(); }
        }

        private int vArred = 20;
        [DisplayName("_Arredondamento")]
        public int Arred
        {
            get => vArred;
            set { vArred = value; Invalidate(); }
        }

        // =========================
        // CORES (LEGACY / HARDCORE)
        // =========================

        private Color vCorBorda = Color.Gray;
        [DisplayName("_Cor da Borda")]
        public Color CorBorda
        {
            get => vCorBorda;
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vCor1 = Color.Purple;
        [DisplayName("_Cor de Fundo 1")]
        public Color Cor1
        {
            get => vCor1;
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.Pink;
        [DisplayName("_Cor de Fundo 2")]
        public Color Cor2
        {
            get => vCor2;
            set { vCor2 = value; Invalidate(); }
        }

        private int vAngulo = 1;
        [DisplayName("_Angulo do Gradiente")]
        public int Angulo
        {
            get => vAngulo;
            set
            {
                if (value < 1) value = 1;
                vAngulo = value;
                Invalidate();
            }
        }

        // =========================
        // IMAGEM
        // =========================

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
            get => vPosicaoImagem;
            set { vPosicaoImagem = value; Invalidate(); }
        }

        private int vTamanhoIcone = 16;
        [DisplayName("_Tamanho Icone")]
        public int TamanhoIcone
        {
            get => vTamanhoIcone;
            set { vTamanhoIcone = value; Invalidate(); }
        }

        // =========================
        // SOMBRA
        // =========================

        private bool vAtivarSombra;
        [DisplayName("_Sombra Inferior - Ativar")]
        public bool AtivarSombra
        {
            get => vAtivarSombra;
            set { vAtivarSombra = value; Invalidate(); }
        }

        private Color vCorSombra = Color.Black;
        [DisplayName("_Sombra Inferior - Cor")]
        public Color CorSombra
        {
            get => vCorSombra;
            set { vCorSombra = value; Invalidate(); }
        }

        private int vTamanhoSombra = 3;
        [DisplayName("_Sombra Inferior - Tamanho")]
        public int TamanhoSombra
        {
            get => vTamanhoSombra;
            set
            {
                if (value < 1) value = 1;
                if (value > 5) value = 5;
                vTamanhoSombra = value;
                Invalidate();
            }
        }

        // =========================
        // RENDERIZAÇÃO
        // =========================

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            RectangleF area = ClientRectangle;

            if (TamBorda > 1)
                area.Inflate(-(TamBorda / 2), -(TamBorda / 2));

            area.Width--;
            area.Height--;

            // Sombra inferior (LEGACY)
            if (AtivarSombra)
            {
                using var shadowPath = FuncoesLegacy.CriarPath(area, Arred);
                using var shadowBrush = new SolidBrush(CorSombra);
                g.FillPath(shadowBrush, shadowPath);

                area.Height -= TamanhoSombra;
            }

            using var path = FuncoesLegacy.CriarPath(area, Arred);
            using var bgBrush = new LinearGradientBrush(ClientRectangle, Cor1, Cor2, Angulo);
            using var borderPen = new Pen(CorBorda, TamBorda);
            using var textBrush = new SolidBrush(ForeColor);

            g.FillPath(bgBrush, path);

            if (TamBorda > 0)
                g.DrawPath(borderPen, path);

            DrawTextAndImage(g, area, textBrush);
        }

        private void DrawTextAndImage(Graphics g, RectangleF area, Brush textBrush)
        {
            SizeF textSize = g.MeasureString(Text, Font);
            SizeF imageSize = Image != null ? new SizeF(TamanhoIcone, TamanhoIcone) : SizeF.Empty;

            float tx = area.X + (area.Width - textSize.Width) / 2;
            float ty = area.Y + (area.Height - textSize.Height) / 2;

            g.DrawString(Text, Font, textBrush, tx + DistIcone, ty);

            if (Image != null)
                g.DrawImage(Image, tx - imageSize.Width, ty, TamanhoIcone, TamanhoIcone);
        }

        // =========================
        // INTERAÇÃO
        // =========================

        private Color Cor1Original;
        private Color Cor2Original;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            CacheCores();
            Cor1 = FuncoesLegacy.CorTransparente(Cor1, 40);
            Cor2 = FuncoesLegacy.CorTransparente(Cor2, 40);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            RestaurarCores();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CacheCores();
            Cor1 = FuncoesLegacy.CorTransparente(Cor1, 20);
            Cor2 = FuncoesLegacy.CorTransparente(Cor2, 20);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            RestaurarCores();
        }

        private void CacheCores()
        {
            if (Cor1Original == default)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }
        }

        private void RestaurarCores()
        {
            Cor1 = Cor1Original;
            Cor2 = Cor2Original;
        }

        // =========================
        // THEME (APLICAÇÃO BÁSICA)
        // =========================

        public void ApplyTheme(GsTheme theme)
        {
            BackColor = theme.Primary;
            ForeColor = theme.TextOnPrimary;
            Font = theme.DefaultFont;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = theme.Border;
        }
    }
}
