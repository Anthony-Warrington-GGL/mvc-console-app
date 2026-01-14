using mvc_console_app.Models;
using mvc_console_app.Controllers;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views;

public class DisplayAllMembersView
{
    private LibraryController Controller {get;}
    private IUserInterface Ui {get;}

    public DisplayAllMembersView (LibraryController controller, IUserInterface ui)
    {
        Controller = controller;
        Ui = ui;
    }

    public void Present()
    {
        var members = Controller.GetAllMembers();
        var displayItems = GetDisplayItems(members);
        
        Ui.PresentItems("All Library Members", displayItems);
    }

    // responsible for putting the members into a list of strings
    private List<string> GetDisplayItems(IEnumerable<Member> members)
    {
        var memberDescriptions = new List<string>();
    
        foreach (var member in members)
        {
            if (member != null)
            {
                memberDescriptions.Add(FormatMemberDescription(member));
            }
        }

        return memberDescriptions.Count == 0 
            ? new List<string> { "No members found" }
            : memberDescriptions;
    }

    // responsible for creating a string from a member's information
    private string FormatMemberDescription(Member member)
    {
        string bookInfo = GetBorrowedBooksInfo(member);
        return $"ID: {member.Id} - {member.LastName}, {member.FirstName} \n Books: ({bookInfo})";
    }

    // responsible for returning a certain string based on whether a member has books checked out or not
    private string GetBorrowedBooksInfo(Member member)
    {
        int borrowedCount = member.BorrowedBooks.Count;
        
        return borrowedCount == 0 
            ? "No books checked out" 
            : $"{borrowedCount} book(s) checked out";
    }
}