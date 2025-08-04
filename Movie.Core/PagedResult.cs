using System.Collections.Generic;

namespace Movie.Core
{
    /// <summary>
    /// Generic container used when returning paginated results from the service layer.
    /// The <see cref="Items"/> collection contains the current page of results while
    /// accompanying metadata describes total result set size and pagination information.
    /// </summary>
    /// <typeparam name="T">Type of the items being returned.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Items contained on the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = new List<T>();
        /// <summary>
        /// Total number of items across all pages.
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Current page number (1-based).
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; }
    }
}