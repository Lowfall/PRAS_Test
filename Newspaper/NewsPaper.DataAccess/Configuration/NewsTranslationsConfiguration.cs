using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newspaper.Domain.Entities;

namespace NewsPaper.DataAccess.Configuration;

public class NewsTranslationsConfiguration : IEntityTypeConfiguration<NewsTranslations>
{
    public void Configure(EntityTypeBuilder<NewsTranslations> builder)
    {
        builder.HasKey(translation => translation.Id);
        
        builder.Property(translation => translation.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(translation => translation.Subtitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(translation => translation.Content)
            .IsRequired();

        builder.Property(translation => translation.Language)
            .IsRequired();

        builder.HasOne(translation => translation.News)
            .WithMany(news => news.Translations)
            .HasForeignKey(translation => translation.NewsId);
    }
}