using System;

namespace GS.Core.UI.Controls.Data
{
    /// <summary>
    /// Argumentos do evento de mudança de página.
    /// </summary>
    public sealed class GsPageChangedEventArgs : EventArgs
    {
        public GsPageChangedEventArgs(int page, int pageSize, int totalPages)
        {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
    }
}
