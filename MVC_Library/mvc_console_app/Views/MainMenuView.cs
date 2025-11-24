// DisplayUserView and all the books they have checked-out
// What are some other Views you could make that would be commonly used?
// GetBooksView, AddNewMemberView

// how user conveys intent and how its conveyed to the controller
using mvc_console_app;
using mvc_console_app.Controllers;
using mvc_console_app.Models;

// Controls User Interface flow 
public class MainMenuView
{
    private LibraryController Controller {get;}

    private IUserInterface Ui {get;}

    public MainMenuView(LibraryController controller, IUserInterface ui)
    {
        Controller = controller;
        Ui = ui;
    }

    // Present() tells view to do something
    public void Present()
    {
        int userInput = -1;

        List<string> options = [
            "View All Books",
            "Search Books",
            "Checkout Books",
            "Return Book",
            "Display All Members",
            "Add New Member",
            "Exit"
        ];

        while (userInput != options.Count)
        {
            userInput = Ui.PresentMenu("Library Management System", options);

            switch (userInput)
            {
                case 1:
                    DisplayAllBooksFlow();
                    break;
                case 2:
                    SearchBooksFlow();
                    break;
                case 3:
                    CheckoutBooksFlow();
                    break;
                case 4:
                    ReturnBookFlow();
                    break;
                case 5:
                    DisplayAllMembersFlow();
                    break;
                case 6:
                    AddNewMemberFlow();
                    break;
                case 7:
                    return; // exit program
                default:
                    break;
            }
        }
    } 

    private void AddNewMemberFlow()
    {
        AddNewMemberView addNewMemberView = new AddNewMemberView(Controller, Ui);
        addNewMemberView.Present();
    }

    private void CheckoutBooksFlow()
    {
        var member = PromptUserForMember();

        // TODO: 
        var book = PromptUserForBook(Controller.GetAllBooks());

        if (Controller.CheckoutBook(member, book))
        {
            Ui.PresentItems("Success", 
                [$"{member?.FirstName} {member?.LastName} has checked out \"{book?.Title}\" by {book?.Author}."]);            
        }
        // TODO: Else throw
    }

    private void DisplayAllBooksFlow()
    {
        // get list of all the books
        var books = Controller.GetAllBooks();

        PresentBooks("All Books", books);
    }

    private void DisplayAllMembersFlow()
    {
        DisplayAllMembersView displayAllMembersView = new DisplayAllMembersView(Controller, Ui);
        displayAllMembersView.Present();
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

        Ui.PresentItems(presentationTitle, listToPreset);
    }

    private Book? PromptUserForBook (IEnumerable<Book> books)
    {
        var items = new List<(string Description, Book Item)>();
        foreach (var book in books)
        {
            items.Add((book.ToString(), book));
        }
        return Ui.PresentCustomItems("Select a Book", items);
    }

    private int? PromptUserForBookId()
    {
        return Ui.GetIntInput("Enter book ID: ");
    }

    /// <summary>
    /// Prompts the user for a member
    /// </summary>
    /// <returns>The member that the user selected, if it exists; otherwise null</returns>
    private Member? PromptUserForMember ()
    {
        var items = new List<(string Description, Member Item)>();
        foreach (var member in Controller.GetAllMembers())
        {
            items.Add((member.ToString(), member));
        }
        return Ui.PresentCustomItems("Select a Member", items);
    }


    private int? PromptUserforMemberId()
    {
        return Ui.GetIntInput("Enter user ID: ");
    }


    private void ReturnBookFlow()
    {
        // get user
        var member = PromptUserForMember();

        if (member is null)
        {
            Ui.PresentItems("Error", ["No member selected."]);
            return;
        }

        // Check if member has any borrowed books
        if (!member.BorrowedBooks.Any())
        {
            Ui.PresentItems("Notice", 
                [$"{member.FirstName} {member.LastName} has no books checked out."]);
            return;
        }

        // get the members list of checked-out books
        var book = PromptUserForBook(member.BorrowedBooks);

        // call the Library to return the book
        if (book is not null)
        {
            Controller.ReturnBook(member, book);
            
            Ui.PresentItems("Success", 
                [$"{member.FirstName} {member.LastName} has returned \"{book.Title}\" by {book.Author}."]);
        }
        else
        {
            Ui.PresentItems("Error", ["No book selected."]);
        }
    }

    private void SearchBooksFlow()
    {
        List<string> options =
        [
            "Search by Author",
            "Search by Title",
            "Search by ID"
        ];

        int userChoice = Ui.PresentMenu("Search Type", options);

        switch (userChoice)
        {
            case 1:
                string authorSearchString = Ui.GetStringInput("Enter Author name: ");
                IEnumerable<Book> authorSearchResults = Controller.GetBooksByAuthor(authorSearchString);
                PresentBooks($"Books by {authorSearchString}", authorSearchResults);
                break;
            case 2:
                string titleSearchString = Ui.GetStringInput("Enter Book Title: ");
                IEnumerable<Book> titleSearchResults = Controller.GetBooksByTitle(titleSearchString);
                PresentBooks($"Books titled \"{titleSearchString}\"", titleSearchResults);
                break;
            case 3:
                int idSearchInt = Ui.GetIntInput("Enter Book ID: ");
                List<Book?> booksToPresent = [Controller.GetBookById(idSearchInt)];
                PresentBooks($"Books with ID of {idSearchInt}: ", booksToPresent);
                break;
        }
    }
}