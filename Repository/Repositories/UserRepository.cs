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

            _connection.Open();
            users = _connection.GetAll<User>();
            _connection.Close();

            return users;
        }

        public User Retrieve(int id)
        {
            User user = new User();

            _connection.Open();
            user = _connection.Get<User>(id);
            _connection.Close();

            return user;
        }

        public void Create(User user)
        {
            _connection.Open();
            using (_transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Insert<User>(user, _transaction);
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
            _connection.Open();
            using (_transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Update<User>(user, _transaction);
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
            _connection.Open();
            using (_transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Delete<User>(new User { Id = id }, _transaction);
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

            _connection.Open();
            using (_transaction = _connection.BeginTransaction())
            {
                try
                {
                    string query = @"SELECT * 
                                    FROM Users 
                                    WHERE Email = @Email";
                    user = await _connection.QueryFirstAsync<User>(query, new { Email = email }, _transaction);
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

            return user;
        }
    }
}
