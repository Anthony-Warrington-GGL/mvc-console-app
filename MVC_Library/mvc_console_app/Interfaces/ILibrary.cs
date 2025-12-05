using Microsoft.VisualBasic;
using mvc_console_app.Models;

namespace mvc_console_app.Interfaces;

public interface ILibrary
{    
    //TODO: Implement the GUIDs where necessary
    // 

    /// <summary>
    /// Checks out a book to a library member, marking it as unavailable and adding it to the member's borrowed books
    /// </summary>
    /// <param name="libraryMember">The member checking out the book</param>
    /// <param name="book">The book to be checked out</param>
    /// <exception cref="ArgumentException"> When an invalid member or book ID is passed </exception>
    /// <exception cref="InvalidOperationException"> For whatever other reason the book checkout failed </exception>
    void CheckoutBook(Guid memberId, Guid bookId);

    /// <summary>
    /// Takes the author and title, creates an id and then creates a book using those
    /// </summary>
    /// <param name="author">The author of the book</param>
    /// <param name="title">The title of the book</param>
    /// <returns>The <see cref="Book"/>, as created</returns>
    /// <exception cref="InvalidOperationException"> When a book can't be created</exception>
    Book CreateBook(string author, string title);

    /// <summary>
    /// Creates multiple books from a list of author-title pairs
    /// </summary>
    /// <param name="books">A list of tuples containing author and title information</param>
    /// <returns>An enumerable collection of created <see cref="Book"/> objects</returns>
    IEnumerable<Book> CreateBooks(List<(string Author, string Title)> books);

    /// <summary>
    /// Creates a member using a first name and a last name
    /// </summary>
    /// <param name="firstName">the first name of the member</param>
    /// <param name="lastName">the last name of the member</param>
    /// <returns>the <see cref="Member"/>, as created</returns>
    /// <exception cref="InvalidOperationException"> When a user can't be created</exception>
    Member CreateMember(string firstName, string lastName);

    /// <summary>
    /// Retrieves all books in the library's collection, including both available and checked-out books
    /// </summary>
    /// <returns>An enumerable collection of all <see cref="Book"/> objects in the library</returns>
    IEnumerable<Book> GetAllBooks();

    /// <summary>
    /// Retrieves all registered members of the library
    /// </summary>
    /// <returns>An enumerable collection of all <see cref="Member"/> objects</returns>
    IEnumerable<Member> GetAllMembers();

    /// <summary>
    /// Finds and returns a specific book by its unique identifier
    /// </summary>
    /// <param name="bookId">The unique ID of the book to retrieve</param>
    /// <returns>The <see cref="Book"/> with the specified ID, or null if no book is found</returns>
    Book? GetBookById(Guid bookId);

    /// <summary>
    /// Searches for books with a matching title
    /// </summary>
    /// <param name="title">The title to search for</param>
    /// <returns>An enumerable collection of <see cref="Book"/> objects with matching titles</returns>
    IEnumerable<Book> GetBooksByTitle(string title);

    /// <summary>
    /// Searches for books written by a specific author
    /// </summary>
    /// <param name="author">The author name to search for</param>
    /// <returns>An enumerable collection of <see cref="Book"/> objects by the specified author</returns>
    IEnumerable<Book> GetBooksByAuthor(string author);
    
    /// <summary>
    /// Retrieves all books currently checked out by a specific member
    /// </summary>
    /// <param name="libraryMember">The member whose borrowed books to retrieve</param>
    /// <returns>An enumerable collection of <see cref="Book"/> objects currently borrowed by the member</returns>
    IEnumerable<Book> GetBooksCheckedOutByMember(Member libraryMember);

    /// <summary>
    /// Finds and returns a specific library member by their unique identifier
    /// </summary>
    /// <param name="id">The unique ID of the member to retrieve</param>
    /// <returns>The <see cref="Member"/> with the specified ID, or null if no member is found</returns>
    Member? GetMemberById(Guid id);

    /// <summary>
    /// Returns a checked-out book to the library, marking it as available and removing it from the member's borrowed books
    /// </summary>
    /// <param name="memberGuid">The member returning the book</param>
    /// <param name="bookGuid">The book being returned</param>
    /// <exception cref="ArgumentException"> When an invalid member or book ID is passed </exception>
    /// <exception cref="InvalidOperationException"> For whatever other reason the book checkout failed </exception>
    void ReturnBook(Guid memberGuid, Guid bookGuid);
}