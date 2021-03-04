using System;
using System.Collections.Generic;
using System.Data;

using Core.Abstractions.Repository;
using Core.Entities;

using Repository.Helpers;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly IDbConnection _dbConnection;
        public readonly IDbCommand _dbCommand; 
        public IDataReader _dataReader { get; set; }

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
                                        [CPF] 
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
                    Console.WriteLine(users.ToString());
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

        public bool Create(User user)
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

                _dbConnection.Open();
                _dbCommand.ExecuteNonQuery();
                _dbConnection.Close();
            }

            return true;
        }

        public void Update(User user)
        {
            using (_dbConnection)
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
                _dbConnection.Close();
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

                _dbConnection.Open();
                _dbCommand.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }
    }
}
