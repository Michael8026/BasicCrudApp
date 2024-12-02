using BasicCrudApp.Domain;
using BasicCrudApp.Domain.Common;
using BasicCrudApp.Domain.Entities;

namespace BasicCrudApp.Services
{
    public interface IUserService
    {
        Task<Result<long>> CreateUser(CreateUserDto user);
        Task<Result<List<User>>> GetUsers();
        Task<Result<User>> GetUser(long id);
        Task<Result<bool>> RemoveUser(long id);
        Task<Result<bool>> UpdateUser(long id, UserDto user);
    }
}
