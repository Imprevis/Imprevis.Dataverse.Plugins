namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Extensions;
    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for the <see cref="IPluginExecutionContext"/> interface.
    /// </summary>
    public static class PluginExecutionContextExtensions
    {
        /// <summary>
        /// Gets an OrganizationRequest from the InputParameters of the plugin context.
        /// </summary>
        public static TRequest GetRequest<TRequest>(this IPluginExecutionContext context) where TRequest : OrganizationRequest
        {
            return context.InputParameters as TRequest;
        }

        /// <summary>
        /// Gets an OrganizationResponse from the OutputParameters of the plugin context.
        /// </summary>
        public static TResponse GetResponse<TResponse>(this IPluginExecutionContext context) where TResponse : OrganizationResponse
        {
            return context.OutputParameters as TResponse;
        }

        /// <summary>
        /// Sets the OutputParameters of the plugin context based on the Results of an OrganizationResponse.
        /// </summary>
        public static void SetResponse<TResponse>(this IPluginExecutionContext context, TResponse response) where TResponse : OrganizationResponse
        {
            foreach (var parameter in response.Results)
            {
                if (context.OutputParameters.ContainsKey(parameter.Key))
                {
                    context.OutputParameters[parameter.Key] = parameter.Value;
                }
                else
                {
                    context.OutputParameters.Add(parameter.Key, parameter.Value);
                }
            }
        }

        /// <summary>
        /// Gets the target entity from the InputParameters of the plugin context.
        /// </summary>
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

        /// <summary>
        /// Gets the target entity from the InputParameters of the plugin context and converts it to the specified type.
        /// </summary>
        public static T GetTarget<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetTarget(context).ToEntity<T>();
        }

        /// <summary>
        /// Gets the target entity from the InputParameters of the plugin context and converts it to the specified type.
        /// </summary>
        public static T GetTargetWithPreImage<T>(this IPluginExecutionContext context) where T : Entity
        {
            return GetTarget(context).Coalesce(GetPreImage(context)).ToEntity<T>();
        }


        /// <summary>
        /// Gets the target entity from the InputParameters of the plugin context and converts it to the specified type.
        /// </summary>
        public static T GetTargetWithPostImage<T>(this IPluginExecutionContext context) where T : Entity
        {
            return GetTarget(context).Coalesce(GetPostImage(context)).ToEntity<T>();
        }

        /// <summary>
        /// Gets the target entity reference from the InputParameters of the plugin context.
        /// </summary>
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

        /// <summary>
        /// Gets the pre-image Entity from the plugin context.
        /// </summary>
        public static Entity GetPreImage(this IPluginExecutionContext context)
        {
            return context.PreEntityImages.FirstOrDefault().Value;
        }

        /// <summary>
        /// Gets the pre-image Entity from the plugin context and converts it to the specified type.
        /// </summary>
        public static T GetPreImage<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetPreImage(context)?.ToEntity<T>();
        }

        /// <summary>
        /// Gets the post-image Entity from the plugin context.
        /// </summary>
        public static Entity GetPostImage(this IPluginExecutionContext context)
        {
            return context.PostEntityImages.FirstOrDefault().Value;
        }

        /// <summary>
        /// Gets the post-image Entity from the plugin context and converts it to the specified type.
        /// </summary>
        public static T GetPostImage<T>(this IPluginExecutionContext context) where T: Entity
        {
            return GetPostImage(context)?.ToEntity<T>();
        }

        /// <summary>
        /// Checks if the plugin is running in asynchronous mode.
        /// </summary>
        public static bool IsAsynchronous(this IPluginExecutionContext context)
        {
            return context.Mode == Mode.Asynchronous;
        }

        /// <summary>
        /// Checks if the plugin is running in synchronous mode.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsSynchronous(this IPluginExecutionContext context)
        {
            return context.Mode == Mode.Synchronous;
        }
    }
}
