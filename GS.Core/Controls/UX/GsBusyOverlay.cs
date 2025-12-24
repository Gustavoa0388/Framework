using GS.Core.UI.Controls.Display;
using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsBusyOverlay
    ///
    /// Overlay visual de bloqueio de interação com indicação
    /// de carregamento.
    ///
    /// RESPONSABILIDADE:
    /// - Bloquear interação do usuário
    /// - Indicar visualmente operação em andamento
    ///
    /// NÃO FAZ:
    /// - Não executa tarefas
    /// - Não decide quando aparecer
    /// - Não conhece fluxo de negócio
    /// </summary>
    public partial class GsBusyOverlay : UserControl, IThemedControl
    {
        private readonly Label lblMessage;
        private readonly GsProgressBar progress;

        private GsTheme _theme;

        public GsBusyOverlay()
        {
            Dock = DockStyle.Fill;
            Visible = false;
            Enabled = true; // 🔑 captura input
            TabStop = true;

            BackColor = Color.Transparent;

            lblMessage = new Label
            {
                AutoSize = false,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter
            };

            progress = new GsProgressBar
            {
                Width = 220,
                Animated = true
            };

            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20),
                Anchor = AnchorStyles.None
            };

            layout.Controls.Add(lblMessage);
            layout.Controls.Add(progress);

            Controls.Add(layout);
        }

        // =====================================================
        // API PÚBLICA
        // =====================================================

        /// <summary>
        /// Exibe o overlay.
        /// </summary>
        public void Show(string message = null)
        {
            lblMessage.Text = message ?? "Processando...";
            Visible = true;
            BringToFront();
            Focus();
        }

        /// <summary>
        /// Oculta o overlay.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;

            // Overlay translúcido baseado em Surface
            BackColor = Color.FromArgb(
                theme.IsDark ? 180 : 140,
                theme.Surface
            );

            lblMessage.Font = theme.DefaultFont;
            lblMessage.ForeColor = theme.TextPrimary;

            progress.ApplyTheme(theme);
            progress.State = ProgressState.Normal;
        }

        // =====================================================
        // BLOQUEIO DE INPUT
        // =====================================================

        protected override void OnMouseDown(MouseEventArgs e) { /* bloqueia */ }
        protected override void OnMouseMove(MouseEventArgs e) { /* bloqueia */ }
        protected override void OnMouseUp(MouseEventArgs e) { /* bloqueia */ }
        protected override bool IsInputKey(Keys keyData) => true;
    }
}
