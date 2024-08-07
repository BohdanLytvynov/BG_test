using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Books
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20).HasColumnType("VARCHAR(20)");
            builder.Property(x => x.Genre).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR");
            builder.Property(x => x.PubDate).IsRequired().HasColumnType("DATE");
        }
    }
}
