namespace Application.Helpers;

using Application.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseSqlServer(Configuration.GetConnectionString("UsersApiConnectionString"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }

}