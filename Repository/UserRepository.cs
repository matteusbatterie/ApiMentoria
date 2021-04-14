using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using Core.Abstractions.Repository;
using Core.Entities;

using Dapper;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly IDbConnection _dbConnection;
        public readonly IDbCommand _dbCommand;
        private IDataReader _dataReader { get; set; }
        private IDbTransaction _dbTransaction { get; set; }

        public UserRepository(IDbConnection dbConnection, IDbCommand dbCommand)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
        }


        public IEnumerable<User> Retrieve()
        {
            List<User> users = new List<User>();

            using (_dbConnection)
            {
                _dbConnection.Open();

                string query = "SELECT * FROM  [dbo].[Users]";
                users = _dbConnection.Query<User>(query).ToList();
                _dbConnection.Close();
            }

            return users;
        }

        public User Retrieve(int id)
        {
            User user;
            using (_dbConnection)
            {
                _dbConnection.Open();

                string query = $@"SELECT * 
                                FROM [dbo].[Users] 
                                WHERE Id = {id}";
                user = _dbConnection.QueryFirst<User>(query);
                _dbConnection.Close();
            }

            return user;
        }

        public void Create(User user)
        {
            using (_dbConnection)
            {
                using (_dbTransaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO [dbo].[Users] 
                                            ([Name], 
                                            [Email], 
                                            [Password], 
                                            [CPF]) 
                                        VALUES
                                            (@Name, 
                                            @Email, 
                                            @Password, 
                                            @CPF)";

                        _dbConnection.Execute(query, user, _dbTransaction);
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

        public void Update(User user)
        {
            using (_dbConnection)
            {
                using (_dbTransaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        string query = @"UPDATE [dbo].[Users] 
                                    SET [Name] = @Name, 
                                        [Email] = @Email, 
                                        [Password] = @Password, 
                                        [CPF] = @CPF
                                    WHERE [Id] = @Id";

                        _dbConnection.Execute(query, user, _dbTransaction);
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

        public void Delete(int id)
        {
            using (_dbConnection)
            {
                using (_dbTransaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        string query = @"DELETE 
                                FROM [dbo].[Users] 
                                WHERE [Id] = @Id";

                        _dbConnection.Execute(query, id);
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
}
