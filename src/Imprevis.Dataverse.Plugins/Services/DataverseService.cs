namespace Imprevis.Dataverse.Plugins;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

internal class DataverseService(IOrganizationService service, ICacheService cache, ILoggingService logger) : IDataverseService
{
    public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
    {
        logger.LogDebug("Retrieving Entity:");
        logger.LogDebug(new EntityReference(entityName, id));

        return service.Retrieve(entityName, id, columnSet);
    }

    public EntityCollection RetrieveMultiple(QueryBase query)
    {
        switch (query)
        {
            case FetchExpression fe:
                logger.LogDebug("Retrieving Entities by FetchXml");
                return service.RetrieveMultiple(fe);
            case QueryExpression qe:
                logger.LogDebug("Retrieving Entities by Expression: {0}", qe.EntityName);
                return service.RetrieveMultiple(qe);
            case QueryByAttribute qba:
                logger.LogDebug("Retrieving Entities by Attributes: {0}", qba.EntityName);
                logger.LogDebug(qba.Attributes);
                return service.RetrieveMultiple(qba);
            default:
                throw new NotSupportedException($"Query of type '{query.GetType().FullName}' is not supported.");
        }
    }

    public Guid Create(Entity entity)
    {
        logger.LogDebug("Create Entity:");
        logger.LogDebug(entity);

        return service.Create(entity);
    }

    public void Update(Entity entity)
    {
        logger.LogDebug("Update Entity:");
        logger.LogDebug(entity);

        service.Update(entity);
    }

    public void Delete(string entityName, Guid id)
    {
        logger.LogDebug("Delete Entity:");
        logger.LogDebug(new EntityReference(entityName, id));

        service.Delete(entityName, id);
    }

    public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
    {
        logger.LogDebug("Associate Entity:");
        logger.LogDebug(new EntityReference(entityName, entityId));

        service.Associate(entityName, entityId, relationship, relatedEntities);
    }

    public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
    {
        logger.LogDebug("Disassociate Entity:");
        logger.LogDebug(new EntityReference(entityName, entityId));

        service.Disassociate(entityName, entityId, relationship, relatedEntities);
    }

    public OrganizationResponse Execute(OrganizationRequest request)
    {
        logger.LogDebug("Executing Request: {0}", request.RequestName);
        logger.LogDebug(request.Parameters);

        return service.Execute(request);
    }

    public void Execute(IDataverseRequest request)
    {
        logger.LogDebug("Executing Request: {0}", request.GetType().FullName);

        request.Execute(this, logger);
    }

    public TResponse Execute<TResponse>(IDataverseRequest<TResponse> request)
    {
        logger.LogDebug("Executing Request: {0}", request.GetType().FullName);

        if (request is IDataverseCachedRequest<TResponse> cachedRequest)
        {
            return cache.GetOrAdd(cachedRequest.CacheKey, () => request.Execute(this, logger), cachedRequest.CacheDuration);
        }

        return request.Execute(this, logger);
    }

    public TResponse ExecuteWithoutCache<TResponse>(IDataverseCachedRequest<TResponse> request)
    {
        logger.LogDebug("Executing Request: {0} (Without Cache)", request.GetType().FullName);

        return request.Execute(this, logger);
    }
}
