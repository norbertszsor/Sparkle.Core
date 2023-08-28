using LinqToDB.Mapping;

namespace Sparkle.Api.Shared.Extensions
{
    public static class MappingSchemaExstension
    {
        public static PropertyMappingBuilder<T, TProperty> HasAssociation<T, TProperty>
           (this PropertyMappingBuilder<T, TProperty> builder, string thisKey, string otherKey, bool canBeNull = true)
        {
            return builder.HasAttribute(new AssociationAttribute
            {
                ThisKey = thisKey,
                OtherKey = otherKey,
                CanBeNull = canBeNull
            });
        }
    }
}
