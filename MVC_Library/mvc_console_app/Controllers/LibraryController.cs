using mvc_console_app.Interfaces;
using mvc_console_app.Models;

namespace mvc_console_app.Controllers;

public class LibraryController
{
    private ILibrary Library { get; }

    public LibraryController(ILibrary library)
    {
        Library = library;
    }

    public Book CreateBook(string author, string title)
    {
        return Library.CreateBook(author, title);
    }

    public Member CreateMember(string firstName, string lastName)
    {
        return Library.CreateMember(firstName, lastName);
    }

    public bool CheckoutBook (Guid memberId, Guid bookId)
    {
        try
        {
            Library.CheckoutBook(memberId, bookId);
            return true;
        }
        catch(ArgumentException)
        {
            return false;
        }
        catch(InvalidOperationException)
        {
            return false;
        }
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Library.GetAllBooks();
    }

    public IEnumerable<Member> GetAllMembers()
    {       
        return Library.GetAllMembers();
    }

    public Book? GetBookById(Guid id)
    {
        return Library.GetBookById(id);
    }

    public IEnumerable<Book> GetBooksByAuthor(string author) => Library.GetBooksByAuthor(author);

    public IEnumerable<Book> GetBooksByTitle(string title) => Library.GetBooksByTitle(title);

    public bool ReturnBook(Member? member, Book? book)
    {        
        if (member is null || book is null)
        {
            return false;
        }    
        return false;
        //return Library.ReturnBook(member.Id, book.Id);
    }
}