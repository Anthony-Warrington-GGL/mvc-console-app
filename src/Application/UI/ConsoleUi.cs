using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.UI;

public class ConsoleUi : IUserInterface
{
    public int PresentMenu(string title, List<string> options) // TODO: give a list of objects and return an object based on what is selected
    {
        WriteCenteredTitled(title);

        // present menu with options
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"\n{i + 1}. {options[i]}");
        }

        while (true)
        {
            string userInput = GetString("Enter your choice: ");
            int choiceAsInt;

            if (int.TryParse(userInput, out choiceAsInt))
            {
                if (choiceAsInt >= 1 && choiceAsInt <= options.Count)
                {
                    return choiceAsInt;
                }
            }

            Console.WriteLine($"Invalid input. Please enter a number between 1 and {options.Count}.");
        }
    }

    // Presents items, asks the user to choose one of them, then returns that item
    public T GetItem<T>(string title, List<(string Description, T item)> items)
    {
        var itemDescriptions = new List<string>();
        foreach (var pair in items)
        {
            itemDescriptions.Add(pair.Description);
        }

        var choice = PresentMenu(title, itemDescriptions);

        var index = choice - 1;

        if (index < 0 || index >= itemDescriptions.Count)
        {
            throw new InvalidOperationException("Somehow got a bad index");
        }

        return items[index].item;
    }

    // Just presents items
    public void PresentItems(string title, IEnumerable<string> items)
    {
        Console.Clear();
        WriteCenteredTitled(title);

        foreach (string item in items)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public string GetString(string prompt)
    {
        TryGetString(prompt, out string result);
        return result;
    }

    public int GetInt(string prompt)
    {
        int result;
        while (!TryGetInt(prompt, out result)) { }
        return result;
    }

    /// <inheritdoc/>
    public bool TryGetInt(string prompt, out int result)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input == null || !int.TryParse(input, out result))
        {
            result = 0;
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <inheritdoc/>
    public bool TryGetString(string prompt, out string result)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input == null)
        {
            result = string.Empty;
            return false;
        }
        else
        {
            result = input.Trim();
            return true;
        }
    }

    /// <inheritdoc/>
    public bool TryGetItem<T>(string title, List<T> items, Func<T, string> formatter, out T result)
    {
        throw new NotImplementedException();
    }

    private void WriteCenteredTitled(string title)
    {
        Console.Clear();
        var titleLength = 50;
        int padding = (titleLength - 2 - title.Length) / 2;
        int rightPadding = titleLength - 2 - title.Length - padding;

        Console.WriteLine($"┌{new string('─', titleLength - 2)}┐");
        Console.WriteLine($"│{new string(' ', padding)}{title}{new string(' ', rightPadding)}│");
        Console.WriteLine($"└{new string('─', titleLength - 2)}┘");
    }
}