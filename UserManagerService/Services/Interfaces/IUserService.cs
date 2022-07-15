using System.Collections.Generic;
using UserManagerService.Dtos;
using UserManagerService.Models;

namespace UserManagerService.Services.Interfaces
{
    public interface IUserService
    {
        public Response<List<User>> GetAllActiveUsers();
        public Response<object> AddUser(UserDto user);
        public Response<object> UpdateUserState(int userId, bool active);
        public Response<object> DeleteUser(int userId);
    }
}
