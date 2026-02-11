namespace Imprevis.Dataverse.Plugins.Extensions;

using Microsoft.Xrm.Sdk;
using System;

/// <summary>
/// Extensions for the DataCollection class.
/// </summary>
public static class DataCollectionExtensions
{
    /// <summary>
    /// Gets a parameter from a ParameterCollection, returning null if the parameter doesn't exist.
    /// </summary>
    public static object? Get(this DataCollection<string, object> parameters, string parameterName)
    {
        if (parameters.TryGetValue(parameterName, out var value))
        {
            return value;
        }

        return default;
    }

    /// <summary>
    /// Gets a parameter from a ParameterCollection, returning a default value if the parameter doesn't exist.
    /// </summary>
    public static TResult? Get<TResult>(this DataCollection<string, object> parameters, string parameterName, TResult? defaultValue = default)
    {
        if (parameters.TryGetValue(parameterName, out var value))
        {
            return (TResult)value;
        }

        return defaultValue;
    }

    /// <summary>
    /// Gets a parameter from a ParameterCollection, runnig it through the parser method to transform the result.
    /// </summary>
    public static TResult? Get<TResult>(this DataCollection<string, object> parameters, string parameterName, Func<object, TResult?> parser)
    {
        if (parameters.TryGetValue(parameterName, out var value))
        {
            return parser(value);
        }

        return default;
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
