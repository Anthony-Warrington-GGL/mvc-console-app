namespace mvc_console_app.Views;

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
    /// Presents the provided items
    /// </summary>
    /// <param name="title">the title for the display</param>
    /// <param name="items">the items to present</param>
    /// <exception cref="NotImplementedException"></exception>
    public void PresentItems(string title, IEnumerable<T> items)
    {
        throw new NotImplementedException();
    }
}