using System.Collections.Generic;

namespace Core.Abstractions.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Retrieve();
        TEntity Retrieve(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
