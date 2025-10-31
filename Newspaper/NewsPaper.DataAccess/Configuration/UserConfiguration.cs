using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newspaper.Domain.Entities;

namespace NewsPaper.DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.HasMany(user => user.News)
            .WithOne(news => news.User)
            .HasForeignKey(news => news.UserId);
    }
}