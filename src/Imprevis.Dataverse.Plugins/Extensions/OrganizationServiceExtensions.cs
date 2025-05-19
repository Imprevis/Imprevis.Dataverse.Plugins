namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for the <see cref="IOrganizationService"/> interface.
    /// </summary>
    public static class OrganizationServiceExtensions
    {
        /// <summary>
        /// Retrieves an entity from the organization service using the specified <see cref="EntityReference"/> and <see cref="ColumnSet"/>.
        /// </summary>
        /// <returns></returns>
        public static Entity Retrieve(this IOrganizationService service, EntityReference entityReference, ColumnSet columnSet)
        {
            return service.Retrieve(entityReference.LogicalName, entityReference.Id, columnSet);
        }

        /// <summary>
        /// Retrieves an entity from the organization service using the specified <see cref="EntityReference"/> and <see cref="ColumnSet"/> and converts it to the specified entity type.
        /// </summary>
        public static TEntity Retrieve<TEntity>(this IOrganizationService service, EntityReference entityReference, ColumnSet columnSet) where TEntity : Entity
        {
            return service.Retrieve(entityReference, columnSet).ToEntity<TEntity>();
        }

        /// <summary>
        /// Retrieves an entity from the organization service using the specified entity name, ID, and <see cref="ColumnSet"/> and converts it to the specified entity type.
        /// </summary>
        public static TEntity Retrieve<TEntity>(this IOrganizationService service, string entityName, Guid id, ColumnSet columnSet) where TEntity : Entity
        {
            return service.Retrieve(entityName, id, columnSet).ToEntity<TEntity>();
        }

        /// <summary>
        /// Retrieves the first entity that matches the specified <see cref="QueryExpression"/> from the organization service.
        /// </summary>
        public static Entity RetrieveFirstOrDefault(this IOrganizationService service, QueryExpression query)
        {
            query.TopCount = 1;
            var results = service.RetrieveMultiple(query);

            return results.Entities.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first entity that matches the specified <see cref="QueryExpression"/> from the organization service and converts it to the specified entity type.
        /// </summary>
        public static TEntity RetrieveFirstOrDefault<TEntity>(this IOrganizationService service, QueryExpression query) where TEntity : Entity
        {
            var entity = service.RetrieveFirstOrDefault(query);
            if (entity == null)
            {
                return default;
            }
            return entity.ToEntity<TEntity>();
        }

        /// <summary>
        /// Creates a new entity in the organization service using the specified <see cref="EntityReference"/> and <see cref="AttributeCollection"/>.
        /// </summary>
        public static Guid Create(this IOrganizationService service, EntityReference entityReference, AttributeCollection attributes)
        {
            var entity = new Entity(entityReference.LogicalName, entityReference.Id)
            {
                Attributes = attributes
            };

            return service.Create(entity);
        }

        /// <summary>
        /// Updates an existing entity in the organization service using the specified <see cref="EntityReference"/> and <see cref="AttributeCollection"/>.
        /// </summary>
        public static void Update(this IOrganizationService service, EntityReference entityReference, AttributeCollection attributes)
        {
            var entity = new Entity(entityReference.LogicalName, entityReference.Id)
            {
                Attributes = attributes
            };

            service.Update(entity);
        }

        /// <summary>
        /// Deletes an entity from the organization service using the specified <see cref="EntityReference"/>.
        /// </summary>
        public static void Delete(this IOrganizationService service, EntityReference entityReference)
        {
            service.Delete(entityReference.LogicalName, entityReference.Id);
        }
    }
}
