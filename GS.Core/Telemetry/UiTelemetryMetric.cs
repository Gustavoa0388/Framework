namespace GS.Core.UI.Telemetry
{
    /// <summary>
    /// Métricas oficiais de UI Telemetria Manual do GS Core UI.
    ///
    /// REPRESENTA:
    /// - Eventos explícitos de infraestrutura de UX
    ///
    /// NÃO REPRESENTA:
    /// - Ações de usuário
    /// - Métricas de tempo
    /// - Telemetria automática
    /// </summary>
    public enum UiTelemetryMetric
    {
        // =====================================================
        // UX STATE
        // =====================================================

        UxStateChanged,
        EmptyStateShown,
        ErrorStateShown,
        SuccessStateShown,
        DisabledStateShown,

        // =====================================================
        // BUSY / SKELETON
        // =====================================================

        BusyActivated,
        BusyDeactivated,
        SkeletonActivated,
        SkeletonDeactivated,

        // =====================================================
        // AÇÕES
        // =====================================================

        RetryRequested,

        // =====================================================
        // NAVEGAÇÃO
        // =====================================================

        FormResultDefined,
        FormClosed,

        // =====================================================
        // DIAGNÓSTICO
        // =====================================================

        DiagnosticsOpened,
        DebugUxEnabled,
        DebugUxDisabled
    }
}
