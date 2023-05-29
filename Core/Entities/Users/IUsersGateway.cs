using Domain.Entities.Users;
using Domain.Result;

using System.Collections.Generic;

namespace Core.Entities.Users
{
    public interface IUsersGateway
    {
        IEnumerable<User> GetAll();
        IResult<int> Create(User user);
    }
}