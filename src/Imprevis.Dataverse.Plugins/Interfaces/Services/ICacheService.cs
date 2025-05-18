namespace Imprevis.Dataverse.Plugins
{
    using System;

    public interface ICacheService
    {
        void Clear();
        T? Get<T>(string key) where T : struct;
        T GetOrAdd<T>(string key, T value, TimeSpan duration) where T : struct;
        T GetOrAdd<T>(string key, Func<T> factory, TimeSpan duration) where T : struct;
        void Remove(string key);
        void Set<T>(string key, T value, TimeSpan duration) where T : struct;
    }
}
