namespace Store.Service.Helpers
{
    public class PaginatedResultDto<T>
    {
        public PaginatedResultDto(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;

            TotalPages = (int)Math.Ceiling(((double)totalCount / PageSize));
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}
