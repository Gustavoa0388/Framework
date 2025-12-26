using System;
using System.Collections.Generic;

namespace GS.Core.UI.Telemetry
{
    /// <summary>
    /// GsUiTelemetry
    ///
    /// Infraestrutura de UI Telemetria Manual do GS Core UI.
    ///
    /// RESPONSABILIDADES:
    /// - Manter contadores explícitos de métricas de UX
    /// - Operar exclusivamente em memória
    /// - Respeitar habilitação opt-in
    ///
    /// NÃO FAZ:
    /// - Persistência
    /// - Automação
    /// - Observação implícita
    /// - Decisão de fluxo
    /// </summary>
    public sealed class GsUiTelemetry
    {
        private readonly Dictionary<UiTelemetryMetric, int> _counters =
            new Dictionary<UiTelemetryMetric, int>();

        public bool IsEnabled { get; }

        public GsUiTelemetry(bool enabled)
        {
            IsEnabled = enabled;
        }

        /// <summary>
        /// Incrementa explicitamente uma métrica.
        /// Se a telemetria estiver desabilitada, não faz nada.
        /// </summary>
        public void Increment(UiTelemetryMetric metric)
        {
            if (!IsEnabled)
                return;

            if (_counters.ContainsKey(metric))
                _counters[metric]++;
            else
                _counters[metric] = 1;
        }

        /// <summary>
        /// Retorna o contador atual de uma métrica.
        /// </summary>
        public int GetCount(UiTelemetryMetric metric)
        {
            return _counters.TryGetValue(metric, out var value)
                ? value
                : 0;
        }

        /// <summary>
        /// Retorna um snapshot somente leitura dos contadores.
        /// </summary>
        public IReadOnlyDictionary<UiTelemetryMetric, int> Snapshot()
        {
            return new Dictionary<UiTelemetryMetric, int>(_counters);
        }
    }
}
