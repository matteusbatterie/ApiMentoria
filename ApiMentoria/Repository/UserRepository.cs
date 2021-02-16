using ApiMentoria.Repository.Interface;
using ApiMentoria.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApiMentoria.Repository
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection _dbConnection { get; }
        private IDbCommand _dbCommand { get; }
        private IDataReader _dataReader { get; set; }

        public UserRepository(IDbConnection dbConnection, IDbCommand dbCommand, IDataReader dataReader)
        {
            this._dbConnection = dbConnection;
            this._dbCommand = dbCommand;
            this._dataReader = dataReader;
        }

        public IEnumerable<User> Retrieve()
        {
            List<User> listUsers = new List<User>();

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

                _dbConnection.Open();
                _dataReader = _dbCommand.ExecuteReader();

                // Read data 
                // TO DO: Mover para um método usando Reflection
                while (_dataReader.Read())
                {
                    User user = new User();
                    user = MapDataReaderToEntity<User>(_dataReader, user);

                    listUsers.Add(user);
                }

                _dbConnection.Close();
            }

            return listUsers;
        }

        public User Retrieve(int id)
        {
            User user = new User();
            using (_dbConnection)
            {
                string query = $"SELECT * FROM [dbo].[Users] WHERE [Id] = {id}";

                _dbCommand.CommandText = query;
                _dbCommand.CommandType = CommandType.Text;

                _dbConnection.Open();
                _dataReader = _dbCommand.ExecuteReader();

                while (_dataReader.Read())
                {
                    user.Id = Convert.ToInt32(_dataReader["Id"]);
                    user.Name = _dataReader["Name"].ToString();
                    user.Email = _dataReader["Email"].ToString();
                    user.Password = _dataReader["Password"].ToString();
                    user.CPF = _dataReader["CPF"].ToString();
                }
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

                _dbCommand.Parameters.Add(id);

                _dbConnection.Open();
                _dbCommand.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }

        // TO DO: Move the following methods to a base service with generic types
        #region Auxiliary Method
        private TEntity MapDataReaderToEntity<TEntity>(IDataReader dataReader, TEntity entity)
        {
            var classType = entity.GetType();
            var properties = classType.GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(entity, _dataReader[property.Name]);
            }

            return entity;
        }
        #endregion
    }
}
