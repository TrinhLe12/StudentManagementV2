using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementV2.Core.PaginatedLists
{
    public class PaginatedList<T> : List<T> where T : class
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList () { }

        public PaginatedList (List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling ((double) count / pageSize);

            // Add items of List to the newly created PaginatedList
            this.AddRange(items);
        }

        public PaginatedList(List<T> items, int totalPages, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;

            // Add items of List to the newly created PaginatedList
            this.AddRange(items);
        }

        // This is method HasPreviousPage
        public bool HasPreviousPage => PageIndex > 1;

        // This is method HasNextPage
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create (IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T> (items, count, pageIndex, pageSize);
        }
    }
}
