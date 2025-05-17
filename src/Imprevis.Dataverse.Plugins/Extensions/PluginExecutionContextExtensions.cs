namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Extensions;
    using System.Linq;

    public static class PluginExecutionContextExtensions
    {
        public static TRequest GetRequest<TRequest>(this IPluginExecutionContext context) where TRequest : OrganizationRequest
        {
            return context.InputParameters as TRequest;
        }

        public static Entity GetTarget(this IPluginExecutionContext context)
        {
            return context.InputParameterOrDefault<Entity>("Target");
        }

        public static EntityReference GetTargetReference(this IPluginExecutionContext context)
        {
            var reference = context.InputParameterOrDefault<EntityReference>("Target");
            if (reference != null)
            {
                return reference;
            }

            return context.GetTarget().ToEntityReference();
        }

        public static Entity GetPreImage(this IPluginExecutionContext context)
        {
            return context.PreEntityImages.FirstOrDefault().Value;
        }

        public static Entity GetPostImage(this IPluginExecutionContext context)
        {
            return context.PostEntityImages.FirstOrDefault().Value;
        }
    }
}
