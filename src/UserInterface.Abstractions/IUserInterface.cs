namespace MvcLibrary.UserInterface.Abstractions;

/// <summary>
/// An interface to a user interface that provides methods for input and output operations
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Gets an integer input from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <returns>the integer input from the user</returns>
    int GetIntInput(string prompt);

    /// <summary>
    /// Gets a string input from the user
    /// </summary>
    /// <param name="prompt">a text prompt to guide the user</param>
    /// <returns>the string input from the user</returns>
    string GetStringInput(string prompt);

    /// <summary>
    /// Presents a set of items to the user, allows them to choose one, and returns the chosen item
    /// </summary>
    /// <typeparam name="T">The type of the items to present</typeparam>
    /// <param name="title">the title to display with the items</param>
    /// <param name="items">a list of tuples containing item descriptions and the items themselves</param>
    /// <returns>the item chosen by the user</returns>
    T PresentCustomItems<T>(string title, List<(string Description, T item)> items);

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
}