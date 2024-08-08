﻿using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Book_Authors
{
    internal class Book_AuthorConfiguration : IEntityTypeConfiguration<Book_Author>
    {
        public void Configure(EntityTypeBuilder<Book_Author> builder)
        {            
            builder.ToTable("Book_Authors", "bookStore");

            builder.HasKey(x => new { x.BookId, x.AuthorId });
        }
    }
}