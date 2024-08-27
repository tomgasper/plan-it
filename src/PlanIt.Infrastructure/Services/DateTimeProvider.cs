using PlanIt.Application.Common.Interfaces.Services;

namespace PlanIt.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}