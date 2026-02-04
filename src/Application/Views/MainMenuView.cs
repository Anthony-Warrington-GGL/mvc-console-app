// DisplayUserView and all the books they have checked-out
// What are some other Views you could make that would be commonly used?
// GetBooksView, AddNewMemberView

// how user conveys intent and how its conveyed to the controller
using System.ComponentModel;
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

    public void Present()
    {
        List<(string Description, Action action)> menuItems =
        [
            ("Display All Books", DisplayAllBooksFlow),
            ("Search Books", SearchBooksFlow),
            ("Checkout Book", CheckoutBooksFlow),
            ("Return Book",  ReturnBookFlow),
            ("Display All Members", DisplayAllMembersFlow),
            ("Add New Member", AddNewMemberFlow),
            ("Show sub-menu", PresentSubMenu)
        ];

        Ui.PresentMenu("Library Management System", menuItems);
    }

    private void PresentSubMenu()
    {
        List<(string Description, Action action)> menuItems =
        [
          ("Item 1", () => {}),  
          ("Item 2", () => {}),
          ("Item 3", () => {}),
          ("Item 4", () => {})
        ];

        Ui.PresentMenu("SubMenu", menuItems);
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
        
        Controller.CheckoutBook(selectedMember.Id, selectedBook.BookId);
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
            Controller.ReturnBook(member.Id, book.BookId);

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