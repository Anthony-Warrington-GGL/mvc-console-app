using Microsoft.EntityFrameworkCore;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

public class EfLibraryModel : ILibrary
{
    private readonly LibraryContext Database;

    public EfLibraryModel(LibraryContext context)
    {
        Database = context;
    }

    public void CheckoutBook(Guid memberId, Guid bookId)
    {
        // Book? book = GetBookByIdPrivate(bookId);
        // Member? member = GetMemberByIdPrivate(memberId);
        // if (book is null || member is null)
        // {
        //     throw new ArgumentException("Book or member not found.");
        // }
        
        // // Set the checkout date and add to member's borrowed books
        // book.CheckedOutDate = DateTime.Now;
        // // if book is in inventory, then add book
        
        //     member.BorrowedBooks.Add(book);
        // // else, its not in stock...
        
        // Database.SaveChanges();
        throw new NotImplementedException();
    }

    public Book CreateBook(string author, string title)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> CreateBooks(IEnumerable<(string Title, string Author)> books)
    {
        throw new NotImplementedException();
    }

    public Member CreateMember(string firstName, string lastName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Member> CreateMembers(IEnumerable<(string firstName, string lastName)> members)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Member> GetAllMembers()
    {
        throw new NotImplementedException();
    }

    public Book? GetBookById(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetBooksByAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetBooksByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Guid memberId)
    {
        throw new NotImplementedException();
    }

    public Member? GetMemberById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void ReturnBook(Guid memberId, Guid bookId)
    {
        throw new NotImplementedException();
    }

    private Book? GetBookByIdPrivate(Guid bookId)
    {
        Book? book = null;
        foreach (Book b in Database.Books)
        {
            if (b.Id == bookId)
            {
                book = b;
                break;
            }
        }
        return book;
    }

    private Member? GetMemberByIdPrivate(Guid memberId)
    {
        // Member? member = null;
        // foreach (Member m in Database.Members)
        // {
        //     if (m.Id == memberId)
        //     {
        //         member = m;
        //         // Load the borrowed books for this member
        //         Database.Entry(member).Collection(m => m.BorrowedBooks).Load();
        //         break;
        //     }
        // }
        // return member;
        throw new NotImplementedException();
    }
}