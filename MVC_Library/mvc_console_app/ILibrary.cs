using Microsoft.VisualBasic;
using mvc_console_app.Models;

namespace mvc_console_app;

public interface ILibrary
{
    // Book operations
    void AddBook(Book book);
    void AddBooks(IEnumerable<Book> books);
    IEnumerable<Book> GetAllBooks();
    Book? GetBookById(int bookId);
    IEnumerable<Book> GetBooksByTitle(string title);
    IEnumerable<Book> GetBooksByAuthor(string author);
    Member? GetMemberById(int id);

    // LibraryMember operations
    IEnumerable<Member> GetAllMembers();
    void AddMember(Member libraryMember);

    // Checkout/Return operations
    void CheckoutBook(Member libraryMember, Book book);
    void ReturnBook(Member libraryMember, Book book);

    // Queries
    IEnumerable<Book> GetBooksCheckedOutByMember(Member libraryMember);
}