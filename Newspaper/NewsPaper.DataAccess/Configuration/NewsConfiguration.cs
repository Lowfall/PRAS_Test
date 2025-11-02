using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newspaper.Domain.Entities;

namespace NewsPaper.DataAccess.Configuration;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.HasKey(news => news.Id);
        
        builder.Property(news => news.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(news => news.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.HasOne(news => news.User)
            .WithMany(user => user.News)
            .HasForeignKey(news => news.UserId);

        builder.HasMany(news => news.Translations)
            .WithOne(translation => translation.News)
            .HasForeignKey(news => news.NewsId);
    }
}