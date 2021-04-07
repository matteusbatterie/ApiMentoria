using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Core.Abstractions.Repository;
using Core.Entities;
using Microsoft.Data.SqlClient;
using Repository.Helpers;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbCommand _dbCommand;
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

                // Create query
                string query = @"SELECT [Id], 
                                        [Name], 
                                        [Email], 
                                        [Password], 
                                        [CPF],
                                   FROM [dbo].[Users]";

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Connection = _dbConnection;

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;


                _dbConnection.Open();
                _dataReader = _dbCommand.ExecuteReader();

                // Read data 
                while (_dataReader.Read())
                {
                    User user = new User();
                    user = RepositoryMapper.MapDataReaderToEntity<User>(_dataReader, user);

                    users.Add(user);
                }

                _dbConnection.Close();

            }

            return users;
        }

        public User Retrieve(int id)
        {
            User user = new User();

            using (_dbConnection)
            {
                string query = $"SELECT * FROM [dbo].[Users] WHERE [Id] = {id}";

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Connection = _dbConnection;

                _dbConnection.Open();
                _dataReader = _dbCommand.ExecuteReader();

                while (_dataReader.Read())
                {
                    user = RepositoryMapper.MapDataReaderToEntity(_dataReader, user);
                }

                _dbConnection.Close();
            }

            return user;
        }

        public void Create(User user)
        {
            using (_dbConnection)
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

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Connection = _dbConnection;

                _dbCommand.Parameters.Add(user.Name);
                _dbCommand.Parameters.Add(user.Email);
                _dbCommand.Parameters.Add(user.Password);
                _dbCommand.Parameters.Add(user.CPF);

                _dbTransaction = _dbConnection.BeginTransaction();
                _dbConnection.Open();

                try
                {
                    _dbCommand.ExecuteNonQuery();
                    _dbTransaction.Commit();
                }
                catch (System.Exception)
                {
                    _dbTransaction.Rollback();
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }

        public void Update(User user)
        {
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (_dbConnection)
                {
                    try
                    {
                        string query = @"UPDATE [dbo].[Users] 
                                    SET [Name] = @Name, 
                                        [Email] = @Email, 
                                        [Password] = @Password, 
                                        [CPF] = @CPF
                                    WHERE [Id] = @Id";

                        _dbCommand.CommandText = query;
                        _dbCommand.CommandType = CommandType.Text;
                        _dbCommand.Connection = _dbConnection;

                        _dbCommand.Parameters.Add(user.Id);
                        _dbCommand.Parameters.Add(user.Name);
                        _dbCommand.Parameters.Add(user.Email);
                        _dbCommand.Parameters.Add(user.Password);
                        _dbCommand.Parameters.Add(user.CPF);

                        _dbConnection.Open();
                        _dbCommand.ExecuteNonQuery();
                    }
                    catch (System.Exception)
                    {
                        txscope.Dispose();
                    }
                    finally
                    {
                        _dbConnection.Close();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (_dbConnection)
            {
                string query = @"DELETE 
                                FROM [dbo].[Users] 
                                WHERE [Id] = @Id";

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Connection = _dbConnection;

                _dbCommand.Parameters.Add(id);

                _dbTransaction = _dbConnection.BeginTransaction();
                _dbConnection.Open();

                try
                {
                    _dbCommand.ExecuteNonQuery();
                    _dbTransaction.Commit();
                }
                catch (System.Exception)
                {
                    _dbTransaction.Rollback();
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }
    }
}
