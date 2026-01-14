using mvc_console_app.Controllers;
using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

public class GetMemberView
{
    private LibraryController Controller {get;}
    private IUserInterface Ui {get;}

    private Func<Member, string> MemberFormatter {get;}

    public GetMemberView (LibraryController controller, IUserInterface ui, Func<Member, string>? memberFormatter = null)
    {
        Controller = controller;
        Ui = ui;
        MemberFormatter = memberFormatter ?? GetMemberDescription;
    }

    public Member? GetMember()
    {
        IEnumerable<Member> allMembers = Controller.GetAllMembers();

        if (allMembers is null)
        {
            return null;
        }

        return Ui.PresentCustomItems("All members", GetMembersMenuItems(allMembers));
    }

    private string GetMemberDescription(Member member)
    {
        return member.ToString();
    }

    private List<(string Description, Member Member)> GetMembersMenuItems(IEnumerable<Member> members)
    {
        List<(string Description, Member Member)> membersTuples = [];
        
        foreach (var member in members)
        {
            membersTuples.Add((MemberFormatter(member), member));
        }
        
        return membersTuples;
    }
}