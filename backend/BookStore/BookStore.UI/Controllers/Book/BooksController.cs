using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BookStore.UI.Controllers.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookStoreDbContext _db;

        public BooksController(BookStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _db.Set<Book>();
        }
    }
}
