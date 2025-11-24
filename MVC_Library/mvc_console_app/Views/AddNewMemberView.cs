using mvc_console_app;
using mvc_console_app.Controllers;
using mvc_console_app.Models;

public class AddNewMemberView
{
    private LibraryController Controller { get; }
    private IUserInterface Ui { get; }

    public AddNewMemberView(LibraryController controller, IUserInterface ui)
    {
        Controller = controller;
        Ui = ui;
    }

    public void Present()
    {
        string firstName = Ui.GetStringInput("Enter member's first name: ");
        string lastName = Ui.GetStringInput("Enter member's last name: ");
        int memberId = Ui.GetIntInput("Enter member ID: ");

        var newMember = new Member(memberId, firstName, lastName);
        if (Controller.AddNewMember(newMember))
        {
            Ui.PresentItems("Success", 
                [$"Member {firstName} {lastName} (ID: {memberId}) has been added to the library."]);            
        }
        else
        {
            // TODO: What if it fails because there is already a member with that ID?
            // It currently won't fail. There will now be a duplicate entry.
            throw new NotImplementedException();
        }
    }
}