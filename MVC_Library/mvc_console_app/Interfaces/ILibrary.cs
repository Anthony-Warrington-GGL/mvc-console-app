using Microsoft.VisualBasic;
using mvc_console_app.Models;

namespace mvc_console_app;

public interface ILibrary
{
    // Book operations
    /// <summary>
    /// Takes the author and title, creates an id and then creates a book using those
    /// </summary>
    /// <param name="author">The author of the book</param>
    /// <param name="title">The title of the book</param>
    /// <returns>The <see cref="Book"/>, as created</returns>
    /// <exception cref="InvalidOperationException"> When a book can't be created</exception>
    Book CreateBook(string author, string title); // TODO: Make this consistent with how members are created (i.e. it should return a Book and the Library should decide the id)
    IEnumerable<Book> CreateBooks(List<(string Author, string Title)> books);
    IEnumerable<Book> GetAllBooks();
    Book? GetBookById(int bookId);
    IEnumerable<Book> GetBooksByTitle(string title);
    IEnumerable<Book> GetBooksByAuthor(string author);
    Member? GetMemberById(int id);

    // LibraryMember operations
    IEnumerable<Member> GetAllMembers();

    /// <summary>
    /// Creates a member using a first name and a last name
    /// </summary>
    /// <param name="firstName">the first name of the member</param>
    /// <param name="lastName">the last name of the member</param>
    /// <returns>the <see cref="Member"/>, as created</returns>
    /// <exception cref="InvalidOperationException"> When a user can't be created</exception>
    Member CreateMember(string firstName, string lastName);

    // Checkout/Return operations
    bool CheckoutBook(Member libraryMember, Book book);
    bool ReturnBook(Member libraryMember, Book book);

    // Queries
    IEnumerable<Book> GetBooksCheckedOutByMember(Member libraryMember);
}