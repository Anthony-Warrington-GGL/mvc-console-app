namespace mvc_console_app.Interfaces;

public interface IUserInterface
{
    int GetIntInput(string prompt);
    string GetStringInput(string prompt);
    T PresentCustomItems<T>(string title, List<(string Description, T item)> items);
    void PresentItems(string title, IEnumerable<string> items);
    int PresentMenu(string title, List<string> options);    
}