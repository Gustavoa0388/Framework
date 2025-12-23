using GS.Core.UI.Controls.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Filtro genérico aplicado a uma propriedade específica de um item.
    /// 
    /// OBSERVAÇÕES IMPORTANTES:
    /// - O valor da propriedade é convertido para string para comparação.
    /// - Não há conversão de tipo (datas, números, enums viram texto).
    /// - Indicado para grids simples e médios.
    /// </summary>
    public class GsGridFilter<T>
    {
        public string PropertyName { get; }
        public GridFilterType FilterType { get; }
        public bool IgnoreCase { get; set; } = true;

        private readonly PropertyInfo _property;

        public GsGridFilter(string propertyName, GridFilterType filterType)
        {
            PropertyName = propertyName;
            FilterType = filterType;

            _property = typeof(T).GetProperty(propertyName)
                ?? throw new InvalidOperationException(
                    $"Propriedade '{propertyName}' não encontrada em {typeof(T).Name}");
        }

        /// <summary>
        /// Aplica o filtro individualmente a uma coleção.
        /// </summary>
        public IEnumerable<T> Apply(IEnumerable<T> source, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return source;

            return source.Where(item => Match(item, filterText));
        }

        /// <summary>
        /// Verifica se um item atende ao critério de filtro.
        /// Usado internamente por grupos de filtro.
        /// </summary>
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

        private bool MatchText(string source, string filter)
        {
            if (IgnoreCase)
            {
                source = source.ToLower();
                filter = filter.ToLower();
            }

            return FilterType switch
            {
                GridFilterType.Contains => source.Contains(filter),
                GridFilterType.Equals => source == filter,
                GridFilterType.StartsWith => source.StartsWith(filter),
                GridFilterType.EndsWith => source.EndsWith(filter),
                _ => false
            };
        }
    }
}
