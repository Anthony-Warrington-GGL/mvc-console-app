using MvcLibrary.UserInterfaces.Abstractions;

namespace UserInterfaces.ConsoleUI;

public class PrettyConsole : IUserInterface
{
    private BorderStyle BorderStyle { get; }

    public PrettyConsole(BorderStyle? borderStyle = null)
    {
        BorderStyle = borderStyle ?? BorderStyles.DoubleWithSingleInner;
    }

    public int GetInt(string prompt)
    {
        // draw a box with the prompt and get int input, retrying until the input is valid
        DrawBorderedBox("Input Required", new List<string> { prompt });
        Console.Write("Please enter an integer: ");
        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            Console.Write("Invalid input. Please enter a valid integer: ");
        }
    }

    public string GetString(string prompt)
    {
        DrawBorderedBox("Input Required", new List<string> { prompt });
        Console.Write("Please enter your input: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public T GetItem<T>(string title, List<(string Description, T item)> items)
    {
        int selectedIndex = 0;
        while (true)
        {
            Console.Clear();
            DrawBorderedBox(title, items.Select((item, index) =>
                index == selectedIndex ? $"> {item.Description} <" : $"  {item.Description}  "));
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + items.Count) % items.Count;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % items.Count;
            }
            else if (key == ConsoleKey.Enter)
            {
                return items[selectedIndex].item;
            }
        }
    }

    public void PresentItems(string title, IEnumerable<string> items)
    {
        DrawBorderedBox(title, items);
    }

    public int GetSelectedIndexFromUser(string title, List<string> options)
    {
        
        while (true)
        {
            DrawTitledBoxWithInputArea(title, options.Select((opt, index) => $"{index + 1}. {opt}"), ">");
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int selection) && selection >= 1 && selection <= options.Count)
            {
                return selection; // return one-based index
            }
            Console.Write("Invalid selection. Please any key to retry...");
            Console.ReadKey(true);
        }
    }

    /// <summary>
    /// Draws a bordered box around the given content using the specified title and border style
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    private void DrawBorderedBox(string title, IEnumerable<string> content)
    {
        // Calculate the width of the box
        int contentWidth = Math.Max(title.Length, content.Max(line => line.Length));
        // let's use the width of the console if it's less than the content width
        contentWidth = Math.Min(contentWidth, Console.WindowWidth - 4);
        int boxWidth = contentWidth + 4; // Adding padding and borders

        // Draw top border
        Console.Write(BorderStyle.CornerTopLeft);
        Console.Write(new string(BorderStyle.EdgeHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.CornerTopRight);

        // Draw title
        Console.Write(BorderStyle.EdgeVertical);
        Console.Write(" " + title.PadRight(boxWidth - 4) + " ");
        Console.WriteLine(BorderStyle.EdgeVertical);

        // Draw separator
        Console.Write(BorderStyle.IntersectionLeft);
        Console.Write(new string(BorderStyle.InnerHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.IntersectionRight);

        // Draw content
        foreach (var line in content)
        {
            Console.Write(BorderStyle.EdgeVertical);
            Console.Write(" " + line.PadRight(boxWidth - 4) + " ");
            Console.WriteLine(BorderStyle.EdgeVertical);
        }

        // Draw bottom border
        Console.Write(BorderStyle.CornerBottomLeft);
        Console.Write(new string(BorderStyle.EdgeHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.CornerBottomRight);
    }

    private void DrawTitledBoxWithInputArea(string title, IEnumerable<string> items, string prompt)
    {
        // Calculate the width of the box
        int contentWidth = Math.Max(title.Length, Math.Max(items.Max(line => line.Length), prompt.Length + 10));
        int boxWidth = contentWidth + 4; // Adding padding and borders

        // Draw top border
        Console.Write(BorderStyle.CornerTopLeft);
        Console.Write(new string(BorderStyle.EdgeHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.CornerTopRight);

        // Draw title
        Console.Write(BorderStyle.EdgeVertical);
        Console.Write(" " + title.PadRight(boxWidth - 4) + " ");
        Console.WriteLine(BorderStyle.EdgeVertical);

        // Draw separator
        Console.Write(BorderStyle.IntersectionLeft);
        Console.Write(new string(BorderStyle.InnerHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.IntersectionRight);

        // Draw items
        foreach (var line in items)
        {
            Console.Write(BorderStyle.EdgeVertical);
            Console.Write(" " + line.PadRight(boxWidth - 4) + " ");
            Console.WriteLine(BorderStyle.EdgeVertical);
        }

        // Draw separator before input area
        Console.Write(BorderStyle.IntersectionLeft);
        Console.Write(new string(BorderStyle.InnerHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.IntersectionRight);

        // Draw input prompt area
        Console.Write(BorderStyle.EdgeVertical);
        Console.Write(" " + prompt.PadRight(boxWidth - 4) + " ");
        Console.WriteLine(BorderStyle.EdgeVertical);

        // Draw bottom border
        Console.Write(BorderStyle.CornerBottomLeft);
        Console.Write(new string(BorderStyle.EdgeHorizontal, boxWidth - 2));
        Console.WriteLine(BorderStyle.CornerBottomRight);

        // set cursor position for user input
        Console.SetCursorPosition(3 + prompt.Length, Console.CursorTop - 2);
    }

    public bool TryGetInt(string prompt, out int result)
    {
        throw new NotImplementedException();
    }

    public bool TryGetString(string prompt, out string result)
    {
        throw new NotImplementedException();
    }

    public bool TryGetItem<T>(string title, List<T> items, Func<T, string> formatter, out T result)
    {
        throw new NotImplementedException();
    }

    public void PresentMenu(string title, List<(string Description, Action Action)> menuItems)
    {
        throw new NotImplementedException();
    }
}