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
        // check if the member exists
        if (!MemberExists(memberGuid))
        {
            Console.WriteLine($"Book checkout failed: Member {memberGuid} does not exist.");
            return false;
        }

        // check if the book exists
        if (!BookExists(bookGuid))
        {
            Console.WriteLine($"Book checkout failed: Book {bookGuid} does not exist.");
            return false;
        }

        // check if the book is available
        if (!BookIsAvailable(bookGuid))
        {
            Console.WriteLine($"Book checkout failed: Book {bookGuid} is not available for loan.");
            return false;
        }

        // make a ledger entry for the loan
        MakeLedgerEntry(memberGuid, bookGuid, out _);
        return true;
    }

    public bool TryReturnBook(Guid bookGuid)
    {
        // check if the book exists
        if (!BookExists(bookGuid))
        {
            Console.WriteLine($"Book return failed: Book {bookGuid} does not exist.");
            return false;
        }

        // find the active ledger entry for this book
        if (!TryGetActiveLedgerEntryForBook(bookGuid, out var ledgerEntry))
        {
            Console.WriteLine($"Book return failed: No active loan found for book {bookGuid}.");
            return false;
        }

        // mark the book as returned in the ledger entry
        MarkBookAsReturned(ledgerEntry);

        return true;
    }

    public IEnumerable<Book> GetBooksCheckedOutByMember(Guid memberId)
    {
        // create a collection to hold the books
        List<Book> checkedOutBooks = [];

        // get all ledger entries
        var allLedgerEntries = LedgerRepository.Items;

        // for each ledger entry
        foreach (var entry in allLedgerEntries)
        {
            // check if this entry is for the given member
            if (LedgerEntryIsForMember(entry, memberId))
            {
                // check if the book is still checked out (not returned)
                if (BookIsCheckedOut(entry))
                {
                    // get the book and add it to the collection
                    if (TryGetBookById(entry.LoanedBookId, out var book))
                    {
                        checkedOutBooks.Add(book);
                    }
                }
            }
        }

        // return the collection
        return checkedOutBooks;
    }

    public bool TryUpdateMember(Member member)
    {
        // check if the member exists in the repository
        if (!MemberExists(member.Id))
        {
            Console.WriteLine($"Member update failed: Member {member.Id} does not exist.");
            return false;
        }

        // update the member in the repository
        UpdateMemberInRepository(member);

        return true;
    }

    /// <summary>
    /// Checks if a member exists in the repository
    /// </summary>
    /// <param name="memberId"> The ID of the member to check </param>
    /// <returns> True if the member exists, false otherwise </returns>
    private bool MemberExists(Guid memberId)
    {
        return MemberRepository.TryGetItem(memberId, out _);
    }

    /// <summary>
    /// Checks if a book exists in the repository
    /// </summary>
    /// <param name="bookId"> The ID of the book to check </param>
    /// <returns> True if the book exists, false otherwise </returns>
    private bool BookExists(Guid bookId)
    {
        return BookRepository.TryGetItem(bookId, out _);
    }

    /// <summary>
    /// Checks if a book is available for checkout
    /// </summary>
    /// <param name="bookId"> The ID of the book to check </param>
    /// <returns> True if the book is available, false if it's currently on loan </returns>
    private bool BookIsAvailable(Guid bookId)
    {
        // get all ledger entries for this book
        var entriesForBook = GetLedgerEntriesForBook(bookId);

        // check if any entry indicates the book is currently on loan
        foreach (var entry in entriesForBook)
        {
            if (EntryIndicatesBookIsOnLoan(entry))
            {
                // book is not available
                return false;
            }
        }

        // if no entries indicate the book is on loan, it's available
        return true;
    }

    /// <summary>
    /// Gets all ledger entries associated with a specific book
    /// </summary>
    /// <param name="bookId"> The ID of the book to find entries for </param>
    /// <returns> An enumerable of all ledger entries for the given book </returns>
    private IEnumerable<LedgerEntry> GetLedgerEntriesForBook(Guid bookId)
    {
        List<LedgerEntry> entries = [];

        foreach (var entry in LedgerRepository.Items)
        {
            if (entry.LoanedBookId == bookId)
            {
                entries.Add(entry);
            }
        }

        return entries;
    }

    /// <summary>
    /// Checks if a ledger entry indicates a book is currently on loan (not yet returned)
    /// </summary>
    /// <param name="entry"> The ledger entry to check </param>
    /// <returns> True if the entry shows the book is on loan, false if it has been returned </returns>
    private bool EntryIndicatesBookIsOnLoan(LedgerEntry entry)
    {
        return entry.ReturnedDate is null;
    }

    /// <summary>
    /// Creates and stores a new ledger entry for a book loan
    /// </summary>
    /// <param name="memberId"> The ID of the member borrowing the book </param>
    /// <param name="bookId"> The ID of the book being borrowed </param>
    /// <param name="entry"> The created ledger entry </param>
    private void MakeLedgerEntry(Guid memberId, Guid bookId, out LedgerEntry entry)
    {
        var ledgerEntry = new LedgerEntry
        {
            Id = GetUniqueGuid(LedgerRepository.Keys),
            BorrowingMemberId = memberId,
            LoanedBookId = bookId,
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
    /// Tries to get the active (unreturned) ledger entry for a given book
    /// </summary>
    /// <param name="bookId"> The ID of the book to find the ledger entry for </param>
    /// <param name="ledgerEntry"> The active ledger entry if found, otherwise default </param>
    /// <returns> True if an active ledger entry was found, false otherwise </returns>
    private bool TryGetActiveLedgerEntryForBook(Guid bookId, out LedgerEntry ledgerEntry)
    {
        // search through all ledger entries
        foreach (var entry in LedgerRepository.Items)
        {
            // check if this entry is for the given book and is not yet returned
            if (entry.LoanedBookId == bookId && entry.ReturnedDate is null)
            {
                ledgerEntry = entry;
                return true;
            }
        }

        // no active entry found
        ledgerEntry = default!;
        return false;
    }

    /// <summary>
    /// Marks a book as returned by updating its ledger entry with the return date
    /// </summary>
    /// <param name="ledgerEntry"> The ledger entry to update </param>
    private void MarkBookAsReturned(LedgerEntry ledgerEntry)
    {
        // set the returned date to now
        ledgerEntry.ReturnedDate = DateTime.Now;

        // update the ledger entry in the repository
        LedgerRepository.StoreOrUpdateItem(ledgerEntry.Id, ledgerEntry);
    }

    /// <summary>
    /// Checks if a ledger entry belongs to a specific member
    /// </summary>
    /// <param name="entry"> The ledger entry to check </param>
    /// <param name="memberId"> The member ID to check against </param>
    /// <returns> True if the entry belongs to the member, false otherwise </returns>
    private bool LedgerEntryIsForMember(LedgerEntry entry, Guid memberId)
    {
        return entry.BorrowingMemberId == memberId;
    }

    /// <summary>
    /// Checks if a book is currently checked out (not returned) based on its ledger entry
    /// </summary>
    /// <param name="entry"> The ledger entry to check </param>
    /// <returns> True if the book is still checked out, false if it has been returned </returns>
    private bool BookIsCheckedOut(LedgerEntry entry)
    {
        return entry.ReturnedDate is null;
    }

    /// <summary>
    /// Updates a member's information in the repository
    /// </summary>
    /// <param name="member"> The member with updated information </param>
    private void UpdateMemberInRepository(Member member)
    {
        MemberRepository.StoreOrUpdateItem(member.Id, member);
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