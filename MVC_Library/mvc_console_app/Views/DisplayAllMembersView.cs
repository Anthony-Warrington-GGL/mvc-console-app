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
        throw new NotImplementedException();
    }
}