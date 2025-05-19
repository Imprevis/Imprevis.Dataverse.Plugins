namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using System;

    /// <summary>
    /// Extensions for OptionSetValue.
    /// </summary>
    public static class OptionSetExtensions
    {
        /// <summary>
        /// Converts an OptionSetValue to an Enum.
        /// </summary>
        public static T ToEnum<T>(this OptionSetValue optionSet) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), optionSet.Value);
        }

        /// <summary>
        /// Converts an Enum to an OptionSetValue.
        /// </summary>
        public static OptionSetValue ToOptionSetValue<T>(this T value) where T: Enum
        {
            return new OptionSetValue(Convert.ToInt32(value));
        }
    }
}
