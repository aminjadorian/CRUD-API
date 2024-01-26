using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid Id);
        Task CreateAsync(User user);
        void Remove(User user);
        void updateAsync(User user);
        Task<bool> isUserExistAsync(string email);
    }
}
