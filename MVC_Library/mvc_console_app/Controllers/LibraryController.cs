using System.Data.Common;
using System.Runtime.CompilerServices;
using mvc_console_app.Models;
using mvc_console_app.Views;

namespace mvc_console_app.Controllers;

public class LibraryController
{
    private ILibrary Library { get; }

    public LibraryController(ILibrary library)
    {
        Library = library;
    }

    public void AddBook(string author, string title)
    {
        var rand = new Random();
        var id = rand.Next(0, 100);
        Library.AddBook(new Book(id, title, author));
    }

    public Member CreateNewMember(string firstName, string lastName)
    {
        return Library.CreateMember(firstName, lastName);
    }

    public bool CheckoutBook (Member? member, Book? book)
    {    
        if (member is null || book is null)
        {
            return false;
        }    

        return Library.CheckoutBook(member, book);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Library.GetAllBooks();
    }

    public IEnumerable<Member> GetAllMembers()
    {       
        return Library.GetAllMembers();
    }

    public Book? GetBookById(int id)
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

        return Library.ReturnBook(member, book);
    }
}