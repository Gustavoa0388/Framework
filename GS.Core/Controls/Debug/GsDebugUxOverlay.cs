using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Controls.Debug
{
    /// <summary>
    /// GsDebugUxOverlay
    ///
    /// Overlay visual flutuante para observabilidade de UX.
    /// Somente leitura. Opt-in.
    /// </summary>
    public sealed partial class GsDebugUxOverlay : UserControl
    {
        // =====================================================
        // CAMPOS INTERNOS (estado observado)
        // =====================================================

        private GsUxState _currentUxState = GsUxState.Hidden;
        private bool _busyActive;
        private bool _skeletonActive;
        private GsFormResultType _lastFormResult = GsFormResultType.None;
        private bool _diagnosticsAllowed;
        private bool _hasTechnicalDetails;

        // =====================================================
        // CONTROLES VISUAIS
        // =====================================================

        private readonly Label lblHeader;
        private readonly Label lblUxState;
        private readonly Label lblBusy;
        private readonly Label lblSkeleton;
        private readonly Label lblNavigation;
        private readonly Label lblDiagnostics;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        public GsDebugUxOverlay()
        {
            Dock = DockStyle.None;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Size = new Size(260, 180);
            BackColor = Color.FromArgb(230, 30, 30, 30);
            ForeColor = Color.White;
            Visible = false;

            lblHeader = new Label
            {
                Text = "DEBUG UX",
                Dock = DockStyle.Top,
                Height = 24,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            lblUxState = CreateLineLabel();
            lblBusy = CreateLineLabel();
            lblSkeleton = CreateLineLabel();
            lblNavigation = CreateLineLabel();
            lblDiagnostics = CreateLineLabel();

            var content = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(8)
            };

            content.Controls.Add(lblUxState);
            content.Controls.Add(lblBusy);
            content.Controls.Add(lblSkeleton);
            content.Controls.Add(lblNavigation);
            content.Controls.Add(lblDiagnostics);

            Controls.Add(content);
            Controls.Add(lblHeader);

            AtualizarVisual();
        }

        // =====================================================
        // API PÚBLICA — SINCRONIZAÇÃO
        // =====================================================

        /// <summary>
        /// Atualiza todos os estados observáveis do Debug UX.
        /// Método de integração com o GsBaseForm.
        /// </summary>
        public void UpdateState(
            GsUxState uxState,
            bool isBusy,
            bool isSkeletonActive,
            GsFormResultType formResult,
            bool diagnosticsAllowed,
            bool hasTechnicalDetails)
        {
            _currentUxState = uxState;
            _busyActive = isBusy;
            _skeletonActive = isSkeletonActive;
            _lastFormResult = formResult;
            _diagnosticsAllowed = diagnosticsAllowed;
            _hasTechnicalDetails = hasTechnicalDetails;

            AtualizarVisual();
        }

        // =====================================================
        // VISUAL
        // =====================================================

        private void AtualizarVisual()
        {
            lblUxState.Text = $"UX State: {_currentUxState}";
            lblBusy.Text = $"Busy: {(_busyActive ? "Active" : "Inactive")}";
            lblSkeleton.Text = $"Skeleton: {(_skeletonActive ? "Active" : "Inactive")}";
            lblNavigation.Text = $"FormResult: {_lastFormResult}";
            lblDiagnostics.Text =
                $"Diagnostics: {(_diagnosticsAllowed ? "Allowed" : "Blocked")} | Details: {(_hasTechnicalDetails ? "Yes" : "No")}";
        }

        private static Label CreateLineLabel()
        {
            return new Label
            {
                Height = 20,
                Width = 220,
                Font = new Font("Segoe UI", 8F),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }
    }
}
