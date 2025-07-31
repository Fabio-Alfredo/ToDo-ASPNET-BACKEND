using ToDoList.Domain.Entities;

namespace ToDoList.Repositories.impl;

public interface IUserRepository
{
    User Save(User user);
    User? FindById(Guid id);
    User? FindByEmail(string email);
    List<User> FindAll();
}