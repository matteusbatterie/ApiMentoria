using System;
using System.Collections.Generic;
using Service.Class_Library.Repository.Interfaces;

namespace Service
{
    public class BaseService<TEntity> where TEntity: class
    {
        private readonly IEntityRepository<TEntity> _entityRepository;

        public BaseService(IEntityRepository<TEntity> entityRepository) 
        {
            this._entityRepository = entityRepository;
        }

        // public BaseService(IRepositoryProvider repositoryProvider)
        // {
        //     this._entityRepository = repositoryProvider.GetRepository<TEntity>();
        // }

        public IEnumerable<TEntity> Get()
        {
            return _entityRepository.Retrieve();
        }

        public TEntity Get(int id)
        {                
            return _entityRepository.Retrieve(id);
        }

        public void Create(TEntity entity)
        {
            _entityRepository.Create(entity);
        }

        public void Update(TEntity entity)
        {
            _entityRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _entityRepository.Delete(id);
        }
    }
}