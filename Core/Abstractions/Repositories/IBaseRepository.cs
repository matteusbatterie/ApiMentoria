using System.Collections.Generic;
using Core.Entities;

namespace Core.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> Retrieve();
        TEntity Retrieve(int id);
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);

        
    }
}
