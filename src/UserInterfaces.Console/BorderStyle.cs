namespace UserInterfaces.ConsoleUI;

/// <summary>
/// A struct that represents a style of border, including corners, edges, and intersections
/// </summary>
public readonly record struct BorderStyle
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