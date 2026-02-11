namespace Imprevis.Dataverse.Plugins.Requests;

using Imprevis.Dataverse.Plugins.Extensions;
using Microsoft.Xrm.Sdk.Query;
using System;

internal class GetUserTimeZoneInfo(Guid userId) : IDataverseCachedRequest<TimeZoneInfo>
{
    public string CacheKey => $"TimeZone_{userId}";

    public TimeSpan CacheDuration => TimeSpan.FromDays(1);

    public TimeZoneInfo Execute(IDataverseService service, ILoggingService logger)
    {
        var query = new QueryExpression("timezonedefinition");
        query.ColumnSet.AddColumn("standardname");

        var link = query.AddLink("usersettings", "timezonecode", "timezonecode");
        link.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, userId);

        var entity = service.RetrieveSingle(query);
        if (entity == null)
        {
            throw new Exception("Unable to retrieve time zone information for user.");
        }

        var timeZoneId = entity.GetAttributeValue<string>("standardname");

        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
}
