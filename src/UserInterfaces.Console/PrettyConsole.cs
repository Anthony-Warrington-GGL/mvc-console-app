using MvcLibrary.UserInterfaces.Abstractions;

namespace UserInterfaces.ConsoleUI;

public class PrettyConsole : IUserInterface
{
    private BorderMap BorderStyle { get; }

    public PrettyConsole(BorderMap borderStyle)
    {
        BorderStyle = borderStyle;
    }

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
        DrawBorderedBox(title, items);
    }

    public int PresentMenu(string title, List<string> options)
    {
        DrawBorderedBox(title, options.Select((option, index) => $"{index + 1}. {option}"));

        Console.Write("Please select an option by entering the corresponding number: ");
        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Count)
            {
                return choice;
            }
            Console.Write("Invalid input. Please enter a number between 1 and " + options.Count + ": ");
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
        int boxWidth = contentWidth + 4; // Adding padding and borders

        // Draw top border
        Console.Write(BorderStyle.TopLeftCorner);
        Console.Write(new string(BorderStyle.HorizontalEdge, boxWidth - 2));
        Console.WriteLine(BorderStyle.TopRightCorner);

        // Draw title
        Console.Write(BorderStyle.VerticalEdge);
        Console.Write(" " + title.PadRight(boxWidth - 4) + " ");
        Console.WriteLine(BorderStyle.VerticalEdge);

        // Draw separator
        Console.Write(BorderStyle.LeftIntersection);
        Console.Write(new string(BorderStyle.HorizontalInner, boxWidth - 2));
        Console.WriteLine(BorderStyle.RightIntersection);

        // Draw content
        foreach (var line in content)
        {
            Console.Write(BorderStyle.VerticalEdge);
            Console.Write(" " + line.PadRight(boxWidth - 4) + " ");
            Console.WriteLine(BorderStyle.VerticalEdge);
        }

        // Draw bottom border
        Console.Write(BorderStyle.BottomLeftCorner);
        Console.Write(new string(BorderStyle.HorizontalEdge, boxWidth - 2));
        Console.WriteLine(BorderStyle.BottomRightCorner);
    }
}