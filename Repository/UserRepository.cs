using System.Collections.Generic;

using Core.Abstractions.Repository;
using Core.Entities;

using Dapper;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        // public readonly IDbConnection _dbConnection;
        // public readonly IDbCommand _dbCommand;
        // private IDataReader _dataReader { get; set; }
        // private IDbTransaction _dbTransaction { get; set; }

        // public UserRepository(IDbConnection dbConnection, IDbCommand dbCommand)
        // {
        //     _dbConnection = dbConnection;
        //     _dbCommand = dbCommand;
        // }
        private readonly DbSession _dbSession;

        public UserRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IEnumerable<User> Retrieve()
        {
            // using (_dbConnection)
            // {
                string query = "SELECT * FROM  [dbo].[Users]";
                return _dbSession.Connection.Query<User>(query);
            //}
        }

        public User Retrieve(int id)
        {
                string query = $@"SELECT * 
                                FROM [dbo].[Users] 
                                WHERE Id = {id}";
                return _dbSession.Connection.QueryFirst<User>(query);
        }

        public void Create(User user)
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

                _dbSession.Connection.Execute(query, user);
        }

        public void Update(User user)
        {
                string query = @"UPDATE [dbo].[Users] 
                                    SET [Name] = @Name, 
                                        [Email] = @Email, 
                                        [Password] = @Password, 
                                        [CPF] = @CPF
                                    WHERE [Id] = @Id";

                _dbSession.Connection.Execute(query, user);
        }

        public void Delete(int id)
        {
                string query = @"DELETE 
                                FROM [dbo].[Users] 
                                WHERE [Id] = @Id";

                 _dbSession.Connection.Execute(query, id);
        }
    }
}
