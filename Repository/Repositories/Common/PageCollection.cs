using System.Collections.Generic;

namespace Data.Repositories.Common
{
    public class PageCollection<TData>
    {
        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }

        public List<TData> Data { get; set; }
    }
}
