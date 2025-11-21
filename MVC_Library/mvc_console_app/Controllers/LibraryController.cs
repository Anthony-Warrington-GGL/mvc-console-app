using System.Data.Common;
using System.Runtime.CompilerServices;
using mvc_console_app.Models;
using mvc_console_app.Views;

namespace mvc_console_app.Controllers;

public class LibraryController
{
    private ILibrary Library { get; }
    private ConsoleView View { get; }

    public LibraryController(ILibrary library, ConsoleView view)
    {
        Library = library;
        View = view;
    }

    public void Start()
    {
        int userInput = -1;

        List<string> options = [
            "View All Books",
            "Search Books",
            "Checkout Books",
            "Return Book",
            "Exit"
        ];

        while (userInput != options.Count)
        {
            userInput = View.PresentMenu("Library Management System", options);

            switch (userInput)
            {
                case 1:
                    DisplayAllBooks();
                    break;
                case 2:
                    SearchBooks();
                    break;
                case 3:
                    CheckoutBooks();
                    break;
                case 4:
                    ReturnBook();
                    break;
                case 5:
                    return;
                default:
                    break;
            }
        }
    }

    public void AddBook(string author, string title)
    {
        var rand = new Random();
        var id = rand.Next(0, 100);
        Library.AddBook(new Book(id, title, author));
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Library.GetAllBooks();
    }

    public Book? GetBookById(int id)
    {
        return Library.GetBookById(id);
    }

    private void DisplayAllBooks()
    {
        // get list of all the books
        var books = GetAllBooks();

        PresentBooks("All Books", books);
    }

    private void SearchBooks()
    {
        List<string> options =
        [
            "Search by Author",
            "Search by Title",
            "Search by ID"
        ];

        int userChoice = View.PresentMenu("Search Type", options);

        switch (userChoice)
        {
            case 1:
                string authorSearchString = View.GetStringInput("Enter Author name: ");
                IEnumerable<Book> authorSearchResults = Library.GetBooksByAuthor(authorSearchString);
                PresentBooks($"Books by {authorSearchString}", authorSearchResults);
                break;
            case 2:
                string titleSearchString = View.GetStringInput("Enter Book Title: ");
                IEnumerable<Book> titleSearchResults = Library.GetBooksByTitle(titleSearchString);
                PresentBooks($"Books titled \"{titleSearchString}\"", titleSearchResults);
                break;
            case 3:
                int idSearchInt = View.GetIntInput("Enter Book ID: ");
                List<Book?> booksToPresent = [Library.GetBookById(idSearchInt)];
                PresentBooks($"Books with ID of {idSearchInt}: ", booksToPresent);
                break;
        }
    }

    private void PresentBooks(string presentationTitle, IEnumerable<Book?> books)
    {
        var booksAsStrings = new List<string>();
        foreach (var book in books)
        {
            if (book != null)
            {
                booksAsStrings.Add($"{book}");
            }
        }

        // call present all items
        var listToPreset = booksAsStrings.Count == 0
            ? ["No books found"]
            : booksAsStrings;

        View.PresentItems(presentationTitle, listToPreset);
    }

    private void CheckoutBooks()
    {
        var member = PromptUserForMember();

        var book = PromptUserForBook(Library.GetAllBooks());

        if (member is not null && book is not null)
        {
            // Checkout the book
            Library.CheckoutBook(member, book); // TODO: What if the checkout is not successful?
            
            View.PresentItems("Success", 
                [$"{member.FirstName} {member.LastName} has checked out \"{book.Title}\" by {book.Author}."]);
        }
    }

    private void ReturnBook()
    {
        // get user
        var member = PromptUserForMember();

        if (member is null)
        {
            View.PresentItems("Error", ["No member selected."]);
            return;
        }

        // Check if member has any borrowed books
        if (!member.BorrowedBooks.Any())
        {
            View.PresentItems("Notice", 
                [$"{member.FirstName} {member.LastName} has no books checked out."]);
            return;
        }

        // get the members list of checked-out books
        var book = PromptUserForBook(member.BorrowedBooks);

        // call the Library to return the book
        if (book is not null)
        {
            Library.ReturnBook(member, book);
            
            View.PresentItems("Success", 
                [$"{member.FirstName} {member.LastName} has returned \"{book.Title}\" by {book.Author}."]);
        }
        else
        {
            View.PresentItems("Error", ["No book selected."]);
        }
    }

    /// <summary>
    /// Prompts the user for a member
    /// </summary>
    /// <returns>The member that the user selected, if it exists; otherwise null</returns>
    private Member? PromptUserForMember ()
    {
        var items = new List<(string Description, Member Item)>();
        foreach (var member in Library.GetAllMembers())
        {
            items.Add((member.ToString(), member));
        }
        return View.PresentCustomItems("Select a Member", items);
    }

    private Book? PromptUserForBook (IEnumerable<Book> books)
    {
        var items = new List<(string Description, Book Item)>();
        foreach (var book in books)
        {
            items.Add((book.ToString(), book));
        }
        return View.PresentCustomItems("Select a Book", items);
    }

    private int? PromptUserForBookId()
    {
        return View.GetIntInput("Enter book ID: ");
    }

    private int? PromptUserforMemberId()
    {
        return View.GetIntInput("Enter user ID: ");
    }
}