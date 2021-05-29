using Core.Abstractions.Repositories;
using Core.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<User> Retrieve()
        {
            IEnumerable<User> users;

            using (_connection)
            {
                _connection.Open();
                users = _connection.GetAll<User>();
            }

            return users;
        }

        public User Retrieve(int id)
        {
            User user = new User();
            using (_connection)
            {
                using (_transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        _connection.Open();
                        user = _connection.Get<User>(id);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        _transaction.Dispose();
                        _connection.Close();
                    }
                }
            }

            return user;
        }

        public void Create(User user)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Insert<User>(user);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Update(User user)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Update<User>(user);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Delete<User>(new User { Id = id });
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = new User();
            using (_connection)
            {
                using (_transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        _connection.Open();
                        string query = @"SELECT * 
                                        FROM Users 
                                        WHERE Email = @email";
                        user = await _connection.QueryFirstAsync<User>(query, email);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        _transaction.Dispose();
                        _connection.Close();
                    }
                }
            }

            return user;
        }
    }
}
