namespace Imprevis.Dataverse.Plugins.Requests
{
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Linq;

    public class GetUserTimeZoneInfo : IDataverseRequest<TimeZoneInfo>
    {
        public TimeZoneInfo Execute(IDataverseService service, ILoggingService logger)
        {
            var query = new QueryExpression("timezonedefinition");
            query.ColumnSet.AddColumn("standardname");

            var link = query.AddLink("usersettings", "timezonecode", "timezonecode");
            link.LinkCriteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);

            var result = service.RetrieveMultiple(query);

            var entity = result.Entities.FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("Unable to retrieve time zone information for user.");
            }

            var timeZoneId = entity.GetAttributeValue<string>("standardname");

            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
    }
}
