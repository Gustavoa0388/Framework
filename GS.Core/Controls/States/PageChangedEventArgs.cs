using System;

namespace GS.Core.UI.Controls.States
{
    /// <summary>
    /// Argumentos do evento de mudança de página.
    /// </summary>
    public class PageChangedEventArgs : EventArgs
    {
        public int Page { get; }
        public int PageSize { get; }

        public PageChangedEventArgs(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
