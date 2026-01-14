namespace MvcLibrary.UserInterfaces.Abstractions;

/// <summary>
/// An interface to a user interface that provides methods for input and output operations
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets an integer from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <returns>the integer input from the user</returns>
    int GetInt(string prompt);

    /// <summary>
    /// Gets a string from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <returns>the string input from the user</returns>
    string GetString(string prompt);

    /// <summary>
    /// Gets an item from the user from a list of items
    /// </summary>
    /// <typeparam name="T">The type of the items to present</typeparam>
    /// <param name="title">the title to display with the items</param>
    /// <param name="items">a list of tuples containing item descriptions and the items themselves</param>
    /// <returns>the item chosen by the user</returns>
    T GetItem<T>(string title, List<(string Description, T item)> items);

    /// <summary>
    /// Presents a set of items to the user
    /// </summary>
    /// <param name="title">the title to display with the items</param>
    /// <param name="items">a collection of item descriptions to present</param>
    void PresentItems(string title, IEnumerable<string> items);

    /// <summary>
    /// Presents a menu to the user and returns the index of the selected option
    /// </summary>
    /// <param name="title">the title to display with the menu</param>
    /// <param name="options">a list of menu option descriptions</param>
    /// <returns>the base-1 index of the option selected by the user</returns>
    int PresentMenu(string title, List<string> options);

    /// <summary>
    /// Tries to get an integer from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <param name="result">when the method returns, contains the integer input from the user if the input was valid; otherwise, zero</param>
    /// <returns>true if the user provided a valid integer; otherwise, false</returns>
    bool TryGetInt(string prompt, out int result);

    /// <summary>
    /// Tries to get a string from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <param name="result">when the method returns, contains the string input from the user if the input was valid; otherwise, null</param>
    /// <returns>>true if the user provided a valid string; otherwise, false</returns>
    bool TryGetString(string prompt, out string result);

    /// <summary>
    /// Tries to get an item from the user from a list of items
    /// </summary>
    /// <typeparam name="T">The type of the items to present</typeparam>
    /// <param name="title">the title to display with the items</param>
    /// <param name="items">a list of items to present</param>
    /// <param name="formatter">a function that formats an item for display</param>
    /// <param name="result">when the method returns, contains the item chosen by the user if the input was valid; otherwise, null</param>
    /// <returns>>true if the user selected a valid item; otherwise, false</returns>
    bool TryGetItem<T>(string title, List<T> items, Func<T, string> formatter, out T result);
}