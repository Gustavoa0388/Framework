using System.Collections.Generic;
using System.Linq;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Agrupa múltiplos GsGridFilter e aplica como OR ou AND.
    /// </summary>
    public class GsGridFilterGroup<T>
    {
        private readonly List<GsGridFilter<T>> _filters = new();

        public GsGridFilterCombineMode CombineMode { get; set; } =
            GsGridFilterCombineMode.Or;

        public void Add(GsGridFilter<T> filter)
        {
            _filters.Add(filter);
        }

        public IEnumerable<T> Apply(IEnumerable<T> source, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return source;

            if (_filters.Count == 0)
                return source;

            return CombineMode == GsGridFilterCombineMode.Or
                ? ApplyOr(source, filterText)
                : ApplyAnd(source, filterText);
        }

        // =============================
        // OR
        // =============================
        private IEnumerable<T> ApplyOr(IEnumerable<T> source, string text)
        {
            return source.Where(item =>
                _filters.Any(f => f.Match(item, text))
            );
        }

        // =============================
        // AND
        // =============================
        private IEnumerable<T> ApplyAnd(IEnumerable<T> source, string text)
        {
            return source.Where(item =>
                _filters.All(f => f.Match(item, text))
            );
        }
    }
}
