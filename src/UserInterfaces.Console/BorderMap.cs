namespace UserInterfaces.ConsoleUI;

/// <summary>
/// A struct that contains a mapping of border characters, including corners, edges, and intersections
/// </summary>
public readonly record struct BorderMap
{
    /// <summary>
    /// The character for the bottom left corner of the box
    /// </summary>
    public required char CornerBottomLeft { get; init; }

    /// <summary>
    /// The character for the bottom right corner of the box
    /// </summary>
    public required char CornerBottomRight { get; init; }

    /// <summary>
    /// The character for the top left corner of the box
    /// </summary>
    public required char CornerTopLeft { get; init; }

    /// <summary>
    /// The character for the top right corner of the box
    /// </summary>
    public required char CornerTopRight { get; init; }

    /// <summary>
    /// The character for non-intersecting horizontal edges of the box
    /// </summary>
    public required char EdgeHorizontal { get; init; }

    /// <summary>
    /// The character for non-intersecting vertical edges of the box
    /// </summary> <summary>
    public required char EdgeVertical { get; init; }

    /// <summary>
    /// The character for intersections at the bottom of the box
    /// </summary>
    public required char IntersectionBottom { get; init; }

    /// <summary>
    /// The character for intersections at the top of the box
    /// </summary>
    public required char IntersectionTop { get; init; }

    /// <summary>
    /// The character for horizontal lines inside the box
    /// </summary>
    public required char InnerHorizontal { get; init; }

    /// <summary>
    /// The character for vertical lines inside the box
    /// </summary>
    public required char InnerVertical { get; init; }

    /// <summary>
    /// The character for intersections inside the box
    /// </summary>
    public required char IntersectionCenter { get; init; }

    /// <summary>
    /// The character for intersections on the left side of the box
    /// </summary>
    public required char IntersectionLeft { get; init; }

    /// <summary>
    /// The character for intersections on the right side of the box
    /// </summary> <summary>
    public required char IntersectionRight { get; init; }
}

/// <summary>
/// A collection of predefined border maps
/// </summary>
public static class BorderMaps
{
    /// <summary>
    /// Creates a BorderMap from a multi-line string representation
    /// </summary>
    /// <param name="borderString">a multi-line string representing the border characters</param>
    /// <returns>the <see cref="BorderMap"/> represented by the string</returns>
    private static BorderMap FromString(string borderString)
    {
        var lines = borderString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        // we must have at least 4 lines with 4 characters each
        if (lines.Length < 4 || lines.Any(line => line.Length < 4))
        {
            throw new ArgumentException("Border string must have at least 4 lines with at least 4 characters each", nameof(borderString));
        }

        return new BorderMap
        {
            CornerTopLeft = lines[0][0],
            IntersectionTop = lines[0][1],
            EdgeHorizontal = lines[0][2],
            CornerTopRight = lines[0][3],
            IntersectionLeft = lines[1][0],
            IntersectionCenter = lines[1][1],
            InnerHorizontal = lines[1][2],
            IntersectionRight = lines[1][3],
            EdgeVertical = lines[2][0],
            InnerVertical = lines[2][1],
            CornerBottomLeft = lines[3][0],
            IntersectionBottom = lines[3][1],
            CornerBottomRight = lines[3][3]
        };
    }

    private static readonly string SingleLineAsString = 
    """
    ┌┬─┐
    ├┼─┤
    ││ │
    └┴─┘
    """;

    private static readonly string DoubleLineAsString = 
    """
    ╔╦═╗
    ╠╬═╣
    ║║ ║
    ╚╩═╝
    """;

    private static readonly string DoubleWithSingleInnerAsString = 
    """
    ╔╤═╗
    ╟┼─╢
    ║│ ║
    ╚╧═╝
    """;

    /// <summary>
    /// A border map using double lines for the outer borders and inner borders
    /// </summary>
    public static readonly BorderMap DoubleLine = FromString(DoubleLineAsString);

    /// <summary>
    /// A border map using double lines for the outer borders and single lines for the inner borders
    /// </summary>
    public static readonly BorderMap DoubleWithSingleInner = FromString(DoubleWithSingleInnerAsString);

    /// <summary>
    /// A border map using single lines for all borders
    /// </summary>
    public static readonly BorderMap SingleLine = FromString(SingleLineAsString);
}