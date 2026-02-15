namespace mvc_console_app.Models;

/// <summary>
/// Represents a single loan transaction in the library, recording the relationship
/// between a <see cref="Member"/> and a <see cref="Book"/> over the lifetime of a loan.
/// A <see cref="LedgerEntry"/> is created when a book is checked out and closed when
/// it is returned, providing a record of borrowing activity
/// </summary>
public class LedgerEntry
{
    /// <summary>
    /// Unique identifier for this ledger entry
    /// </summary>
    public Guid Id {get; init;}

    /// <summary>
    /// The unique identifier of the <see cref="Book"/> associated with this loan
    /// </summary>
    public Guid LoanedBookId {get; set;}

    /// <summary>
    /// The unique identifier of the <see cref="Member"/> who checked out the book
    /// </summary>
    public Guid LoaningMemberId {get; set;} 

    /// <summary>
    /// The date and time at which the book was checked out
    /// </summary>
    public DateTime CheckedOutDate {get; set;}

    /// <summary>
    /// The date and time by which the book must be returned, calculated as 14 days from <see cref="CheckedOutDate"/>
    /// </summary>
    public DateTime DueDate {get; set;}

    /// <summary>
    /// The date and time at which the book was returned, or null if the loan is still active
    /// </summary>
    public DateTime? ReturnedDate {get; set;}

    /// <summary>
    /// Whether this loan is currently active, i.e. the book has not yet been returned
    /// </summary>    
    public bool IsActive => ReturnedDate == null;

    public LedgerEntry() { }

    /// <summary>
    /// Creates a new active ledger entry for a book being checked out by a member.
    /// Sets <see cref="CheckedOutDate"/> to the current time and <see cref="DueDate"/>
    /// to 14 days from now.
    /// </summary>
    /// <param name="bookId">The unique identifier of the book being checked out</param>
    /// <param name="memberId">The unique identifier of the member checking out the book</param>
   public LedgerEntry(Guid bookId, Guid memberId)
    {
        Id = Guid.NewGuid();
        LoanedBookId = bookId;
        LoaningMemberId = memberId;
        CheckedOutDate = DateTime.Now;
        DueDate = CheckedOutDate.AddDays(14);
        ReturnedDate = null;
    }
}