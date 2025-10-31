using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsPaper.DataAccess.Options;
using Newspaper.Domain.Entities;

namespace NewsPaper.DataAccess.Data;

public class AppDbContext(DbContextOptions options, IOptions<ConnectionStrings> connectionStrings) : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionStrings.Value.DefaultConnection);
        base.OnConfiguring(optionsBuilder);
    }
}