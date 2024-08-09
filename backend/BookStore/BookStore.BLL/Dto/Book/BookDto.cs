using BookStore.BLL.Dto.Author;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.Book
{
    public class BookDto : CreateAuthorDto
    {
        public int Id { get; set; }        
    }
}
