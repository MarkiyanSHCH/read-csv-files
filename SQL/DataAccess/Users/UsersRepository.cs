using Core.Entities.Users;

using Domain.Entities.Users;
using Domain.Result;

using Sql.DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQL.DataAccess.Users
{
    public class UsersRepository : IUsersGateway
    {
        private readonly ISqlDbSettings _settings;

        public UsersRepository(ISqlDbSettings settings)
            => this._settings = settings;

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult<int> Create(User user)
        {
            try
            {
                using var connection = new SqlConnection(this._settings.ConnectionString);
                using var command = new SqlCommand("[dbo].[spUsers_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = user.Name;
                command.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = user.BirthDate;
                command.Parameters.Add("@Married", SqlDbType.Bit).Value = user.Married;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = user.Phone;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = user.Salary;
                connection.Open();
                return Result.Success<int>((int)command.ExecuteScalar());
            }
            catch (Exception exception)
            {
                return Result.Failed<int>("Failed to create user.", exception);
            }
        }
    }
}