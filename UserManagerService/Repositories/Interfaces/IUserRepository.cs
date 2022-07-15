using System.Collections.Generic;
using UserManagerService.Models;

namespace UserManagerService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllActiveUsers();
        public void AddUser(User user);
        public bool UpdateUserState(int userId, bool active);
        public bool DeleteUser(int userId);
    }
}
