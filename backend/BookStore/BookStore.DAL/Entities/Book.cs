using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateOnly? PubDate { get; set; }

        public string? Genre { get; set; }

        public Author? Author { get; set; }

        public int AuthorId { get; set; }
    }
}
