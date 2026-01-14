using mvc_console_app.Controllers;
using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views;

/// <summary>
/// Helper methods for creating commonly used views
/// </summary>
public static class LibraryViews
{
    /// <summary>
    /// Creates a view for selecting a single member
    /// </summary>
    /// <param name="controller">the library controller to get members from</param>
    /// <param name="ui">the user interface to present the view with</param>
    /// <returns>a view configured to select a member</returns>
    public static GetItemView<Member> GetMemberView(IUserInterface ui)
    {
        return new GetItemView<Member>
        (
            itemFormatter: member => $"{member}",
            ui: ui
        );
    }

    /// <summary>
    /// Gets a view for selecting a single member,
    /// where members with checked out books have those books listed in the display string.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="ui"></param>
    /// <returns></returns>
    public static GetItemView<Member> GetMemberViewWithDetails(IUserInterface ui)
    {
        return new GetItemView<Member>
        (
            itemFormatter: member =>
            {
                var books = member.BorrowedBooks;
                if (books.Count != 0)
                {
                    var bookList = string.Join("\n", books.Select(b => $"    {b.Title}"));
                    return $"{member}\n  Checked out books:\n{bookList}";
                }
                else
                {
                    return $"{member}";
                }
            },
            ui: ui
        );
    }

    /// <summary>
    /// Creates a view for selecting a single book
    /// </summary>
    /// <param name="controller">the library controller to get books from</param>
    /// <param name="ui">the user interface to present the view with</param>
    /// <returns>a view configured to select a book</returns>
    public static GetItemView<Book> GetBookView(IUserInterface ui)
    {
        return new GetItemView<Book>
        (
            itemFormatter: member => member.ToString(),
            ui: ui
        );
    }

}