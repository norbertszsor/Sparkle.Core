using LinqToDB.Mapping;

namespace Sparkle.Domain.Extensions
{
    public static class MappingSchemaExtension
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
