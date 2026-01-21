using mvc_console_app.Controllers;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views;

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

       try
       {
            var member = Controller.CreateMember(firstName, lastName);
            Ui.PresentItems("Success", 
               [$"Member {member.FirstName} {member.LastName} (ID: {member.Id}) has been added to the library."]);            
       }
       catch
       {
           throw new InvalidOperationException("Couldn't add member.");
       }
    }
}