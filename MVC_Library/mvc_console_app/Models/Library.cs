namespace mvc_console_app.Models;

public class LibraryModel : ILibrary
{
    private HashSet<Book> Books { get; set; } = new HashSet<Book>();

    private HashSet<Member> LibraryMembers;

    private int LastUsedId {get; set;} = 1;

    public LibraryModel()
    {
        LibraryMembers = new HashSet<Member>();
    }

    public Book CreateBook(string author, string title)
    {
        // get an id
        // create the book object
        // try to add the book to the library
        // return the book if it was successful
        throw new NotImplementedException();
    }

    public IEnumerable<Book> CreateBooks(List<(string Author, string Title)> books)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Books;
    }

    public Book? GetBookById(int id)
    {
        foreach (Book b in Books)
        {
            if (b.Id == id)
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

    public Member? GetMemberById(int memberId)
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

    public Member CreateMember(string firstName, string lastName)
    {
        // get a int for the id - one that doesn't already exist
        Member member = new Member(GetNextAvailableId(), firstName, lastName);
        
        if (LibraryMembers.Add(member))
        {
            return member;            
        }
        
        throw new InvalidOperationException("Couldn't add member.");
    }

    public bool CheckoutBook(Member member, Book book)
    {
        if (Books.Remove(book))
        {
            book.CheckedOutDate = DateTime.Now;
            book.IsAvailable = false;
            member.BorrowedBooks.Add(book);
            return true;
        }
        return false;
    }

    public bool ReturnBook(Member member, Book book)
    {
        if (member.BorrowedBooks.Remove(book))
        {
            book.CheckedOutDate = null;
            book.IsAvailable = true;
            Books.Add(book);
            return true;
        }
        return false;
    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Member member)
    {
        return member.BorrowedBooks;
    }

    private int GetNextAvailableId()
    {
        return LastUsedId++;
    }
}