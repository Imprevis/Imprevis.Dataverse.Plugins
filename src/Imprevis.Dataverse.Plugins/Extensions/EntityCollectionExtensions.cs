namespace Imprevis.Dataverse.Plugins.Extensions;

using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extensions for the Entity class.
/// </summary>
public static class EntityCollectionExtensions
{
    /// <summary>
    /// Converts any <see cref="IEnumerable{Entity}"/> into an <see cref="EntityCollection"/>.
    /// </summary>
    /// <remarks>This is useful for returning an EntityCollection in a custom action due to serialization issues with early-bound types.</remarks>
    public static EntityCollection ToEntityCollection(this IEnumerable<Entity> records)
    {
        return new EntityCollection([.. records.Select(x => x.ToEntity())]);
    }
}

