using Domain.Entities.Users;
using Domain.Result;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;

namespace Core.Entities.Users
{
    public interface IUsersBoundary
    {
        IEnumerable<User> GetAll();
        IResult<int> Create(User user);
        IResult ParseCSV(IFormFile file);
    }
}