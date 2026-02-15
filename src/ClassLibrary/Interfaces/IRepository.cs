using System.Net.ServerSentEvents;

namespace mvc_console_app.Interfaces;

public interface IRepository<TKey, TItem>
{
    /// <summary>
    /// Tries to create an item in the repository or, if the item already exists,
    /// overwrites it with the passed item
    /// </summary>
    /// <param name="item"> The item to store or update </param>
    /// <returns> True if the item was successfully stored in the repository, false if not</returns>
    /// <exception cref="ArgumentNullException"> When <paramref name="item"/> is null</exception>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow storing or updating items</exception>
    public bool TryStoreUpdateItem(TItem item);

    /// <summary>
    /// Tries to read an item from the repository from its key
    /// </summary>
    /// <param name="key"> The key of the item to retrieve</param>
    /// <param name="item"> The item, if it was successfully retrieved</param>
    /// <returns> True if the item was successfully retrieved from the repository, false if not</returns>
    /// <exception cref="ArgumentNullException"> When <paramref name="key"/> is null</exception>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow reading items</exception>
    public bool TryGetItem(TKey key, out TItem item);

    /// <summary>
    /// Tries to delete an item from the repository from its key
    /// </summary>
    /// <param name="key"> The key of the item to delete</param>
    /// <returns> True if the item was successfully deleted from the repository, false if not</returns>
    /// <exception cref="ArgumentNullException"> When <paramref name="key"/> is null</exception>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow deleting items</exception>
    public bool TryRemoveItem(TKey key);

    /// <summary>
    /// Tries to retrieve all items from the repository
    /// </summary>
    /// <param name="items"> A collection containing all the items in the repository</param>
    /// <returns> True if any items were retrieved, false if no items were retrieved</returns>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow reading items</exception>
    public bool TryGetAll(out IEnumerable<TItem> items);
}