using LibrarySystemAPI.Models;

namespace LibrarySystemAPI.Repository.IRepository
{
    public interface IBookRepository 
    {
        ICollection<Book> GetAllBooks();
        Book GetBookById(int bookId);
        bool BookExists(int bookId);
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book bookId);    
        bool Save();
    }

}
