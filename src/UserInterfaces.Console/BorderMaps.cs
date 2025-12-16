namespace UserInterfaces.ConsoleUI;

/// <summary>
/// A collection of predefined border styles
/// </summary>
public static class BorderStyles
{
    /// <summary>
    /// Creates a <see cref="BorderStyle"> from a multi-line string representation
    /// </summary>
    /// <param name="borderString">a multi-line string representing the border characters</param>
    /// <returns>the <see cref="BorderStyle"/> represented by the string</returns>
    private static BorderStyle FromString(string borderString)
    {
        var lines = borderString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        // we must have at least 4 lines with 4 characters each
        if (lines.Length < 4 || lines.Any(line => line.Length < 4))
        {
            throw new ArgumentException("Border string must have at least 4 lines with at least 4 characters each", nameof(borderString));
        }

        return new BorderStyle
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
    public static readonly BorderStyle DoubleLine = FromString(DoubleLineAsString);

    /// <summary>
    /// A border map using double lines for the outer borders and single lines for the inner borders
    /// </summary>
    public static readonly BorderStyle DoubleWithSingleInner = FromString(DoubleWithSingleInnerAsString);

    /// <summary>
    /// A border map using single lines for all borders
    /// </summary>
    public static readonly BorderStyle SingleLine = FromString(SingleLineAsString);
}