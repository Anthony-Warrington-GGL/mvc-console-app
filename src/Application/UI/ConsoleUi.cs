using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.UI;

public class ConsoleUi : IUserInterface
{
    // TODO: Are there mixed concerns which can be extracted? Is it singular in its purpose?
    public void PresentMenu(string title, List<(string Description, Action Action)> menuItems)
    {
        bool running = true;
        while (running)
        {
            WriteCenteredTitled(title);
            //menuItems.Add(("Exit", Environment.Exit(0))); // Could potentially avoid a bunch of logic below by just adding the exit-menu-item here?

            // present menu with options
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Description}");
            }

            // Print exit line
            Console.WriteLine($"{menuItems.Count() + 1}. Exit");

            string userInput = GetString("Enter your choice: ");
            int choiceAsInt;

            // Menu choice
            if (int.TryParse(userInput, out choiceAsInt) && choiceAsInt <= menuItems.Count() && choiceAsInt >= 1)
            {
                menuItems[choiceAsInt - 1].Action.Invoke();
            }
            // App exit choice
            // TODO: Is this the concern of ConsoleUi?
            // Yes - other UIs might have different ways of exiting out of the application, this concern seems tied to consoleui
            else if (choiceAsInt == menuItems.Count + 1)
            {
                // But is this coupling-up the presenting of a menu with the concept of exiting the application?
                // In doing this, has the method now become less flexible?
                // Is this now doing more than the Interface it implements says the method should do?
                Environment.Exit(0);
            }
            // Invalid input
            else
            {
                Console.WriteLine($"Invalid input. Please enter a number between 1 and {menuItems.Count() + 1}.\nPress any key to continue.");
                Console.ReadKey();
            }
        }
    }

    public int GetSelectedIndexFromUser(string title, List<string> options)
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

        var choice = GetSelectedIndexFromUser(title, itemDescriptions);

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