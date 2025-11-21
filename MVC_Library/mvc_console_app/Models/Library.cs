namespace mvc_console_app.Models;

public class LibraryModel : ILibrary
{
    private HashSet<Book> Books { get; set; } = new HashSet<Book>();

    private HashSet<Member> LibraryMembers;

    public LibraryModel()
    {
        LibraryMembers = new HashSet<Member>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void AddBooks(IEnumerable<Book> books)
    {
        foreach(Book book in books)
        {
            Books.Add(book);
        }
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

    public void AddMember(Member member)
    {
        LibraryMembers.Add(member);
    }

    public void CheckoutBook(Member member, Book book)
    {
        if (Books.Remove(book))
        {
            book.CheckedOutDate = DateTime.Now;
            book.IsAvailable = false;
            member.BorrowedBooks.Add(book);
        }
    }

    public void ReturnBook(Member member, Book book)
    {
        if (member.BorrowedBooks.Remove(book))
        {
            book.CheckedOutDate = null;
            book.IsAvailable = true;
            Books.Add(book);
        }
    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Member member)
    {
        return member.BorrowedBooks;
    }
}