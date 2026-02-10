namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

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
        /// Retrieves multiple entities that match the specified <see cref="QueryBase"/> from the organization service.
        /// </summary>
        public static IEnumerable<TEntity> RetrieveMultiple<TEntity>(this IOrganizationService service, QueryBase query) where TEntity : Entity
        {
            var results = service.RetrieveMultiple(query);
            return results.Entities.Select(e => e.ToEntity<TEntity>());
        }

        /// <summary>
        /// Retrieves the first entity that matches the specified <see cref="QueryBase"/> from the organization service or null if no entity is found.
        /// </summary>
        /// <remarks>This method adds a "TopCount" of 1 to the provided query.</remarks>
        public static Entity RetrieveSingle(this IOrganizationService service, QueryBase query)
        {
            switch (query)
            {
                case FetchExpression fe:
                    var fetchXml = XElement.Parse(fe.Query);
                    fetchXml.SetAttributeValue("top", 1);
                    fe.Query = fetchXml.ToString();
                    break;
                case QueryExpression qe:
                    qe.TopCount = 1;
                    break;
                case QueryByAttribute qba:
                    qba.TopCount = 1;
                    break;
                default:
                    throw new NotSupportedException("Unknown query object.");
            }

            var results = service.RetrieveMultiple(query);

            return results.Entities.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first entity that matches the specified <see cref="QueryBase"/> from the organization service and converts it to the specified entity type. If no entity is found it returns null.
        /// </summary>
        /// /// <remarks>This method adds a "TopCount" of 1 to the provided query.</remarks>
        public static TEntity RetrieveSingle<TEntity>(this IOrganizationService service, QueryBase query) where TEntity : Entity
        {
            var entity = service.RetrieveSingle(query);
            if (entity == null)
            {
                return null;
            }
            return entity.ToEntity<TEntity>();
        }

        /// <summary>
        /// Retrieves all pages of the provided <see cref="QueryBase"/>.
        /// </summary>
        public static IEnumerable<Entity> RetrieveAll(this IOrganizationService service, QueryBase query)
        {
            switch (query)
            {
                case FetchExpression fe:
                    var pageNumber = 1;
                    var pageCookie = string.Empty;

                    var fetchXml = XElement.Parse(fe.Query);
                    fetchXml.SetAttributeValue("page", pageNumber);
                    fetchXml.SetAttributeValue("paging-cookie", pageCookie);
                    fe.Query = fetchXml.ToString();

                    while (true)
                    {
                        var results = service.RetrieveMultiple(fe);

                        foreach (var entity in results.Entities)
                        {
                            yield return entity;
                        }

                        if (!results.MoreRecords)
                        {
                            break;
                        }

                        fetchXml.SetAttributeValue("page", pageNumber++);
                        fetchXml.SetAttributeValue("paging-cookie", results.PagingCookie);
                        fe.Query = fetchXml.ToString();
                    }

                    break;
                case QueryExpression qe:
                    qe.PageInfo = new PagingInfo
                    {
                        PageNumber = 1,
                    };

                    while (true)
                    {
                        var results = service.RetrieveMultiple(qe);

                        foreach (var entity in results.Entities)
                        {
                            yield return entity;
                        }

                        if (!results.MoreRecords)
                        {
                            break;
                        }

                        qe.PageInfo.PageNumber++;
                        qe.PageInfo.PagingCookie = results.PagingCookie;
                    }
                    break;
                case QueryByAttribute qba:
                    qba.PageInfo = new PagingInfo
                    {
                        PageNumber = 1,
                    };

                    while (true)
                    {
                        var results = service.RetrieveMultiple(qba);

                        foreach (var entity in results.Entities)
                        {
                            yield return entity;
                        }

                        if (!results.MoreRecords)
                        {
                            break;
                        }

                        qba.PageInfo.PageNumber++;
                        qba.PageInfo.PagingCookie = results.PagingCookie;
                    }
                    break;
                default:
                    throw new NotSupportedException("Unknown query object.");
            }
        }

        /// <summary>
        /// Retrieves all pages of the provided <see cref="QueryBase"/> and converts it to the specified entity type.
        /// </summary>
        public static IEnumerable<TEntity> RetrieveAll<TEntity>(this IOrganizationService service, QueryBase query) where TEntity : Entity
        {
            foreach (var entity in service.RetrieveAll(query))
            {
                yield return entity.ToEntity<TEntity>();
            }
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

        /// <summary>
        /// Executres an action in the organization organization using the specified request.
        /// </summary>
        public static TResponse Execute<TRequest, TResponse>(this IOrganizationService service, TRequest request) where TRequest : OrganizationRequest where TResponse : OrganizationResponse
        {
            return (TResponse)service.Execute(request);
        }
    }
}
