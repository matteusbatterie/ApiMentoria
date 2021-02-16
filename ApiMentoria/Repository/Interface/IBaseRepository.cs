using System.Collections.Generic;

namespace ApiMentoria.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Retrieve();
        TEntity Retrieve(int id);
        bool Create(TEntity user);
        void Update(TEntity user);
        void Delete(int id);
    }
}