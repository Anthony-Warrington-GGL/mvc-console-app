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
    public Guid Id {get; init;} = Guid.NewGuid();

    /// <summary>
    /// The unique identifier of the <see cref="Book"/> associated with this loan
    /// </summary>
    public Guid LoanedBookId {get; set;}

    /// <summary>
    /// The unique identifier of the <see cref="Member"/> who checked out the book
    /// </summary>
    public Guid BorrowingMemberId {get; set;}

    /// <summary>
    /// The date and time at which the book was checked out
    /// </summary>
    public DateTime CheckedOutDate {get; set;}

    /// <summary>
    /// The date and time by which the book must be returned
    /// </summary>
    public DateTime DueDate {get; set;}

    /// <summary>
    /// The date and time at which the book was returned, or null if the loan is still active
    /// </summary>
    public DateTime? ReturnedDate {get; set;}
}