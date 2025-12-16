using MvcLibrary.UserInterfaces.Abstractions;

namespace UserInterfaces.ConsoleUI;

public class BasicConsole : IUserInterface
{
    public int GetIntInput(string prompt)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        else
        {
            throw new FormatException("Input was not a valid integer.");
        }
    }

    public string GetStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();
        return input ?? string.Empty;
    }

    public T PresentCustomItems<T>(string title, List<(string Description, T item)> items)
    {
        Console.WriteLine(title);
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].Description}");
        }

        Console.Write("Select an item by number: ");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Count)
        {
            return items[choice - 1].item;
        }
        else
        {
            throw new FormatException("Invalid selection.");
        }
    }

    public void PresentItems(string title, IEnumerable<string> items)
    {
        Console.WriteLine(title);
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public int PresentMenu(string title, List<string> options)
    {
        Console.WriteLine(title);
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        Console.Write("Select an option by number: ");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Count)
        {
            return choice;
        }
        else
        {
            throw new FormatException("Invalid menu selection.");
        }
    }
}
