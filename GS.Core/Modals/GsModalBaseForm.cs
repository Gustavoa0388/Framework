using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Forms;
using GS.Core.UI.Forms.Navigation;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Modals
{
    /// <summary>
    /// GsModalBaseForm
    ///
    /// Form base abstrato para todos os modais corporativos
    /// do GS Core UI.
    ///
    /// RESPONSABILIDADE:
    /// - Centralizar layout, mensagem e botões
    /// - Garantir retorno semântico explícito
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não executa regra de negócio
    /// - Não conhece telas consumidoras
    /// </summary>
    public abstract class GsModalBaseForm : GsBaseForm
    {
        protected Label lblMessage;
        protected PictureBox picIcon;
        protected FlowLayoutPanel pnlButtons;

        protected Button btnPrimary;
        protected Button btnSecondary;

        protected GsModalBaseForm()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            Width = 420;
            Height = 220;

            BuildLayout();
        }

        // =====================================================
        // LAYOUT
        // =====================================================

        private void BuildLayout()
        {
            picIcon = new PictureBox
            {
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 0, 12, 0)
            };

            lblMessage = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var content = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var messageLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
            };

            messageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            messageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            messageLayout.Controls.Add(picIcon, 0, 0);
            messageLayout.Controls.Add(lblMessage, 1, 0);

            content.Controls.Add(messageLayout);

            pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                Height = 50
            };

            Controls.Add(content);
            Controls.Add(pnlButtons);
        }

        // =====================================================
        // API PROTEGIDA
        // =====================================================

        protected void SetMessage(string message)
        {
            lblMessage.Text = message;
        }

        protected void SetIcon(Image icon)
        {
            picIcon.Image = icon;
            picIcon.Visible = icon != null;
        }

        protected void ConfigurePrimaryButton(string text, GsFormResult result)
        {
            btnPrimary = CreateButton(text, () => CloseWithResult(result));
            pnlButtons.Controls.Add(btnPrimary);
        }

        protected void ConfigureSecondaryButton(string text, GsFormResult result)
        {
            btnSecondary = CreateButton(text, () => CloseWithResult(result));
            pnlButtons.Controls.Add(btnSecondary);
        }

        private Button CreateButton(string text, Action action)
        {
            var btn = new Button
            {
                Text = text,
                Width = 90,
                Height = 28
            };

            btn.Click += (_, _) => action();

            return btn;
        }

        // =====================================================
        // LIFECYCLE
        // =====================================================

        protected override bool OnBeforeClose()
        {
            // Se ninguém definiu resultado, fecha como Closed
            if (NavigationResult == null || NavigationResult.ResultType == GsFormResultType.None)
            {
                CloseWithResult(GsFormResult.Closed());
                return false;
            }

            return true;
        }
    }
}
