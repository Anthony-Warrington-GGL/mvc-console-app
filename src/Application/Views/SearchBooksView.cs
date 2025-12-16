
using System.Dynamic;
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using MvcLibrary.UserInterfaces.Abstractions;

/// <summary>
/// Allows the user to select a number of books contained in a libary and then
/// return that collection 
/// </summary>
public class SearchBooksView
{
    private LibraryController Controller {get;}
    
    private IUserInterface UserInterface {get;}

    public SearchBooksView (LibraryController controller, IUserInterface userInterface)
    {
        Controller = controller;
        UserInterface = userInterface;
    }

    public IEnumerable<Book> SearchBooks ()
    {
        string userInput = UserInterface.GetStringInput("Enter a search term: ");
        IEnumerable<Book> books = Controller.SearchBooks(userInput);
        return books;
    }
}