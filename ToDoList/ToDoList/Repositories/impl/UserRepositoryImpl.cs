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
    
    public User Save(User user)
    {
        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
        return user;
    }

    public User? FindById(Guid id)
    {
        var user = appDbContext.Users.Find(id);
        return user ?? null;
    }

    public User? FindByEmail(string email)
    {
        var user = appDbContext.Users.FirstOrDefault(e => e.Email == email);
        return user ?? null;
    }

    public List<User> FindAll()
    {
        return appDbContext.Users.ToList();
    }
}