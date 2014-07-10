using System;
using System.Collections.Generic;
using System.Linq;

namespace Lxs.Core
{
    [Serializable]
    public class PagedList<T>: List<T>,IPagedList<T>
    {
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            TotalCount = total;
            TotalPages = total/pageSize;

            if (total%pageSize >0)
            {
                TotalPages++;
            }

            PageIndex = pageIndex;
            PageSize = pageSize;
            this.AddRange(source.Skip(pageIndex * PageSize).Take(pageSize).ToList());
        }
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
            {
                TotalPages++;
            }

            PageIndex = pageIndex;
            PageSize = pageSize;
            this.AddRange(source.Skip(pageIndex * PageSize).Take(pageSize).ToList());
        }
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source);
        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
