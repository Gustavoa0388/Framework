using GS.Core.UI.Theming;
using GS.Core.UI.Controls.Display;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// GsStateView
    ///
    /// Componente visual reutilizável para exibição
    /// de estados globais de UX (Loading, Empty, Error, Success, Disabled).
    ///
    /// RESPONSABILIDADE:
    /// - Exibir feedback visual global
    /// - Reagir ao estado informado pelo container
    ///
    /// NÃO FAZ:
    /// - Lógica de negócio
    /// - Carga de dados
    /// - Decisão de estado
    /// </summary>
    public class GsStateView : UserControl, IThemedControl
    {
        private readonly Label lblTitle;
        private readonly Label lblMessage;
        private readonly GsProgressBar progress;
        private readonly Button btnAction;

        private GsTheme _theme;
        private GsUxState _state = GsUxState.Hidden;

        // =====================================================
        // PROPRIEDADES
        // =====================================================

        public GsUxState State
        {
            get => _state;
            set
            {
                _state = value;
                AtualizarVisual();
            }
        }

        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string Message
        {
            get => lblMessage.Text;
            set => lblMessage.Text = value;
        }

        public bool ShowProgress { get; set; }

        public ProgressState ProgressState
        {
            get => progress.State;
            set => progress.State = value;
        }

        public string ActionText
        {
            get => btnAction.Text;
            set => btnAction.Text = value;
        }

        public event EventHandler ActionClicked;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        public GsStateView()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;
            Visible = false;

            lblTitle = new Label
            {
                AutoSize = false,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblMessage = new Label
            {
                AutoSize = false,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            progress = new GsProgressBar
            {
                Width = 220,
                Visible = false
            };

            btnAction = new Button
            {
                Visible = false,
                Width = 140,
                Height = 32
            };

            btnAction.Click += (_, _) => ActionClicked?.Invoke(this, EventArgs.Empty);

            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(20),
                Anchor = AnchorStyles.None
            };

            layout.Controls.Add(lblTitle);
            layout.Controls.Add(lblMessage);
            layout.Controls.Add(progress);
            layout.Controls.Add(btnAction);

            Controls.Add(layout);
        }

        // =====================================================
        // THEME
        // =====================================================

        public void ApplyTheme(GsTheme theme)
        {
            _theme = theme;

            lblTitle.Font = theme.SubtitleFont;
            lblMessage.Font = theme.DefaultFont;

            btnAction.Font = theme.DefaultFont;
            btnAction.BackColor = theme.Primary;
            btnAction.ForeColor = theme.TextOnPrimary;

            progress.ApplyTheme(theme);

            AtualizarVisual();
        }

        // =====================================================
        // VISUAL
        // =====================================================

        private void AtualizarVisual()
        {
            Visible = _state != GsUxState.Hidden;

            lblTitle.Visible = false;
            lblMessage.Visible = false;
            progress.Visible = false;
            btnAction.Visible = false;

            if (_theme == null)
                return;

            Color textColor;

            switch (_state)
            {
                case GsUxState.Loading:
                    textColor = _theme.Info;
                    lblTitle.Text = "Carregando";
                    lblMessage.Text = Message;
                    progress.Visible = ShowProgress;
                    break;

                case GsUxState.Empty:
                    textColor = _theme.Info;
                    lblTitle.Text = "Sem dados";
                    lblMessage.Text = Message;
                    break;

                case GsUxState.Error:
                    textColor = _theme.Error;
                    lblTitle.Text = "Erro";
                    lblMessage.Text = Message;
                    break;

                case GsUxState.Success:
                    textColor = _theme.Success;
                    lblTitle.Text = "Sucesso";
                    lblMessage.Text = Message;
                    break;

                case GsUxState.Disabled:
                    textColor = _theme.TextSecondary;
                    lblTitle.Text = "Indisponível";
                    lblMessage.Text = Message;
                    break;

                default:
                    return;
            }

            lblTitle.ForeColor = textColor;
            lblMessage.ForeColor = textColor;

            lblTitle.Visible = true;
            lblMessage.Visible = true;
            btnAction.Visible = !string.IsNullOrWhiteSpace(btnAction.Text);
        }
    }
}
