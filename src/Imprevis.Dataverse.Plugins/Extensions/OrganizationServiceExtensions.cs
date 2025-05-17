namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;

    public static class OrganizationServiceExtensions
    {
        public static Entity Retrieve(this IOrganizationService service, EntityReference entityReference, ColumnSet columns)
        {
            return service.Retrieve(entityReference.LogicalName, entityReference.Id, columns);
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
