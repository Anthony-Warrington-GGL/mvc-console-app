// DisplayUserView and all the books they have checked-out
// What are some other Views you could make that would be commonly used?
// GetBooksView, AddNewMemberView

// how user conveys intent and how its conveyed to the controller
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using mvc_console_app.Views.Generic;
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

    // TODO: fix after implementing new interface so that the Ui is responsible for looping through the main menu
    public void PresentNew()
    {
        Func<DescribedAction, string> itemFormatter = act => act.Description;

        // present items to user and have user pick one
        var getItemView = new GetItemView<DescribedAction>(itemFormatter, Ui);

        List<DescribedAction> actions = 
        [
            new DescribedAction{Description = "View All Books", Action = DisplayAllBooksFlow},
            new DescribedAction{Description = "Search Books", Action = SearchBooksFlow},
            new DescribedAction{Description = "Checkout Book", Action = CheckoutBooksFlow},
            new DescribedAction{Description = "Return Book", Action = ReturnBookFlow},
            new DescribedAction{Description = "Display All Members", Action = DisplayAllMembersFlow},
            new DescribedAction{Description = "Add New Member", Action = AddNewMemberFlow}
        ];

        var selectedAction = getItemView.GetItem("Library Management System", actions);
        
        selectedAction?.Action.Invoke();
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
            userInput = Ui.GetSelectedIndexFromUser("Library Management System", options);

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
        var searchBooksView = new SearchBooksView(Controller, Ui);
        IEnumerable<Book> books = searchBooksView.SearchBooks();

        var selectBookView = CommonLibraryViews.GetBookView(Ui);

        var selectedBook = selectBookView.GetItem("Select a Book", books) ?? throw new InvalidOperationException("No book is selected.");
        var getMemberView = CommonLibraryViews.GetMemberView(Ui);

        Member? selectedMember = getMemberView.GetItem("Select a Member", Controller.GetAllMembers()) ?? throw new InvalidOperationException("No member is selected.");
        
        Controller.CheckoutBook(selectedMember.Id, selectedBook.Id);
    }

    private void DisplayAllBooksFlow()
    {
        // get list of all the books
        var books = Controller.GetAllBooks();

        var displayItemsView = CommonLibraryViews.DisplayBooksView(Ui);
        
        displayItemsView.PresentItems("All Books", books);        
    }

    private void DisplayAllMembersFlow()
    {
        var view = CommonLibraryViews.DisplayMembersView(Ui);

        view.PresentItems("All Members", Controller.GetAllMembers());
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
        var getBookView = CommonLibraryViews.GetBookView(Ui);
        var book = getBookView.GetItem("Choose a book to return", member.BorrowedBooks);

        // call the Library to return the book
        if (book is not null)
        {
            Controller.ReturnBook(member.Id, book.Id);

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
        var displayBooksView = CommonLibraryViews.DisplayBooksView(Ui);
        displayBooksView.PresentItems("Search Results", books);
    }
}