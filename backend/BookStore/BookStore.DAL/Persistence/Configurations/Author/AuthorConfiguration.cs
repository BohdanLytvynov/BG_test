using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Authors
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20).HasColumnType("VARCHAR(20)");
            builder.Property(x => x.Surename).IsRequired().HasMaxLength(20).HasColumnType("VARCHAR(20)");
            builder.Property(x => x.BirthDate).IsRequired().HasColumnType("DATE");

            builder.HasMany<Book>().WithOne(x => x.Author).HasForeignKey(x => x.AuthorId);
        }
    }
}
