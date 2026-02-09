namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;

    /// <summary>
    /// Extensions for the DataCollection class.
    /// </summary>
    public static class DataCollectionExtensions
    {
        /// <summary>
        /// Gets a parameter from a ParameterCollection, returning null if the parameter doesn't exist.
        /// </summary>
        public static object Get(this DataCollection<string, object> parameters, string parameterName)
        {
            if (parameters.ContainsKey(parameterName))
            {
                return parameters[parameterName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a parameter from a ParameterCollection, returning a default value if the parameter doesn't exist.
        /// </summary>
        public static T Get<T>(this DataCollection<string, object> parameters, string parameterName, T defaultValue = default)
        {
            if (parameters.ContainsKey(parameterName))
            {
                return (T)parameters[parameterName];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Sets a parameter in a ParameterCollection, adding it if it doesn't already exist or updating it if it does.
        /// </summary>
        public static void Set(this DataCollection<string, object> parameters, string parameterName, object value)
        {
            if (parameters.ContainsKey(parameterName))
            {
                parameters[parameterName] = value;
            }
            else
            {
                parameters.Add(parameterName, value);
            }
        }
    }
}
