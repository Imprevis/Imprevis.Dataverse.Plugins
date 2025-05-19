namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Extensions;
    using System;
    using System.Linq;

    public static class PluginExecutionContextExtensions
    {
        public static TRequest GetRequest<TRequest>(this IPluginExecutionContext context) where TRequest : OrganizationRequest
        {
            return context.InputParameters as TRequest;
        }

        public static Entity GetTarget(this IPluginExecutionContext context)
        {
            if (!context.InputParameters.ContainsKey("Target"))
            {
                throw new Exception("Target is not present in InputParameters.");
            }

            var target = context.InputParameterOrDefault<Entity>("Target");
            if (target == null)
            {
                throw new Exception("Target is not an Entity object.");
            }

            return target;
        }

        public static T GetTarget<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetTarget(context).ToEntity<T>();
        }

        public static EntityReference GetTargetReference(this IPluginExecutionContext context)
        {
            if (!context.InputParameters.ContainsKey("Target"))
            {
                throw new Exception("Target is not present in InputParameters.");
            }

            var target = context.InputParameterOrDefault<EntityReference>("Target");
            if (target != null)
            {
                return target;
            }

            return context.GetTarget().ToEntityReference();
        }

        public static Entity GetPreImage(this IPluginExecutionContext context)
        {
            return context.PreEntityImages.FirstOrDefault().Value;
        }

        public static T GetPreImage<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetPreImage(context).ToEntity<T>();
        }

        public static Entity GetPostImage(this IPluginExecutionContext context)
        {
            return context.PostEntityImages.FirstOrDefault().Value;
        }

        public static T GetPostImage<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetPostImage(context).ToEntity<T>();
        }

        public static bool IsAsynchronous(this IPluginExecutionContext context)
        {
            return context.Mode == Mode.Asynchronous;
        }

        public static bool IsSynchronous(this IPluginExecutionContext context)
        {
            return context.Mode == Mode.Synchronous;
        }
    }
}
