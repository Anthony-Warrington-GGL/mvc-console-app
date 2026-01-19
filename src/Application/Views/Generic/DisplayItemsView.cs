using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views.Generic;

// TODO: implement this class
//
// You may refer to GetItemView.cs for guidance

/// <summary>
/// A generic view that presents a list of items
/// </summary>
/// <typeparam name="T">the type of item to present</typeparam>
public class DisplayItemsView<T> where T : class
{
    /// <summary>
    /// The function that formats an item for display
    /// </summary>
    private Func<T, string> ItemFormatter { get; }

    private IUserInterface UI {get;}

    public DisplayItemsView (Func<T, string> itemFormatter, IUserInterface ui)
    {        
        ArgumentNullException.ThrowIfNull(itemFormatter);
        ArgumentNullException.ThrowIfNull(ui);

        ItemFormatter = itemFormatter;
        UI = ui;
    }

    /// <summary>
    /// Presents the provided items
    /// </summary>
    /// <param name="title">the title for the display</param>
    /// <param name="items">the items to present</param>
    /// <exception cref="NotImplementedException"></exception>
    public void PresentItems(string title, IEnumerable<T> items)
    {
        var displayItems = GetDisplayItems(items);
        UI.PresentItems($"{title}", displayItems);
    }

    private List<string> GetDisplayItems(IEnumerable<T> items)
    {
        var itemDescriptions = new List<string>();

        foreach (var item in items)
        {
            if (item != null)
            {
                itemDescriptions.Add(FormatItemDescription(item));
            }
        }

        return itemDescriptions.Count == 0
            ? new List<string> {"No items found"}
            : itemDescriptions;
    }

    private string FormatItemDescription(T item)
    {
        return ItemFormatter(item);
    }
}