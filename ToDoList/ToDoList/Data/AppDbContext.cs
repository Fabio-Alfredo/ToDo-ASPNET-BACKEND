using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Token>Tokens { get; set; }
}