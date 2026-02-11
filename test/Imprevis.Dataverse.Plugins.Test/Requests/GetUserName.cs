namespace Imprevis.Dataverse.Plugins.Test.Requests;

using Imprevis.Dataverse.Plugins;
using Imprevis.Dataverse.Plugins.Extensions;
using Microsoft.Xrm.Sdk.Query;

public class GetUserName : IDataverseRequest<string>
{
    public string Execute(IDataverseService service, ILoggingService logger)
    {
        var query = new QueryExpression("systemuser");
        query.Criteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);
        query.ColumnSet.AddColumn("fullname");

        var user = service.RetrieveSingle(query);

        return user.GetAttributeValue<string>("fullname");
    }
}
