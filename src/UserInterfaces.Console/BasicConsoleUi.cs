using MvcLibrary.UserInterfaces.Abstractions;

namespace UserInterfaces.Console;

public class BasicConsoleUi : IUserInterface
{
    public int GetIntInput(string prompt)
    {
        throw new NotImplementedException();
    }

    public string GetStringInput(string prompt)
    {
        throw new NotImplementedException();
    }

    public T PresentCustomItems<T>(string title, List<(string Description, T item)> items)
    {
        throw new NotImplementedException();
    }

    public void PresentItems(string title, IEnumerable<string> items)
    {
        throw new NotImplementedException();
    }

    public int PresentMenu(string title, List<string> options)
    {
        throw new NotImplementedException();
    }
}
