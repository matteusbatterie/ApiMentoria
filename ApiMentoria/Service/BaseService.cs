using System.Collections.Generic;

using ApiMentoria.Repository.Interface;

namespace ApiMentoria.Service
{
    public abstract class BaseService<TEntity> where TEntity: class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository) 
        {
            this._repository = repository;
        }

        // public BaseService(IRepositoryProvider repositoryProvider)
        // {
        //     this._entityRepository = repositoryProvider.GetRepository<TEntity>();
        // }

        public virtual IEnumerable<TEntity> Retrieve()
        {
            return _repository.Retrieve();
        }

        public virtual TEntity Retrieve(int id)
        {                
            return _repository.Retrieve(id);
        }

        public virtual bool Create(TEntity entity)
        {
            _repository.Create(entity);

            return true;
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}