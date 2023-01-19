using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookStore.Infrastructure.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {


            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(150)");

            builder.Property(x => x.Author).IsRequired().HasColumnType("varchar(150)");

            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(350)");

            builder.Property(x => x.Value).IsRequired();

            builder.Property(x => x.PublishDate).IsRequired();

            builder.Property(x => x.CategoryId).IsRequired();

            builder.ToTable("Books");
        }
    }
}
