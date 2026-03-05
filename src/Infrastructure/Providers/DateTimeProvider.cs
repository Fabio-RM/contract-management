using Application.Common.Interfaces;

namespace Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get => DateTime.UtcNow; }
}