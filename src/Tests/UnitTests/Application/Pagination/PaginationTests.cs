using Application.Clients.Queries.GetAllClients;
using Application.Common;
using FluentAssertions;

namespace Tests.UnitTests.Application.Pagination;

public class PaginationTests
{
    [Fact]
    public void Should_limit_page_size_to_max()
    {
        var query = new GetAllClientsQuery
        {
            PageSize = 9999
        };
        
        query.PageSize.Should().Be(Constants.MAX_PAGE_SIZE);
    }
    
    [Fact]
    public void Should_calculate_skip_correctly()
    {
        var query = new GetAllClientsQuery
        {
            CurrentPage = 3,
            PageSize = 10
        };

        query.Skip.Should().Be(20);
    }
}