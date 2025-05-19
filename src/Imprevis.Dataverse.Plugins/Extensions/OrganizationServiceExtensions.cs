namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Linq;

    public static class OrganizationServiceExtensions
    {
        public static Entity Retrieve(this IOrganizationService service, EntityReference entityReference, ColumnSet columnSet)
        {
            return service.Retrieve(entityReference.LogicalName, entityReference.Id, columnSet);
        }

        public static TEntity Retrieve<TEntity>(this IOrganizationService service, EntityReference entityReference, ColumnSet columnSet) where TEntity : Entity
        {
            return service.Retrieve(entityReference, columnSet).ToEntity<TEntity>();
        }

        public static TEntity Retrieve<TEntity>(this IOrganizationService service, string entityName, Guid id, ColumnSet columnSet) where TEntity : Entity
        {
            return service.Retrieve(entityName, id, columnSet).ToEntity<TEntity>();
        }

        public static Entity RetrieveFirstOrDefault(this IOrganizationService service, QueryExpression query)
        {
            query.TopCount = 1;
            var results = service.RetrieveMultiple(query);

            return results.Entities.FirstOrDefault();
        }

        public static TEntity RetrieveFirstOrDefault<TEntity>(this IOrganizationService service, QueryExpression query) where TEntity : Entity
        {
            var entity = service.RetrieveFirstOrDefault(query);
            if (entity == null)
            {
                return default;
            }
            return entity.ToEntity<TEntity>();
        }

        public static Guid Create(this IOrganizationService service, EntityReference entityReference, AttributeCollection attributes)
        {
            var entity = new Entity(entityReference.LogicalName, entityReference.Id)
            {
                Attributes = attributes
            };

            return service.Create(entity);
        }

        public static void Update(this IOrganizationService service, EntityReference entityReference, AttributeCollection attributes)
        {
            var entity = new Entity(entityReference.LogicalName, entityReference.Id)
            {
                Attributes = attributes
            };

            service.Update(entity);
        }

        public static void Delete(this IOrganizationService service, EntityReference entityReference)
        {
            service.Delete(entityReference.LogicalName, entityReference.Id);
        }
    }
}
