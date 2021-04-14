using System.Collections.Generic;
using System.Data;

using Core.Abstractions.Repository;
using Core.Entities;

using Repository.Helpers;

using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Linq;
using System;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<User> Retrieve()
        {
            List<User> users = new List<User>();

            using (_dbConnection)
            {
                _dbConnection.Open();
                users = _dbConnection.GetAll<User>().ToList();
            }

            return users;
        }

        public User Retrieve(int id)
        {
            User user = new User();
            using (_dbConnection)
            {
                using (_dbTransaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        _dbConnection.Open();
                        user = _dbConnection.Get<User>(id);
                        _dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        _dbTransaction.Dispose();
                        _dbConnection.Close();
                    }
                }
            }

            return user;
        }

        public void Create(User user)
        {
            using (_dbConnection)
            {
                try
                {
                    _dbConnection.Open();
                    _dbConnection.Insert<User>(user);
                    _dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _dbTransaction.Dispose();
                    _dbConnection.Close();
                }
            }
        }

        public void Update(User user)
        {
            using (_dbConnection)
            {
                try
                {
                    _dbConnection.Open();
                    _dbConnection.Update<User>(user);
                    _dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _dbTransaction.Dispose();
                    _dbConnection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (_dbConnection)
            {
                try
                {
                    _dbConnection.Open();
                    _dbConnection.Delete<User>(new User { Id = id });
                    _dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _dbTransaction.Dispose();
                    _dbConnection.Close();
                }
            }
        }
    }
}
