using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

public class SelectBookView
{
    private IUserInterface UserInterface {get;}

    public SelectBookView(IUserInterface userInterface)
    {
        UserInterface = userInterface;
    }

    public Book SelectBook(IEnumerable<Book> books)
    {
        if (books.Count() < 1)
        {
            throw new ArgumentException("The list of books was empty.");
        }

        return UserInterface.PresentCustomItems("Select a book: ", GetBooksMenuItems(books));
    }

    private (string Description, Book Book) GetBookMenuItem (Book book)
    {
        string bookDescription = $"Author: {book.Author}, Title: {book.Title}";
        return (bookDescription, book);
    }

    private List<(string Description, Book book)> GetBooksMenuItems(IEnumerable<Book> books)
    {
        List<(string Description, Book book)> booksTuples = [];

        foreach(var book in books)
        {
            booksTuples.Add(GetBookMenuItem(book));
        }

        return booksTuples;
    }
}