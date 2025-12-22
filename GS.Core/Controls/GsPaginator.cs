using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Controla paginação de listas genéricas.
    /// </summary>
    public class GsPaginator<T>
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; private set; } = 1;
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }

        private List<T> _source = new();

        public void SetSource(IEnumerable<T> source)
        {
            _source = source?.ToList() ?? new List<T>();
            TotalItems = _source.Count;
            TotalPages = Math.Max(1, (int)Math.Ceiling((double)TotalItems / PageSize));
            CurrentPage = 1;
        }

        public IEnumerable<T> GetCurrentPage()
        {
            return _source
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);
        }

        public void Next()
        {
            if (CurrentPage < TotalPages)
                CurrentPage++;
        }

        public void Previous()
        {
            if (CurrentPage > 1)
                CurrentPage--;
        }

        public void First()
        {
            CurrentPage = 1;
        }

        public void Last()
        {
            CurrentPage = TotalPages;
        }
    }
}
