using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

public class LibraryModel : ILibrary
{
    private IRepository<Guid, Book> BookRepository { get; }
    private IRepository<Guid, Member> MemberRepository { get; }
    private IRepository<Guid, LedgerEntry> LedgerRepository { get; }
    private IGuidManager GuidManager { get; }

    public LibraryModel(
        IGuidManager guidManager,
        IRepository<Guid, Book> bookRepository,
        IRepository<Guid, Member> memberRepository,
        IRepository<Guid, LedgerEntry> ledgerRepository)
    {
        GuidManager = guidManager;
        BookRepository = bookRepository;
        MemberRepository = memberRepository;
        LedgerRepository = ledgerRepository;
    }

    /// <inheritdoc/>
    public Book CreateBook(string author, string title)
    {
        Guid uniqueId = GetUniqueGuid(BookRepository.Keys);
        Book book = new Book(uniqueId, title, author);
        _ = BookRepository.StoreOrUpdateItem(book.Id, book);
        return book;
    }

    /// <inheritdoc/>
    public IEnumerable<Book> CreateBooks(IEnumerable<(string Title, string Author)> books)
    {
        List<Book> createdBooks = [];

        foreach (var (title, author) in books)
        {
            createdBooks.Add(CreateBook(author, title));
        }

        return createdBooks;
    }

    /// <inheritdoc/>
    public Member CreateMember(string firstName, string lastName)
    {
        Guid uniqueId = GetUniqueGuid(MemberRepository.Keys);
        Member member = new Member(uniqueId, firstName, lastName);
        _ = MemberRepository.StoreOrUpdateItem(member.Id, member);
        return member;
    }

    /// <inheritdoc/>
    public IEnumerable<Member> CreateMembers(IEnumerable<(string firstName, string lastName)> members)
    {
        List<Member> createdMembers = [];

        foreach (var (firstName, lastName) in members)
        {
            createdMembers.Add(CreateMember(firstName, lastName));
        }

        return createdMembers;
    }

    /// <inheritdoc/>
    public IEnumerable<Book> GetAllBooks()
    {
        return BookRepository.Items;
    }

    public bool TryGetBookById(Guid id, out Book book) => BookRepository.TryGetItem(id, out book);

    public IEnumerable<Book> GetBooksByTitle(string title)
    {
        HashSet<Book> booksFound = [];
        foreach (Book book in BookRepository.Items)
        {
            if (book.Title == title)
            {
                booksFound.Add(book);
            }
        }
        return booksFound;
    }

    public IEnumerable<Book> GetBooksByAuthor(string author)
    {
        HashSet<Book> booksFound = [];
        foreach (Book book in BookRepository.Items)
        {
            if (book.Author == author)
            {
                booksFound.Add(book);
            }
        }
        return booksFound;
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return MemberRepository.Items;
    }

    public bool TryGetMember(Guid memberId, out Member member) => MemberRepository.TryGetItem(memberId, out member);

    public bool CheckoutBook(Guid memberGuid, Guid bookGuid)
    {
        // if that book or member don't exist
        if (MemberRepository.TryGetItem(memberGuid, out _) || BookRepository.TryGetItem(bookGuid, out _))
        {
            Console.WriteLine($"Book checkout failed: {memberGuid} or {bookGuid} does not exist.");
            return false;
        }
        // if the book isn't available
        else if (!BookIsAvailable(bookGuid))
        {
            Console.WriteLine($"Book checkout failed: {bookGuid} is not available for loan.");
            return false;
        }
        else
        {
            // make a ledger entry for the loan
            MakeLedgerEntry(memberGuid, bookGuid, out _);
            return true;
        }
    }

    public bool TryReturnBook(Guid bookGuid)
    {
        // if that book or member don't exist
            // (throw?)
        //else
            // find the ledger entry
                // (what if the ledger entry doesn't exist?)
                // edit the ledger entry to mark the book returned
        throw new NotImplementedException();

    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Guid memberId)
    {
        // make book collection
        // get ledger entries
        // for entries including this member
        // if the  book is checked-out
        // add the book to the collection
        // return the collection
        throw new NotImplementedException();
    }    

    public bool TryUpdateMember(Member member)
    {
        throw new NotImplementedException();
    }

    private bool BookIsAvailable(Guid bookId)
    {
        // look through all ledger entries for bookId
        foreach (var entry in LedgerRepository.Items)
        {
            // if this is the book for bookId and its not been returned yet
            if (entry.LoanedBookId == bookId && entry.ReturnedDate is null)
            {
                // its not available
                return false;
            }
        }

        // otherwise, its available
        return true;
    }

    private void MakeLedgerEntry(Guid memberId, Guid bookid, out LedgerEntry entry)
    {
        var ledgerEntry = new LedgerEntry
        {
            Id = GetUniqueGuid(LedgerRepository.Keys),
            BorrowingMemberId = memberId,
            LoanedBookId = bookid,
            CheckedOutDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14), // set due date to 2 weeks from now
            ReturnedDate = null
        };

        entry = ledgerEntry;

        if (LedgerRepository.StoreOrUpdateItem(ledgerEntry.Id, ledgerEntry))
        {
            Console.WriteLine($"The ledger entry {ledgerEntry.Id} overwrote another entry with the same ID while being stored in the repository.");
        }
    }

    /// <summary>
    /// Gets a unique GUID guaranteed not to be in a set of existing GUIDs
    /// </summary>
    /// <param name="existingGuids"> The given set of existing guids </param>
    /// <returns> The unique GUID </returns>
    private Guid GetUniqueGuid(IReadOnlySet<Guid> existingGuids)
    {
        Guid id = GuidManager.GetNewGuid();

        while (existingGuids.Contains(id))
        {
            id = GuidManager.GetNewGuid();
        }
        return id;
    }
}