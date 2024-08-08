﻿using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Persistence.Configurations.Book_Genres
{
    internal class Book_GenreConfiguration : IEntityTypeConfiguration<Book_Genre>
    {
        public void Configure(EntityTypeBuilder<Book_Genre> builder)
        {
            builder.ToTable("Book_Geners","bookStore");

            builder.HasKey(x => new { x.GenreId, x.BookId });            
        }
    }
}