using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

public class LibraryModel : ILibrary
{
    private HashSet<Book> Books { get; set; } = [];

    private HashSet<Member> LibraryMembers {get;} = [];

    private IGuidManager GuidManager {get;}

    public LibraryModel(IGuidManager guidManager)
    {
        GuidManager = guidManager;
    }

    /// <inheritdoc/>
    public Book CreateBook(string author, string title)
    {
        Book book = new Book(GuidManager.GetNewGuid(), title, author);

        if (Books.Add(book))
        {
            return book;
        }
        throw new InvalidOperationException("Couldn't add book to library.");
    }

    public IEnumerable<Book> CreateBooks(IEnumerable<(string Title, string Author)> books)
    {
        var createdBooks = new List<Book>();
        
        foreach (var (title, author) in books)
        {
            Book book = CreateBook(author, title);
            createdBooks.Add(book);
        }
        
        return createdBooks;
    }

    public Member CreateMember(string firstName, string lastName)
    {
        // get a int for the id - one that doesn't already exist
        Member member = new Member(GuidManager.GetNewGuid(), firstName, lastName);
        
        if (LibraryMembers.Add(member))
        {
            return member;            
        }
        
        throw new InvalidOperationException("Couldn't add member.");
    }

    public IEnumerable<Member> CreateMembers(IEnumerable<(string firstName, string lastName)> members)
    {
        var createdMembers = new List<Member>();

        foreach (var (firstName, lastName) in members)
        {
            Member member = CreateMember(firstName, lastName);
            createdMembers.Add(member);
        }

        return createdMembers;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Books;
    }

    public Book? GetBookById(Guid id)
    {
        foreach (Book b in Books)
        {
            if (b.BookId == id)
            {
                return b;
            }
        }
        return null;
    }

    public IEnumerable<Book> GetBooksByTitle(string title)
    {
        HashSet<Book> booksFound = [];
        foreach (Book b in Books)
        {
            if (b.Title == title)
            {
                booksFound.Add(b);
            }
        }
        return booksFound;
    }

    public IEnumerable<Book> GetBooksByAuthor(string author)
    {
        HashSet<Book> booksFound = [];
        foreach (Book b in Books)
        {
            if (b.Author == author)
            {
                booksFound.Add(b);
            }
        }
        return booksFound;
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return LibraryMembers;
    }

    public Member? GetMemberById(Guid memberId)
    {
        foreach (Member lm in LibraryMembers)
        {
            if (lm.Id == memberId)
            {
                return lm;
            }
        }
        return null;
    }

    public void CheckoutBook(Guid memberGuid, Guid bookGuid)
    {
        Book? book = GetBookById(bookGuid);
        Member? member = GetMemberById(memberGuid);
        if (book is null || member is null)
        {
            throw new ArgumentException("Book or member not found.");
        }
        
        if (Books.Remove(book))
        {
            book.CheckedOutDate = DateTime.Now;
            member.BorrowedBooks.Add(book);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void ReturnBook(Guid memberGuid, Guid bookId)
    {
        Member? member = GetMemberById(memberGuid);
        Book? book = GetBookFromCollection(bookId, member?.BorrowedBooks ?? []);
        
        if (book is null || member is null)
        {
            throw new ArgumentException("Book or member not found.");
        }

        if (member.BorrowedBooks.Remove(book))
        {
            book.CheckedOutDate = null;
            Books.Add(book);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Guid memberId)
    {
        Member? member = GetMemberById(memberId);

        if (member is null)
        {
            throw new ArgumentException("Member not found.");
        }

        return [.. member.BorrowedBooks];
    } 

    private Book? GetBookFromCollection(Guid bookId, IEnumerable<Book> books)
    {
        foreach (Book book in books)
        {
            if (book.BookId == bookId)
            {
                return book;
            }
        }
        return null;
    }
}