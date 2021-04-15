using System.Collections.Generic;
using System.Data;

namespace Core.Abstractions.Repository
{
    public abstract class BaseRepository
    {

        protected IDbConnection _dbConnection { get; }
        protected IDbCommand _dbCommand { get; }
        protected IDataReader _dataReader { get; set; }

        public BaseRepository(IDbConnection dbConnection, IDbCommand dbCommand)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
        }

        #region Auxiliary Method
        protected TEntity MapDataReaderToEntity<TEntity>(IDataReader dataReader, TEntity entity) 
            where TEntity : class
        {
            var classType = entity.GetType();
            var properties = classType.GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entity, _dataReader[property.Name]);
            }

            return entity;
        }
        #endregion
    }
}