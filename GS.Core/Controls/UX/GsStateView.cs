using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.UX
{
    /// <summary>
    /// Componente visual reutilizável para exibição
    /// de estados de UX (Loading, Empty, Error).
    /// </summary>
    public class GsStateView : UserControl, IThemedControl
    {
        private readonly Label lblMessage;
        private readonly ProgressBar progress;

        private GsUxState _state = GsUxState.Hidden;

        public GsUxState State
        {
            get => _state;
            set
            {
                _state = value;
                AtualizarVisual();
            }
        }

        public string Message { get; set; }

        public GsStateView()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;

            lblMessage = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            progress = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Width = 180,
                Height = 20,
                Visible = false
            };

            Controls.Add(lblMessage);
            Controls.Add(progress);
        }

        private void AtualizarVisual()
        {
            Visible = _state != GsUxState.Hidden;

            lblMessage.Visible = false;
            progress.Visible = false;

            switch (_state)
            {
                case GsUxState.Loading:
                    progress.Visible = true;
                    break;

                case GsUxState.Empty:
                    lblMessage.Text = Message ?? "Nenhum registro encontrado";
                    lblMessage.Visible = true;
                    break;

                case GsUxState.Error:
                    lblMessage.Text = Message ?? "Erro ao carregar dados";
                    lblMessage.Visible = true;
                    break;
            }
        }

        public void ApplyTheme(GsTheme theme)
        {
            lblMessage.Font = theme.DefaultFont;
            lblMessage.ForeColor = theme.TextSecondary;
        }
    }
}
