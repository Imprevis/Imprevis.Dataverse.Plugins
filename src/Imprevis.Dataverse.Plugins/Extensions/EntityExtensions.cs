namespace Imprevis.Dataverse.Plugins.Extensions
{
    using Microsoft.Xrm.Sdk;
    using System;

    /// <summary>
    /// Extensions for the Entity class.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Gets the value of an AliasedValue from an entity, using the alias name and attribute name.
        /// </summary>
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

        /// <summary>
        /// Gets the formatted value of an AliasedValue from an entity, using the alias name and attribute name.
        /// </summary>
        public static string GetAliasedFormattedValue(this Entity entity, string aliasName, string fieldName, string defaultValue = null)
        {
            return entity.GetFormattedValue($"{aliasName}.{fieldName}", defaultValue);
        }

        /// <summary>
        /// Gets the formatted value of an attribute from an entity.
        /// </summary>
        public static string GetFormattedValue(this Entity entity, string fieldName, string defaultValue = null)
        {
            if (entity.FormattedValues.ContainsKey(fieldName))
            {
                return entity.FormattedValues[fieldName] ?? defaultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the value of an OptionSetValue from an entity.
        /// </summary>
        public static int? GetOptionSetValue(this Entity entity, string attributeName)
        {
            return entity.GetAttributeValue<OptionSetValue>(attributeName)?.Value;
        }

        /// <summary>
        /// Gets the value of an OptionSetValue from an entity, or returns a default value if it doesn't exist.
        /// </summary>
        public static int GetOptionSetValue(this Entity entity, string attributeName, int defaultValue)
        {
            return entity.GetOptionSetValue(attributeName) ?? defaultValue;
        }

        /// <summary>
        /// Gets the value of an OptionSetValue from an entity, or returns a default value if it doesn't exist.
        /// </summary>
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

        /// <summary>
        /// Clones the entity, creating a new instance with the same attributes and values.
        /// </summary>
        public static Entity Clone(this Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var newEntity = new Entity(entity.LogicalName, entity.Id);

            foreach (var attribute in entity.Attributes)
            {
                newEntity.Attributes[attribute.Key] = attribute.Value;
            }

            foreach (var formattedValue in entity.FormattedValues)
            {
                newEntity.FormattedValues[formattedValue.Key] = formattedValue.Value;
            }

            foreach (var keyAttribute in entity.KeyAttributes)
            {
                newEntity.KeyAttributes[keyAttribute.Key] = keyAttribute.Value;
            }

            return newEntity;
        }

        /// <summary>
        /// Clones the entity, creating a new instance with the same attributes and values.
        /// </summary>
        public static T Clone<T>(this Entity entity) where T : Entity
        {
            return Clone(entity).ToEntity<T>();
        }

        /// <summary>
        /// Coalesces the entities into the source entity in the given order.
        /// </summary>
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

        /// <summary>
        /// Coalesces the entities into the source entity in the given order.
        /// </summary>
        public static T Coalesce<T>(this Entity baseEntity, params Entity[] entities) where T : Entity
        {
            return Coalesce(baseEntity, entities).ToEntity<T>();
        }
    }
}
