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

    [TestMethod]
    public void CONSTRUCTOR__when_id_is_empty__then_book_is_created()
    {
        // Arrange
        Guid id = Guid.Empty;
        string title = "Test Title";
        string author = "Test Author";

        // Act
        var book = new Book(id, title, author);

        // Assert
        Assert.IsNotNull(book);
        Assert.AreEqual(Guid.Empty, book.Id);
    }

    [TestMethod]
    public void CONSTRUCTOR__when_title_is_empty__then_book_is_created()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string title = "";
        string author = "Test Author";

        // Act
        var book = new Book(id, title, author);

        // Assert
        Assert.IsNotNull(book);
        Assert.AreEqual("", book.Title);
    }

    [TestMethod]
    public void CONSTRUCTOR__when_author_is_empty__then_book_is_created()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string title = "Test Title";
        string author = "";

        // Act
        var book = new Book(id, title, author);

        // Assert
        Assert.IsNotNull(book);
        Assert.AreEqual("", book.Author);
    }

    [TestMethod]
    public void CONSTRUCTOR__when_valid_values_passed__then_properties_are_set_correctly()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        string expectedTitle = "The Great Gatsby";
        string expectedAuthor = "F. Scott Fitzgerald";

        // Act
        var book = new Book(expectedId, expectedTitle, expectedAuthor);

        // Assert
        Assert.AreEqual(expectedId, book.Id);
        Assert.AreEqual(expectedTitle, book.Title);
        Assert.AreEqual(expectedAuthor, book.Author);
        Assert.IsNull(book.CheckedOutDate);
    }

    [TestMethod]
    public void CONSTRUCTOR__when_book_created__then_CheckedOutDate_is_null()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string title = "Test Title";
        string author = "Test Author";

        // Act
        var book = new Book(id, title, author);

        // Assert
        Assert.IsNull(book.CheckedOutDate);
    }

    [TestMethod]
    public void IsOverdue__when_CheckedOutDate_is_within_14_days_in_the_past__then_returns_false()
    {
        // Arrange
        var book = new Book(Guid.NewGuid(), "Test Title", "Test Author");
        book.CheckedOutDate = DateTime.Now.AddDays(-10);

        // Act
        bool result = book.IsOverdue();

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsOverdue__when_CheckedOutDate_is_more_than_14_days_in_the_past__then_returns_true()
    {
        // Arrange
        var book = new Book(Guid.NewGuid(), "Test Title", "Test Author");
        book.CheckedOutDate = DateTime.Now.AddDays(-15);

        // Act
        bool result = book.IsOverdue();

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ToString__returns_correct_format()
    {
        // Arrange
        var book = new Book(Guid.NewGuid(), "1984", "George Orwell");
        string expected = "Title: \"1984\", Author: George Orwell";

        // Act
        string result = book.ToString();

        // Assert
        Assert.AreEqual(expected, result);
    }
}