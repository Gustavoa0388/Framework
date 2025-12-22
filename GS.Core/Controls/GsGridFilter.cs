using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Filtro genérico para listas exibidas em GsDataGridView.
    /// </summary>
    public class GsGridFilter<T>
    {
        public string PropertyName { get; }
        public GsGridFilterType FilterType { get; }

        public bool IgnoreCase { get; set; } = true;

        public GsGridFilter(string propertyName, GsGridFilterType filterType)
        {
            PropertyName = propertyName;
            FilterType = filterType;
        }

        /// <summary>
        /// Aplica o filtro sobre a lista informada.
        /// </summary>
        public IEnumerable<T> Apply(IEnumerable<T> source, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return source;

            var prop = typeof(T).GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
                throw new InvalidOperationException($"Propriedade '{PropertyName}' não encontrada em {typeof(T).Name}");

            return source.Where(item =>
            {
                var value = prop.GetValue(item);
                if (value == null)
                    return false;

                var text = value.ToString();
                return Match(text, filterText);
            });
        }

        // =============================
        // COMPARAÇÃO
        // =============================
        private bool Match(string source, string filter)
        {
            if (IgnoreCase)
            {
                source = source.ToLower();
                filter = filter.ToLower();
            }

            return FilterType switch
            {
                GsGridFilterType.Contains => source.Contains(filter),
                GsGridFilterType.Equals => source == filter,
                GsGridFilterType.StartsWith => source.StartsWith(filter),
                GsGridFilterType.EndsWith => source.EndsWith(filter),
                _ => false
            };
        }
    }
}
