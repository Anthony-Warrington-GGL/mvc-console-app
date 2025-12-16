namespace UserInterfaces.ConsoleUI;

/// <summary>
/// A struct that contains a mapping of border characters, including corners, edges, and intersections
/// </summary>
public record struct BorderMap
{
    public char TopLeftCorner { get; init; }
    public char TopRightCorner { get; init; }
    public char BottomLeftCorner { get; init; }
    public char BottomRightCorner { get; init; }
    public char HorizontalEdge { get; init; }
    public char VerticalEdge { get; init; }
    public char TopIntersection { get; init; }
    public char BottomIntersection { get; init; }
    public char LeftIntersection { get; init; }
    public char RightIntersection { get; init; }
    public char HorizontalInner { get; init; }
    public char VerticalInner { get; init; }
    public char CenterIntersection { get; init; }
}

public static class BorderMaps
{
    public static readonly BorderMap SingleLine = new()
    {
        TopLeftCorner = '┌',
        TopRightCorner = '┐',
        BottomLeftCorner = '└',
        BottomRightCorner = '┘',
        HorizontalEdge = '─',
        VerticalEdge = '│',
        TopIntersection = '┬',
        BottomIntersection = '┴',
        LeftIntersection = '├',
        RightIntersection = '┤',
        HorizontalInner = '─',
        VerticalInner = '│',
        CenterIntersection = '┼'
    };

    public static readonly BorderMap DoubleLine = new()
    {
        TopLeftCorner = '╔',
        TopRightCorner = '╗',
        BottomLeftCorner = '╚',
        BottomRightCorner = '╝',
        HorizontalEdge = '═',
        VerticalEdge = '║',
        TopIntersection = '╦',
        BottomIntersection = '╩',
        LeftIntersection = '╠',
        RightIntersection = '╣',
        HorizontalInner = '═',
        VerticalInner = '║',
        CenterIntersection = '╬'
    };

    public static readonly BorderMap DoubleWithSingleInner = new()
    {
        TopLeftCorner = '╔',
        TopRightCorner = '╗',
        BottomLeftCorner = '╚',
        BottomRightCorner = '╝',
        HorizontalEdge = '═',
        VerticalEdge = '║',
        TopIntersection = '╤',
        BottomIntersection = '╧',
        LeftIntersection = '╟',
        RightIntersection = '╢',
        HorizontalInner = '─',
        VerticalInner = '│',
        CenterIntersection = '╫'
    };
}