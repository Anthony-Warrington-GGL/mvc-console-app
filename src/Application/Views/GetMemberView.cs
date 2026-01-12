using mvc_console_app.Controllers;
using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

public class GetMemberView
{
    private LibraryController Controller {get;}
    private IUserInterface Ui {get;}

    public GetMemberView (LibraryController controller, IUserInterface ui)
    {
        Controller = controller;
        Ui = ui;
    }

    public Member? GetMember()
    {
        IEnumerable<Member> allMembers = Controller.GetAllMembers();

        if (allMembers is not null)
        {
            List<(string, Member)> membersToDisplay = [];
            foreach(Member member in allMembers)
            {
                membersToDisplay.Add((member.ToString(), member));
            }           

            return Ui.PresentCustomItems("All members", membersToDisplay);            
        }
        
        return null; 
    }
}