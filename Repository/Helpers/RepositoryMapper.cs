using System.Data;

namespace Repository.Helpers
{
    public static class RepositoryMapper
    {
        public static TEntity MapDataReaderToEntity<TEntity>(IDataReader dataReader, TEntity entity) 
            where TEntity : class
        {
            var classType = entity.GetType();
            var properties = classType.GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entity, dataReader[property.Name]);
            }

            return entity;
        }
    }
}