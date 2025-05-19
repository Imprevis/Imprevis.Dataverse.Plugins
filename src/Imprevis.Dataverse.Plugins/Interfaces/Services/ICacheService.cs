namespace Imprevis.Dataverse.Plugins
{
    using System;

    /// <summary>
    /// Interface containing methods for interacting with a cache.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Get a value from the cache.
        /// </summary>
        /// <typeparam name="T">Type of the cached value.</typeparam>
        /// <param name="key">Key of the cache item.</param>
        /// <returns>The value from the cache or null if it doesn't exist or is expired.</returns>
        T? Get<T>(string key) where T : struct;

        /// <summary>
        /// Get a value from the cache or add it if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">Type of the cached value.</typeparam>
        /// <param name="key">Key of the cache item.</param>
        /// <param name="value">Value to cache if the key doesn't exist already.</param>
        /// <param name="duration">Duration to cache the value.</param>
        /// <returns>The value from the cache.</returns>
        T GetOrAdd<T>(string key, T value, TimeSpan duration) where T : struct;

        /// <summary>
        /// Get a value from the cache or add it if it doesn't exist by calling the factory function.
        /// </summary>
        /// <typeparam name="T">Type of the cached value.</typeparam>
        /// <param name="key">Key of the cache item.</param>
        /// <param name="factory">Function to execute to get the value if the key doesn't exist alraedy.</param>
        /// <param name="duration">Duration to cache the value.</param>
        /// <returns>The value from the cache.</returns>
        T GetOrAdd<T>(string key, Func<T> factory, TimeSpan duration) where T : struct;

        /// <summary>
        /// Remove a value from the cache.
        /// </summary>
        /// <param name="key">Key of the cache item.</param>
        void Remove(string key);

        /// <summary>
        /// Set a value in the cache with a specific duration.
        /// </summary>
        /// <typeparam name="T">Type of the value to cache.</typeparam>
        /// <param name="key">Key of the cache item.</param>
        /// <param name="value">Value to cache.</param>
        /// <param name="duration">Duration to cache the value.</param>
        void Set<T>(string key, T value, TimeSpan duration) where T : struct;
    }
}
