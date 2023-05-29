using Domain.Entities.Users;
using Domain.Result;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Entities.Users
{
    public class UsersInteractor : IUsersBoundary
    {
        private readonly IUsersGateway _users;

        public UsersInteractor(IUsersGateway users)
            => this._users = users;

        public IEnumerable<User> GetAll()
            => this._users.GetAll();

        public IResult<int> Create(User user)
            => this._users.Create(user);

        public IResult ParseCSV(IFormFile file)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());

                string currentRecord = reader.ReadLine();

                while (!string.IsNullOrWhiteSpace(currentRecord))
                {
                    string[] values = currentRecord.Split(",");

                    this._users.Create(new User
                    {
                        Name = values[0],
                        BirthDate = DateTime.Parse(values[1]),
                        Married = Convert.ToBoolean(values[2]),
                        Salary = Convert.ToDecimal(values[3]),
                        Phone = values[4]
                    });

                    currentRecord = reader.ReadLine();
                }

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failed("Failed to process file.");
            }
        }
    }
}