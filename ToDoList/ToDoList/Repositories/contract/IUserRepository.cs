using ToDoList.Domain.Entities;

namespace ToDoList.Repositories.impl;

public interface IUserRepository
{
    Task<User> Save(User user);
    Task<User?> FindById(Guid id);
    Task<User?> FindByEmail(string email);
    Task<List<User>> FindAll();
}