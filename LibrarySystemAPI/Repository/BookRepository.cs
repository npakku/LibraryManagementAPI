using LibrarySystemAPI.Data;
using LibrarySystemAPI.Models;
using LibrarySystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibrarySystemAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public bool AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            return Save();
        }

        public bool BookExists(int bookId)
        {
            return _dbContext.Books.Any(b => b.Id == bookId);   
        }

        public bool DeleteBook(Book book)
        {
            _dbContext.Remove(book);
            return Save();
        }

        public ICollection<Book> GetAllBooks()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBookById(int bookId)
        {
            return _dbContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
     
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;    
        }

        public bool UpdateBook(Book book)
        {
            _dbContext.Update(book);
            return Save();
        }
    }
}
