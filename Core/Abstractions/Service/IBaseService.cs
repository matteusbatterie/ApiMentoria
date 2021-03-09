using System.Collections.Generic;

namespace Core.Abstractions.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Retrieve();
        TEntity Retrieve(int id);
        bool Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
