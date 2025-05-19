namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using System;

    public static class EntityExtensions
    {
        public static T GetAliasedValue<T>(this Entity entity, string aliasName, string attributeName, T defaultValue = default)
        {
            if (entity == null)
            {
                return defaultValue;
            }

            var value = entity.GetAttributeValue<AliasedValue>(aliasName + "." + attributeName)?.Value;
            if (value == null)
            {
                return defaultValue;
            }

            return (T)value;
        }

        public static string GetAliasedFormattedValue(this Entity entity, string aliasName, string fieldName, string defaultValue = null)
        {
            return entity.GetFormattedValue($"{aliasName}.{fieldName}", defaultValue);
        }

        public static string GetFormattedValue(this Entity entity, string fieldName, string defaultValue = null)
        {
            if (entity.FormattedValues.ContainsKey(fieldName))
            {
                return entity.FormattedValues[fieldName] ?? defaultValue;
            }

            return defaultValue;
        }

        public static int? GetOptionSetValue(this Entity entity, string attributeName)
        {
            return entity.GetAttributeValue<OptionSetValue>(attributeName)?.Value;
        }

        public static int GetOptionSetValue(this Entity entity, string attributeName, int defaultValue)
        {
            return entity.GetOptionSetValue(attributeName) ?? defaultValue;
        }

        public static T? GetOptionSetValue<T>(this Entity entity, string attributeName) where T: struct
        {
            var value = entity.GetOptionSetValue(attributeName);
            var success = Enum.TryParse(value?.ToString(), out T result);
            if (success)
            {
                return result;
            }

            return null;
        }

        public static Entity Clone(this Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var newEntity = new Entity(entity.LogicalName, entity.Id);

            foreach (var attribute in entity.Attributes)
            {
                entity.Attributes[attribute.Key] = attribute.Value;
            }

            foreach (var formattedValue in entity.FormattedValues)
            {
                entity.FormattedValues[formattedValue.Key] = formattedValue.Value;
            }

            foreach (var keyAttribute in entity.KeyAttributes)
            {
                entity.KeyAttributes[keyAttribute.Key] = keyAttribute.Value;
            }

            return newEntity;
        }

        /// <summary>
        /// Coalesces the entities into the source entity in the given order.
        /// </summary>
        /// <returns>A new entity with the coalesced attributes.</returns>
        public static Entity Coalesce(this Entity baseEntity, params Entity[] entities)
        {
            if (baseEntity == null)
            {
                throw new ArgumentNullException(nameof(baseEntity));
            }

            var newEntity = baseEntity.Clone();

            foreach (var entity in entities)
            {
                foreach (var attribute in entity.Attributes)
                {
                    if (!newEntity.Attributes.ContainsKey(attribute.Key))
                    {
                        newEntity[attribute.Key] = attribute.Value;
                    }
                }

                foreach (var formattedValue in entity.FormattedValues)
                {
                    if (!newEntity.FormattedValues.ContainsKey(formattedValue.Key))
                    {
                        newEntity.FormattedValues[formattedValue.Key] = formattedValue.Value;
                    }
                }

                foreach (var keyAttribute in entity.KeyAttributes)
                {
                    if (!newEntity.KeyAttributes.ContainsKey(keyAttribute.Key))
                    {
                        newEntity.KeyAttributes[keyAttribute.Key] = keyAttribute.Value;
                    }
                }
            }

            return newEntity;
        }
    }
}
