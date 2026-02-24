namespace Application.Common.Pagination;

public class PagedResults<T>
{
    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    
    public PagedResults(IReadOnlyList<T> items, int totalCount, int currentPage, int pageSize) {
        Items = items;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}