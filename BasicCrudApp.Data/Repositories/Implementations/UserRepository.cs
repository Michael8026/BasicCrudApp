using BasicCrudApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicCrudApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ICoreDbContext _context;

        public UserRepository(ICoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var results = await _context.Users.ToListAsync();

            return results;

        }

        public async Task<User> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<bool> RemoveUser(long id)
        {
            try
            {
                var user = await _context.Users
                            .Where(x => x.Id == id)
                            .FirstAsync();

                var result = _context.Users.Remove(user);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateUser(long id, User user)
        {
            try
            {
                _context.Users.Update(user);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CheckExistingUserAsync(string email, string phone)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return "Email already exists";

            if (await _context.Users.AnyAsync(u => u.Phone == phone))
                return "Phone number already exists";

            return null;
        }


    }

}
