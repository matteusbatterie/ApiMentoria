using System.Collections.Generic;

namespace Service.Class_Library.Repository.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Retrieve();
        TEntity Retrieve(int id);
        void Create(TEntity user);
        void Update(TEntity user);
        void Delete(int id);
    }
}