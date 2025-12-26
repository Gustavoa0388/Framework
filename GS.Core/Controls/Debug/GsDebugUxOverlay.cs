using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.UX;
using GS.Core.UI.Forms.Navigation;
using GS.Core.UI.Telemetry;
using System.Collections.Generic;


namespace GS.Core.UI.Controls.Debug
{
    /// <summary>
    /// GsDebugUxOverlay
    ///
    /// Overlay visual flutuante para observabilidade de UX no GS Core UI.
    ///
    /// CARACTERÍSTICAS:
    /// - Uso estritamente técnico (Debug / Diagnóstico)
    /// - Totalmente opt-in
    /// - Somente leitura
    /// - Nenhuma interferência no fluxo da UI
    ///
    /// RESPONSABILIDADES:
    /// - Exibir estado atual da sessão (UX, Busy, Skeleton, Navegação)
    /// - Exibir snapshot de UI Telemetria Manual de forma organizada
    ///
    /// NÃO FAZ:
    /// - Não persiste dados
    /// - Não executa lógica
    /// - Não altera métricas
    /// - Não automatiza decisões
    /// </summary>
    public sealed partial class GsDebugUxOverlay : UserControl
    {
        // =====================================================
        // CAMPOS INTERNOS (ESTADO OBSERVADO)
        // =====================================================
        // Estes campos representam apenas uma cópia do estado
        // atual da sessão. Nenhum deles é fonte de verdade.

        private GsUxState _currentUxState = GsUxState.Hidden;
        private bool _busyActive;
        private bool _skeletonActive;
        private GsFormResultType _lastFormResult = GsFormResultType.None;
        private bool _diagnosticsAllowed;
        private bool _hasTechnicalDetails;

        /// <summary>
        /// Snapshot somente leitura da UI Telemetria Manual.
        /// Sempre recebido pronto via GsBaseForm.
        /// </summary>
        private IReadOnlyDictionary<UiTelemetryMetric, int> _telemetrySnapshot;

        // =====================================================
        // CONTROLES VISUAIS — INFORMAÇÕES DE SESSÃO
        // =====================================================

        private readonly Label lblHeader;
        private readonly Label lblUxState;
        private readonly Label lblBusy;
        private readonly Label lblSkeleton;
        private readonly Label lblNavigation;
        private readonly Label lblDiagnostics;

        // =====================================================
        // PAINEL DIAGNÓSTICO — TELEMETRIA
        // =====================================================
        // Responsável apenas por exibir métricas de forma
        // organizada e legível para diagnóstico humano.

        private readonly Label lblTelemetryHeader;
        private readonly FlowLayoutPanel telemetryPanel;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        /// <summary>
        /// Inicializa o overlay de Debug UX.
        /// Invisível por padrão e controlado via EnableDebugUx.
        /// </summary>
        public GsDebugUxOverlay()
        {
            Dock = DockStyle.None;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Size = new Size(260, 180);
            BackColor = Color.FromArgb(230, 30, 30, 30);
            ForeColor = Color.White;
            Visible = false;

            // Cabeçalho principal
            lblHeader = new Label
            {
                Text = "DEBUG UX",
                Dock = DockStyle.Top,
                Height = 24,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            // Linhas de estado da sessão
            lblUxState = CreateLineLabel();
            lblBusy = CreateLineLabel();
            lblSkeleton = CreateLineLabel();
            lblNavigation = CreateLineLabel();
            lblDiagnostics = CreateLineLabel();

            // Container vertical principal
            var content = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(8)
            };

            // Cabeçalho da seção de telemetria
            lblTelemetryHeader = new Label
            {
                Text = "TELEMETRIA (UI)",
                Height = 22,
                Width = 220,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.Gainsboro,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 8, 0, 2)
            };

            // Painel que recebe as métricas dinamicamente
            telemetryPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Width = 220
            };

            // Montagem do layout
            content.Controls.Add(lblUxState);
            content.Controls.Add(lblBusy);
            content.Controls.Add(lblSkeleton);
            content.Controls.Add(lblNavigation);
            content.Controls.Add(lblDiagnostics);
            content.Controls.Add(lblTelemetryHeader);
            content.Controls.Add(telemetryPanel);

            Controls.Add(content);
            Controls.Add(lblHeader);

            AtualizarVisual();
        }

        // =====================================================
        // API PÚBLICA — SINCRONIZAÇÃO
        // =====================================================

        /// <summary>
        /// Atualiza o estado técnico atual da sessão.
        /// Chamado exclusivamente pelo GsBaseForm.
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

        /// <summary>
        /// Recebe snapshot somente leitura da UI Telemetria Manual.
        /// Nenhuma modificação é realizada neste método.
        /// </summary>
        public void UpdateTelemetry(IReadOnlyDictionary<UiTelemetryMetric, int> snapshot)
        {
            _telemetrySnapshot = snapshot;
            AtualizarVisual();
        }

        // =====================================================
        // RENDERIZAÇÃO DA TELEMETRIA (READ-ONLY)
        // =====================================================

        /// <summary>
        /// Renderiza o painel de telemetria agrupado por categoria.
        /// </summary>
        private void RenderTelemetry()
        {
            telemetryPanel.Controls.Clear();

            if (_telemetrySnapshot == null || _telemetrySnapshot.Count == 0)
            {
                telemetryPanel.Controls.Add(
                    CreateTelemetryLine("Telemetria desabilitada ou vazia")
                );
                return;
            }

            AddTelemetryGroup(
                "UX STATE",
                UiTelemetryMetric.UxStateChanged,
                UiTelemetryMetric.EmptyStateShown,
                UiTelemetryMetric.ErrorStateShown,
                UiTelemetryMetric.SuccessStateShown,
                UiTelemetryMetric.DisabledStateShown
            );

            AddTelemetryGroup(
                "BUSY / SKELETON",
                UiTelemetryMetric.BusyActivated,
                UiTelemetryMetric.BusyDeactivated,
                UiTelemetryMetric.SkeletonActivated,
                UiTelemetryMetric.SkeletonDeactivated
            );

            AddTelemetryGroup(
                "NAVEGAÇÃO",
                UiTelemetryMetric.FormResultDefined,
                UiTelemetryMetric.FormClosed
            );

            AddTelemetryGroup(
                "DIAGNÓSTICO",
                UiTelemetryMetric.DiagnosticsOpened,
                UiTelemetryMetric.DebugUxEnabled,
                UiTelemetryMetric.DebugUxDisabled
            );
        }

        /// <summary>
        /// Adiciona um grupo de métricas ao painel.
        /// </summary>
        private void AddTelemetryGroup(string title, params UiTelemetryMetric[] metrics)
        {
            telemetryPanel.Controls.Add(
                CreateTelemetryLine(title, bold: true)
            );

            foreach (var metric in metrics)
            {
                var value = _telemetrySnapshot.TryGetValue(metric, out var count)
                    ? count
                    : 0;

                telemetryPanel.Controls.Add(
                    CreateTelemetryLine($"- {metric}: {value}")
                );
            }
        }

        /// <summary>
        /// Cria uma linha de texto simples para exibição de telemetria.
        /// </summary>
        private static Label CreateTelemetryLine(string text, bool bold = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font(
                    "Segoe UI",
                    8F,
                    bold ? FontStyle.Bold : FontStyle.Regular
                ),
                ForeColor = Color.White,
                Margin = new Padding(0, 1, 0, 1)
            };
        }

        // =====================================================
        // ATUALIZAÇÃO VISUAL GERAL
        // =====================================================

        /// <summary>
        /// Atualiza todas as informações exibidas no overlay.
        /// </summary>
        private void AtualizarVisual()
        {
            lblUxState.Text = $"UX State: {_currentUxState}";
            lblBusy.Text = $"Busy: {(_busyActive ? "Active" : "Inactive")}";
            lblSkeleton.Text = $"Skeleton: {(_skeletonActive ? "Active" : "Inactive")}";
            lblNavigation.Text = $"FormResult: {_lastFormResult}";
            lblDiagnostics.Text =
                $"Diagnostics: {(_diagnosticsAllowed ? "Allowed" : "Blocked")} | Details: {(_hasTechnicalDetails ? "Yes" : "No")}";

            // Debug auxiliar (opcional)
            // Mantido apenas para inspeção técnica durante desenvolvimento
            if (_telemetrySnapshot != null && _telemetrySnapshot.Count > 0)
            {
                foreach (var item in _telemetrySnapshot)
                {
                    Console.WriteLine($"[Telemetry] {item.Key}: {item.Value}");
                }
            }

            RenderTelemetry();
        }

        /// <summary>
        /// Cria um label padrão para linhas de estado.
        /// </summary>
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
