using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Filtro genérico para uma propriedade específica de um item.
    /// </summary>
    public class GsGridFilter<T>
    {
        public string PropertyName { get; }
        public GsGridFilterType FilterType { get; }
        public bool IgnoreCase { get; set; } = true;

        private readonly PropertyInfo _property;

        public GsGridFilter(string propertyName, GsGridFilterType filterType)
        {
            PropertyName = propertyName;
            FilterType = filterType;

            _property = typeof(T).GetProperty(propertyName)
                ?? throw new InvalidOperationException(
                    $"Propriedade '{propertyName}' não encontrada em {typeof(T).Name}");
        }

        // =============================
        // USO INDIVIDUAL (filtro simples)
        // =============================
        public IEnumerable<T> Apply(IEnumerable<T> source, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return source;

            return source.Where(item => Match(item, filterText));
        }

        // =============================
        // MATCH USADO PELO GRUPO
        // =============================
        public bool Match(T item, string filterText)
        {
            if (item == null || string.IsNullOrWhiteSpace(filterText))
                return false;

            var value = _property.GetValue(item);
            if (value == null)
                return false;

            var text = value.ToString();
            return MatchText(text, filterText);
        }

        // =============================
        // COMPARAÇÃO DE TEXTO
        // =============================
        private bool MatchText(string source, string filter)
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
