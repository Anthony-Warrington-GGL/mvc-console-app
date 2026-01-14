// DisplayUserView and all the books they have checked-out
// What are some other Views you could make that would be commonly used?
// GetBooksView, AddNewMemberView

// how user conveys intent and how its conveyed to the controller
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

namespace mvc_console_app.Views; 

// Controls User Interface flow 
public class MainMenuView
{
    private LibraryController Controller { get; }

    private IUserInterface Ui { get; }

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
                    // TODO: Implement DisplayItemsView, then refactor
                    // DisplayAllBooksView to use DisplayItemsView to
                    // present the books, instead of the PresentBooks()
                    // method in this class
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
                    // TODO: after implementing DisplayItemsView (see case 1),
                    // refactor DisplayAllMembersFlow to use the generic
                    // DisplayItemsView to present the members, instead of
                    // using DisplayAllMembersView
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
        SearchBooksView searchBooksView = new SearchBooksView(Controller, Ui);
        IEnumerable<Book> books = searchBooksView.SearchBooks();

        SelectBookView selectBookView = new SelectBookView(Ui);
        Book selectedBook = selectBookView.SelectBook(books);

        var getMemberView = CommonLibraryViews.GetMemberView(Ui);

        Member? selectedMember = getMemberView.GetItem("Select a Member", Controller.GetAllMembers());

        if (selectedMember is null)
        {
            throw new InvalidOperationException("No member is selected.");
        }

        Controller.CheckoutBook(selectedMember.Id, selectedBook.Id);
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

    private Book? PromptUserForBook(IEnumerable<Book> books)
    {
        var items = new List<(string Description, Book Item)>();
        foreach (var book in books)
        {
            items.Add((book.ToString(), book));
        }
        return Ui.GetItem("Select a Book", items);
    }

    private int? PromptUserForBookId()
    {
        return Ui.GetInt("Enter book ID: ");
    }

    /// <summary>
    /// Prompts the user for a member
    /// </summary>
    /// <returns>The member that the user selected, if it exists; otherwise null</returns>
    private Member? PromptUserForMember()
    {
        var items = new List<(string Description, Member Item)>();
        foreach (var member in Controller.GetAllMembers())
        {
            items.Add((member.ToString(), member));
        }
        return Ui.GetItem("Select a Member", items);
    }

    private int? PromptUserforMemberId()
    {
        return Ui.GetInt("Enter user ID: ");
    }

    private void ReturnBookFlow()
    {
        GetItemView<Member> getMemberView = CommonLibraryViews.GetMemberViewWithDetails(Ui);

        // get user
        var member = getMemberView.GetItem("Select a Member", Controller.GetAllMembers());

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
        SearchBooksView searchBooksView = new SearchBooksView(Controller, Ui);
        IEnumerable<Book> books = searchBooksView.SearchBooks();
        PresentBooks("Search Results", books);
    }
}