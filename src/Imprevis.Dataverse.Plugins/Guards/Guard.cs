namespace Imprevis.Dataverse.Plugins.Guards
{
    using System;

    public static class Guard
    {
        public static void IsNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void AreEqual(object value1, object value2, string errorMessage = "Values are not equal.")
        {
            if (!value1.Equals(value2))
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
