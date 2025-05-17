namespace Imprevis.Dataverse.Plugins
{
    using System;

    public interface IDateTimeService
    {
        /// <summary>
        /// Get the current time in UTC.
        /// </summary>
        /// <returns></returns>
        DateTime Get();

        /// <summary>
        /// Get the current time in the user's local time zone.
        /// </summary>
        /// <returns></returns>
        DateTime GetUserLocal();
    }
}
