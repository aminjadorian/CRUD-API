using Domain.User;
using Infrastructure.DbContextConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CrudDbContext _dbContext;

        public UserRepository(CrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public Task<User?> GetByIdAsync(Guid Id)
        {
            var result = _dbContext.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> isUserExistAsync(string email)
        {
             return await _dbContext.Users.AnyAsync(a=> a.Email == email);
        }

        public void Remove(User user)
        {
           _dbContext.Users.Remove(user);
        }
        //it will manage by ef core, we dont need to implement it :)
        public void updateAsync(User user)
        {
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).State = EntityState.Modified;
        }
    }
}
