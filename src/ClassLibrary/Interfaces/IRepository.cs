using System.Net.ServerSentEvents;

namespace mvc_console_app.Interfaces;

public interface IRepository<TKey, TItem>
{
    /// <summary>
    /// Tries to create an item in the repository or, if the item already exists,
    /// overwrites it with the passed item
    /// </summary>
    /// <param name="key"> The key of the item to store or update </param>
    /// <param name="item"> The item to store or update </param>
    /// <returns> True if the item was created in the repository and was a new item, false if it already existed and was overwritten </returns>
    /// <exception cref="ArgumentNullException"> When <paramref name="item"/> is null </exception>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow storing or updating items </exception>
    public bool StoreOrUpdateItem(TKey key, TItem item);

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
    /// Deletes an item from the repository from its key
    /// </summary>
    /// <param name="key"> The key of the item to delete</param>
    /// <returns> True if the item was successfully deleted from the repository, false if not</returns>
    /// <exception cref="ArgumentNullException"> When <paramref name="key"/> is null</exception>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow deleting items</exception>
    public bool RemoveItem(TKey key);

    /// <summary>
    /// Retrieves all items from the repository
    /// </summary>
    /// <param name="items"> A collection containing all the items in the repository</param>
    /// <returns> True if any items were retrieved, false if no items were retrieved</returns>
    /// <exception cref="InvalidOperationException"> When the repository is in a state that does not allow reading items</exception>
    public IEnumerable<TItem> GetAll(); // return empty list - change to just "getall"
}