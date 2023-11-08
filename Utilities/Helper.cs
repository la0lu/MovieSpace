namespace MovieSpace.Utilities
{
    public class Helper
    {
        public static PaginatorResponseDto<IEnumerable<T>> Paginate<T>(IQueryable<T> query, int pageNum, int pageSize)
        {
            if(pageNum <= 0) pageNum = 1;
            if(pageSize <= 0) pageSize = 10;

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (pageNum > totalPages) pageNum = totalPages;

            var skipAmount = (pageNum - 1) * pageSize;
            var paginatedItems = query.Skip(skipAmount).Take(pageSize).ToList();

            return new PaginatorResponseDto<IEnumerable<T>>
            {
                PageItems = paginatedItems,
                PageSize = pageSize,
                CurrentPage = pageNum,
                NumberOfPages = totalPages,
                TotalCount = totalCount,
                PreviousPage = pageNum > 1 ? pageNum - 1 : null,
                NextPage = totalPages == pageNum ? null : pageNum + 1
            };
        }
    }
}
