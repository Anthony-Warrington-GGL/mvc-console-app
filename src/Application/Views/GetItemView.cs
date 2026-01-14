using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views;

/// <summary>
/// A generic view that can retrieve a set of items and present them for selection
/// </summary>
/// <typeparam name="T">the type of item to retrieve, present, and select</typeparam>
public class GetItemView<T> where T : class
{
    /// <summary>
    /// The function that retrieves the items to present
    /// </summary>
    private Func<IEnumerable<T>> GetItemsFunc { get; }

    /// <summary>
    /// The function that formats an item for display
    /// </summary>
    private Func<T, string> ItemFormatter { get; }

    /// <summary>
    /// The user interface to present the items with
    /// </summary>
    private IUserInterface UI { get; }

    /// <summary>
    /// Creates a new instance of <see cref="GetItemView{T}"/>,
    /// with a specified function to get all items,
    /// a function to format each item for display,
    /// and a user interface to present the items with.
    /// </summary>
    /// <param name="getItemsFunc">the function that retrieves all items</param>
    /// <param name="itemFormatter">the function that formats an item for display</param>
    /// <param name="ui">the user interface to present the items with</param>
    /// <exception cref="ArgumentNullException">thrown if any argument is null</exception>
    public GetItemView(Func<IEnumerable<T>> getItemsFunc, Func<T, string> itemFormatter, IUserInterface ui)
    {
        ArgumentNullException.ThrowIfNull(getItemsFunc);
        ArgumentNullException.ThrowIfNull(itemFormatter);
        ArgumentNullException.ThrowIfNull(ui);

        GetItemsFunc = getItemsFunc;
        ItemFormatter = itemFormatter;
        UI = ui;
    }

    /// <summary>
    /// Presents the items and allows the user to select one
    /// </summary>
    /// <param name="title">an optional title for the selection menu</param>
    /// <returns>the selected item, or null if no items are available</returns>
    public T? GetItem(string title = "Select an item")
    {
        var items = GetItemsFunc();
        if (items is null)
        {
            return null;
        }
        var customItems = GetCustomItems(items);
        return UI.PresentCustomItems(title, customItems);
    }

    /// <summary>
    /// Creates the custom items tuples required for presenting
    /// </summary>
    /// <param name="items">the items to create custom items for</param>
    /// <returns>the list of custom items tuples</returns>
    private List<(string Description, T Item)> GetCustomItems(IEnumerable<T> items)
    {
        List<(string Description, T Item)> itemsTuples = [];

        foreach (var item in items)
        {
            itemsTuples.Add((ItemFormatter(item), item));
        }
        return itemsTuples;
    }

}