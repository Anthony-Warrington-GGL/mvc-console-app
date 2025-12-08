namespace mvc_console_app.Models;

/// <summary>
/// Unit tests for <see cref="Book"/>
/// </summary>
[TestClass]
public sealed class BookTests
{
    [TestMethod]
    public void CONSTRUCTOR__when_author_is_null__then_throws_ArgumentNullException()
    {
        // Arrange
        string author = null!;
        string title = "";
        Guid id = Guid.Empty;

        // Act
        void act() => new Book(id, title, author);

        // Assert
        Assert.ThrowsException<ArgumentNullException>(act);
    }

    [TestMethod]
    public void CONSTRUCTOR__when_title_is_null__then_throws_ArgumentNullException()
    {
        // Arrange
        string author = "";
        string title = null!;
        Guid id = Guid.Empty;

        // Act
        void act()
        {
            _ = new Book(id, title, author);
        }

        // Assert
        Assert.ThrowsException<ArgumentNullException>(act);
    }

    // Guid.Empty to be valid?

    // Empty title valid?

    // Empty author valid?

    // verify when they're not null, it is actually the value that you passed

    // 
}