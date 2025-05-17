namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using System;

    public static class OptionSetExtensions
    {
        public static T ToEnum<T>(this OptionSetValue optionSet) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), optionSet.Value);
        }

        public static OptionSetValue ToOptionSetValue<T>(this T value) where T: Enum
        {
            return new OptionSetValue(Convert.ToInt32(value));
        }
    }
}
