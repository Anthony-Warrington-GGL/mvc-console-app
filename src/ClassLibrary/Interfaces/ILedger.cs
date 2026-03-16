namespace mvc_console_app.Interfaces;

/// <summary>
/// Is responsible for keeping a record of when books are checked out and returned
/// </summary>
public interface ILedger
{
    /// <summary>
    /// Tries to record a book as being checked out. 
    /// </summary>
    /// <param name="bookId"> The id of the book being checked out </param>
    /// <param name="memberId"> The id of the member checking out the book </param>
    /// <returns> True if the book wasn't already checked out; otherwise false </returns>
    public bool TryRecordBookCheckout(Guid bookId, Guid memberId);

    /// <summary>
    /// Tries to record a book as being returned.
    /// </summary>
    /// <param name="bookId"> The id of the book being returned </param>
    /// <returns> True if the book was already checked out; otherwise false </returns>
    public bool TryRecordBookReturn(Guid bookId);

    /// <summary>
    /// Checks if the book is available
    /// </summary>
    /// <param name="bookId"> The id of the book </param>
    /// <returns> True if the book is not checked out; otherwise false </returns>
    public bool IsBookAvailable(Guid bookId);
}