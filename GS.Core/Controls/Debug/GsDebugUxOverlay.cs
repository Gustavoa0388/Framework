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
    ///
    /// RESPONSABILIDADE:
    /// - Exibir estados internos de UX de forma visual
    /// - Apoiar debug, suporte e auditoria
    ///
    /// NÃO FAZ:
    /// - Não decide fluxo
    /// - Não executa ações
    /// - Não captura exceções
    /// - Não persiste dados
    /// - Não interfere na UI
    ///
    /// MODO:
    /// - Opt-in
    /// - Somente leitura
    /// </summary>
    public sealed partial class GsDebugUxOverlay : UserControl
    {
        // =====================================================
        // CAMPOS INTERNOS (estado observado)
        // =====================================================

        private GsUxState _currentUxState = GsUxState.Hidden;
        private bool _stateViewVisible;
        private GsUxState? _stateViewState;

        private bool _busyActive;
        private bool _skeletonActive;

        private GsFormResultType? _lastFormResult;

        private bool _diagnosticsAllowed;
        private bool _hasTechnicalDetails;

        // =====================================================
        // CONTROLES VISUAIS
        // =====================================================

        private readonly Label lblHeader;
        private readonly Label lblUxState;
        private readonly Label lblStateView;
        private readonly Label lblBusy;
        private readonly Label lblSkeleton;
        private readonly Label lblNavigation;
        private readonly Label lblDiagnostics;

        // =====================================================
        // PROPRIEDADES PÚBLICAS (somente leitura)
        // =====================================================

        public bool IsEnabled => Visible;

        public GsUxState CurrentUxState => _currentUxState;

        public bool IsStateViewVisible => _stateViewVisible;

        public GsUxState? StateViewState => _stateViewState;

        public bool IsBusyActive => _busyActive;

        public bool IsSkeletonActive => _skeletonActive;

        public GsFormResultType? LastFormResult => _lastFormResult;

        public bool DiagnosticsAllowed => _diagnosticsAllowed;

        public bool HasTechnicalDetails => _hasTechnicalDetails;

        // =====================================================
        // EVENTOS
        // =====================================================

        public event EventHandler VisibilityChanged;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        public GsDebugUxOverlay()
        {
            // Overlay flutuante
            Dock = DockStyle.None;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Size = new Size(260, 200);
            BackColor = Color.FromArgb(230, 30, 30, 30); // escuro semi-transparente
            ForeColor = Color.White;
            Visible = false;

            // Header
            lblHeader = new Label
            {
                Text = "DEBUG UX",
                Dock = DockStyle.Top,
                Height = 24,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            // Labels de estado
            lblUxState = CreateLineLabel();
            lblStateView = CreateLineLabel();
            lblBusy = CreateLineLabel();
            lblSkeleton = CreateLineLabel();
            lblNavigation = CreateLineLabel();
            lblDiagnostics = CreateLineLabel();

            var content = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(8),
                AutoScroll = false
            };

            content.Controls.Add(lblUxState);
            content.Controls.Add(lblStateView);
            content.Controls.Add(lblBusy);
            content.Controls.Add(lblSkeleton);
            content.Controls.Add(lblNavigation);
            content.Controls.Add(lblDiagnostics);

            Controls.Add(content);
            Controls.Add(lblHeader);

            AtualizarVisual();
        }

        // =====================================================
        // API PÚBLICA — CONTROLE DE VISIBILIDADE
        // =====================================================

        public void Enable()
        {
            if (Visible)
                return;

            Visible = true;
            VisibilityChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Disable()
        {
            if (!Visible)
                return;

            Visible = false;
            VisibilityChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Toggle()
        {
            if (Visible)
                Disable();
            else
                Enable();
        }

        // =====================================================
        // API PÚBLICA — ATUALIZAÇÃO DE ESTADO (push explícito)
        // =====================================================

        public void UpdateUxState(GsUxState state)
        {
            _currentUxState = state;
            AtualizarVisual();
        }

        public void UpdateStateView(GsUxState state, bool isVisible)
        {
            _stateViewState = state;
            _stateViewVisible = isVisible;
            AtualizarVisual();
        }

        public void UpdateBusyState(bool isActive)
        {
            _busyActive = isActive;
            AtualizarVisual();
        }

        public void UpdateSkeletonState(bool isActive)
        {
            _skeletonActive = isActive;
            AtualizarVisual();
        }

        public void UpdateFormResult(GsFormResultType resultType)
        {
            _lastFormResult = resultType;
            AtualizarVisual();
        }

        public void UpdateDiagnostics(bool allowDiagnostics, bool hasTechnicalDetails)
        {
            _diagnosticsAllowed = allowDiagnostics;
            _hasTechnicalDetails = hasTechnicalDetails;
            AtualizarVisual();
        }

        // =====================================================
        // VISUAL
        // =====================================================

        private void AtualizarVisual()
        {
            lblUxState.Text = $"UX State: {_currentUxState}";
            lblStateView.Text = $"StateView: {(IsStateViewVisible ? _stateViewState?.ToString() : "Hidden")}";
            lblBusy.Text = $"Busy: {(_busyActive ? "Active" : "Inactive")}";
            lblSkeleton.Text = $"Skeleton: {(_skeletonActive ? "Active" : "Inactive")}";
            lblNavigation.Text = $"FormResult: {_lastFormResult?.ToString() ?? "None"}";
            lblDiagnostics.Text =
                $"Diagnostics: {(_diagnosticsAllowed ? "Allowed" : "Blocked")} | Details: {(_hasTechnicalDetails ? "Yes" : "No")}";
        }

        private static Label CreateLineLabel()
        {
            return new Label
            {
                AutoSize = false,
                Height = 20,
                Width = 220,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8F),
                Margin = new Padding(0, 2, 0, 2)
            };
        }
    }
}
