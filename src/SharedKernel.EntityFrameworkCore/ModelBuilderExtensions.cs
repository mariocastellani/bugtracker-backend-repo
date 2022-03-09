namespace SharedKernel.EntityFrameworkCore
{
    internal static class ModelBuilderExtensions
    {
        public static void SetDefaultDeleteBehaviorInForeignKeys(this ModelBuilder modelBuilder, DeleteBehavior deleteBehavior)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var property in entityType.GetForeignKeys().Where(x => !x.IsOwnership))
                    property.DeleteBehavior = deleteBehavior;
        }

        public static void SetDefaultMaxLengthOfStringProperties(this ModelBuilder modelBuilder, int defaultMaxLength)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var stringProperties = entityType.GetProperties()
                    .Where(x =>
                        x.PropertyInfo.PropertyType == typeof(string) &&
                        !x.PropertyInfo.GetCustomAttributes(typeof(MaximumMaxLengthAttribute), false).Any());

                foreach (var property in stringProperties)
                    property.SetMaxLength(defaultMaxLength);
            }
        }
    }
}