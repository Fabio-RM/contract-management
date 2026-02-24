namespace Application.Common.Pagination;

public abstract record PagedQuery
{
    private const int MaxPageSize = Constants.MAX_PAGE_SIZE;
    private int _pageSize = Constants.DEFAULT_PAGE_SIZE;
    
    public int CurrentPage { get; init; } = 1;

    public int PageSize
    {
        get => _pageSize;
        init
        {
            if (value <= 0)               _pageSize = Constants.DEFAULT_PAGE_SIZE;
            else if (value > MaxPageSize) _pageSize = MaxPageSize;
            else                          _pageSize = value;
        }
    }
    
    public int Skip => (CurrentPage - 1) * PageSize;
}