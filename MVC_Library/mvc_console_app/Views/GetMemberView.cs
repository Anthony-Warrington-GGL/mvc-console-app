using mvc_console_app.Interfaces;
using mvc_console_app.Controllers;
using mvc_console_app.Models;

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
        throw new NotImplementedException();
    }
}