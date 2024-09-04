using PlanIt.Domain.Models;

namespace PlanIt.WebApi.Common.Mapping;

public static class IdMapping
{
    public static string MapToResponse(this IdValueObject id) => id.Value.ToString();
    public static string MapToResponse(this AggregateRootId<Guid> id) => id.Value.ToString();
    public static IReadOnlyList<string> MapIdsToStrings(IReadOnlyList<IdValueObject> listOfIds) => listOfIds.Select( id => id.MapToResponse() ).ToList();
    public static IReadOnlyList<string> MapIdsToStrings(IReadOnlyList<AggregateRootId<Guid>> listOfIds) => listOfIds.Select( id => id.MapToResponse() ).ToList();
}