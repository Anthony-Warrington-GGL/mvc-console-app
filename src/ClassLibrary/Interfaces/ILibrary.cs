using Microsoft.VisualBasic;
using mvc_console_app.Models;

namespace mvc_console_app.Interfaces;

public interface ILibrary
{    
// TODO: Go through the methods and decide which should throw exceptions

    /// <summary>
    /// Checks out a book to a library member
    /// </summary>
    /// <param name="memberId">The member checking out the book</param>
    /// <param name="bookId">The book to be checked out</param>
    /// <returns> True if the book was successfully checked out, false if it was not able to be checked out </>
    /// <exception cref="ArgumentException"> When an invalid member or book ID is passed </exception>
    /// <exception cref="InvalidOperationException"> For whatever other reason the book checkout failed </exception>
    bool CheckoutBook(Guid memberId, Guid bookId);

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
    /// <exception cref="InvalidOperationException"> When a book can't be created</exception>
    IEnumerable<Book> CreateBooks(IEnumerable<(string Title, string Author)> books);

    /// <summary>
    /// Creates a member using a first name and a last name
    /// </summary>
    /// <param name="firstName">the first name of the member</param>
    /// <param name="lastName">the last name of the member</param>
    /// <returns>the <see cref="Member"/>, as created</returns>
    /// <exception cref="InvalidOperationException"> When a user can't be created</exception>
    Member CreateMember(string firstName, string lastName);

    /// <summary>
    /// Creates multiple members from a list of firstname-lastname pairs
    /// </summary>
    /// <param name="members">A list of tuples containing firstname and lastname</param>
    /// <returns>An enumerable collection of created <see cref="Member"/> objects</returns>
    /// <exception cref="InvalidOperationException"> When a user can't be created</exception>
    IEnumerable<Member> CreateMembers(IEnumerable<(string firstName, string lastName)> members);

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
    /// <param name="memberId">The member's id</param>
    /// <returns>An enumerable collection of <see cref="Book"/> objects currently borrowed by the member</returns>
    /// <exception cref="ArgumentException"> When no member exists with <paramref name="memberId"/> </exception>
    IEnumerable<Book> GetBooksCheckedOutByMember(Guid memberId);

    /// <summary>
    /// TODO: 
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    bool TryGetBookById(Guid bookId, out Book book);

    /// <summary>
    /// TODO: 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    bool TryGetMember(Guid id, out Member member);

    /// <summary>
    /// Returns a checked-out book to the library
    /// </summary>
    /// <param name="bookGuid">The book being returned</param>
    /// <returns> True when the book was successfully returned, false if it was not checked-out </returns>
    bool TryReturnBook(Guid bookId);
    
    /// <summary>
    /// Tries to update an existing member
    /// </summary>
    /// <param name="member"> The member to update </param>
    /// <returns> True if the member was successfully updated, false if not</returns>
    bool TryUpdateMember (Member member);
}