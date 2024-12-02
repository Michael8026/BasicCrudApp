using AutoMapper;
using BasicCrudApp.Data;
using BasicCrudApp.Domain;
using BasicCrudApp.Domain.Common;
using BasicCrudApp.Domain.Entities;

namespace BasicCrudApp.Services
{
    public class UserService : IUserService
    {
        private readonly ICoreDbContext _context;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(
            ICoreDbContext context,
            IUserRepository repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<long>> CreateUser(CreateUserDto userDto)
        {
            Result<long> result = new(false);

            try
            {
                //check if email and phone already exists
                var existingUser = await _repository.CheckExistingUserAsync(userDto.Email, userDto.Phone);

                if (existingUser != null)
                {
                    result.SetError("user not created", "email or phone already exists");

                    return result;
                }


                var user = _mapper.Map<User>(userDto);

                var response = await _repository.CreateUser(user);

                await _context.SaveChangesAsync();

                if (response == null)
                {
                    result.SetError("User not created", "User not created");
                }
                else
                {
                    result.SetSuccess(response.Id, $"User with Id {response.Id} Created Successfully !");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while creating User");
            }
            return result;
        }

        public async Task<Result<List<User>>> GetUsers()
        {
            Result<List<User>> result = new(false);

            try
            {
                var response = await _repository.GetUsers();

                result.SetSuccess(response.ToList(), "Retrieved Successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while retrieving Users");
            }
            return result;
        }

        public async Task<Result<User>> GetUser(long id)
        {
            Result<User> result = new(false);

            try
            {
                var response = await _repository.GetUser(id);

                result.SetSuccess(response, "Retrieved Successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while retrieving User");
            }

            return result;
        }

        public async Task<Result<bool>> RemoveUser(long id)
        {
            Result<bool> result = new(false);

            try
            {
                var response = await _repository.RemoveUser(id);

                await _context.SaveChangesAsync();

                if (!response)
                {
                    result.SetError("User not deleted", $"User with Id {id} not deleted");
                }
                else
                {
                    result.SetSuccess(response, $"User with Id {id} deleted Successfully !");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while removing User");
            }

            return result;
        }

        public async Task<Result<bool>> UpdateUser(long id, UserDto userDto)
        {
            Result<bool> result = new(false);

            try
            {
                var existingUser = await _repository.GetUser(id);

                if (existingUser == null) result.SetError("User not updated", $"User with Id {id} not updated");

                _mapper.Map(userDto, existingUser);

                var response = await _repository.UpdateUser(id, existingUser);

                await _context.SaveChangesAsync();

                if (!response)
                {
                    result.SetError("User not updated", $"User with Id {id} not updated");
                }
                else
                {
                    result.SetSuccess(response, $"User with Id {id} updated Successfully.");
                }

                result.Content = response;

            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while Updating User");
            }
            return result;
        }



    }
}
