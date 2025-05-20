namespace Imprevis.Dataverse.Plugins.Requests
{
    using Imprevis.Dataverse.Plugins.Extensions;
    using Microsoft.Xrm.Sdk.Query;
    using System;

    internal class GetUserTimeZoneInfo : IDataverseRequest<TimeZoneInfo>
    {
        public TimeZoneInfo Execute(IDataverseService service, ILoggingService logger)
        {
            var query = new QueryExpression("timezonedefinition");
            query.ColumnSet.AddColumn("standardname");

            var link = query.AddLink("usersettings", "timezonecode", "timezonecode");
            link.LinkCriteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);

            var entity = service.RetrieveSingle(query);
            if (entity == null)
            {
                throw new Exception("Unable to retrieve time zone information for user.");
            }

            var timeZoneId = entity.GetAttributeValue<string>("standardname");

            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
    }
}
