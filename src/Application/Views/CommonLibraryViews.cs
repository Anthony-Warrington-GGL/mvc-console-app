using mvc_console_app.Models;
using mvc_console_app.Views.Generic;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views;

/// <summary>
/// Helper methods for creating commonly used views
/// </summary>
public static class CommonLibraryViews
{
    /// <summary>
    /// Creates a view for selecting a single member
    /// </summary>
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
    /// Creates a view for selecting a single book
    /// </summary>
    /// <param name="ui">the user interface to present the view with</param>
    /// <returns>a view configured to select a book</returns>
    public static GetItemView<Book> GetBookView(IUserInterface ui)
    {
        return new GetItemView<Book>
        (
            itemFormatter: book => book.ToString(),
            ui: ui
        );
    }

    public static DisplayItemsView<Member> DisplayMembersView(IUserInterface ui)
    {
        return new DisplayItemsView<Member>
        (
            itemFormatter: member => member.ToString(),
            ui: ui
        );
    }

    public static DisplayItemsView<Book> DisplayBooksView(IUserInterface ui)
    {
        return new DisplayItemsView<Book>
        (
            itemFormatter: book => book.ToString(),
            ui: ui
        );
    }
    
    /// <summary>
    /// Gets a view for selecting a single member,
    /// where members with checked out books have those books listed in the display string.
    /// </summary>
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
}