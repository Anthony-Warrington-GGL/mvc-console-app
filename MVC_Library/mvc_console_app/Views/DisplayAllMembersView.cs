using mvc_console_app;
using mvc_console_app.Controllers;

public class DisplayAllMembersView
{
    private LibraryController Controller {get;}
    private IUserInterface Ui {get;}

    public DisplayAllMembersView (LibraryController controller, IUserInterface ui)
    {
        Controller = controller;
        Ui = ui;
    }

    // present method to present all members
    public void Present()
    {        
        var members = Controller.GetAllMembers();
        
        var membersAsStrings = new List<string>();
        foreach (var member in members)
        {
            if (member != null)
            {
                // Include information about borrowed books?
                // This concern could potentially be extracted and moved somewhere else because
                // this method should only have one concern
                int borrowedCount = member.BorrowedBooks.Count;
                string bookInfo = borrowedCount == 0 
                    ? "No books checked out" 
                    : $"{borrowedCount} book(s) checked out";
                    
                membersAsStrings.Add($"ID: {member.Id} - {member.LastName}, {member.FirstName} \n Books: ({bookInfo})");
            }
        }

        var listToPresent = membersAsStrings.Count == 0
            ? new List<string> { "No members found" }
            : membersAsStrings;

        Ui.PresentItems("All Library Members", listToPresent);
    }
}