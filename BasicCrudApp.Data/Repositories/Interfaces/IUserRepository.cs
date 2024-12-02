using BasicCrudApp.Domain.Entities;

namespace BasicCrudApp.Data
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<List<User>> GetUsers();
        Task<User> GetUser(long id);
        Task<bool> RemoveUser(long id);
        Task<bool> UpdateUser(long id, User user);
        Task<string> CheckExistingUserAsync(string email, string phone);
    }
}
