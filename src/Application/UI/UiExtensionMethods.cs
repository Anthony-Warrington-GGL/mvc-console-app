using mvc_console_app.Models;
using mvc_console_app.Views;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.UI;

/// <summary>
/// Extension methods for <see cref="IUserInterface"/>
/// </summary>
public static class UiExtensionMethods
{
    /// <summary>
    /// Creates a view for selecting a single member
    /// </summary>
    /// <param name="ui">the user interface to present the view with</param>
    /// <returns>a view configured to select a member</returns>
    public static GetItemView<Member> GetMemberView(this IUserInterface ui)
    {
        return new GetItemView<Member>
        (
            itemFormatter: member => $"{member}",
            ui: ui
        );
    }
}