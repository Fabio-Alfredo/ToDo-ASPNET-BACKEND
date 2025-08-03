using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Domain.Entities;

namespace ToDoList.Repositories.impl;

public class UserRepositoryImpl:IUserRepository
{
    private readonly AppDbContext appDbContext;
    
    public UserRepositoryImpl(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
        public async Task<User> Save(User user)
        {
            appDbContext.Users.Add(user);
             await appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> FindById(Guid id)
        {
            var user = await appDbContext.Users.FindAsync(id);
            return user ?? null;
        }

        public async Task<User?> FindByEmail(string email)
        {
            
            var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            return user ?? null;
        }

    public async Task<List<User>> FindAll()
    {
        return await appDbContext.Users.ToListAsync();
    }
}