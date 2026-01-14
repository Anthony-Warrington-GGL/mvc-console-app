using mvc_console_app.Controllers;
using MvcLibrary.UserInterfaces.Abstractions;

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
        string firstName = Ui.GetString("Enter member's first name: ");
        string lastName = Ui.GetString("Enter member's last name: ");

//        if (member is not null)
//        {
//            Ui.PresentItems("Success", 
//                [$"Member {member.FirstName} {member.LastName} (ID: {member.Id}) has been added to the library."]);            
//        }
//        else
//        {
//            throw new InvalidOperationException("Couldn't add member.");
//        }

        try
        {            
            Controller.CreateMember(firstName, lastName);
        }
        catch
        {
            
        }
    }
}