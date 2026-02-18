// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using mvc_console_app;
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using mvc_console_app.Repositories;
using mvc_console_app.UI;
using mvc_console_app.Views;
using MvcLibrary.UserInterfaces.Abstractions;

if (false)
{
    // create db before library
    using var db = new LibraryContext();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var guidManager = new GuidManager();
    //var library = new LibraryModel(guidManager); // old library
    var library = new EfLibraryModel(db); // new library

    LibraryInitializer.Initialize(library);
    var controller = new LibraryController(library);
    IUserInterface ui = new ConsoleUi();
    var mainView = new MainMenuView(controller, ui);


    Console.WriteLine($"Database path: {db.DbPath}.");
    var book = new Book(Guid.NewGuid(), "1984", "George Orwell");
    var member = new Member(Guid.NewGuid(), "John", "Smith");
    //member.BorrowedBooks.Add(book);

    // Add it to the DbContext
    db.Books.Add(book);
    db.Members.Add(member);

    // Save changes to the database
    db.SaveChanges();

    Console.WriteLine($"Added book: {book}");
    Console.ReadKey();

    mainView.Present();
}
var repoDirectoryPath = "./repoFiles";
var repository = new JsonRepository<Guid, int>(repoDirectoryPath);

Guid keyToSave = Guid.NewGuid();
repository.StoreOrUpdateItem(keyToSave, 111111);
repository.StoreOrUpdateItem(Guid.NewGuid(), 1283123);
repository.StoreOrUpdateItem(Guid.NewGuid(), 12376124);

var allItems = repository.GetAll();

foreach (var item in allItems)
{
    Console.WriteLine(item);
}

List<Guid> keysToCheck =
[
    keyToSave,
    Guid.NewGuid()
];

foreach (var key in keysToCheck)
{
    if (repository.TryGetItem(key, out var retrievedItem))
    {
        Console.WriteLine($"{key} : {retrievedItem}");
    }
    else
    {
        Console.WriteLine($"{key} : not found");
    }
}

if (repository.RemoveItem(keyToSave))
{
    Console.WriteLine($"{keyToSave} removed.");
}
else
{
    Console.WriteLine($"{keyToSave} was not removed.");
}

if (repository.RemoveItem(keyToSave))
{
    Console.WriteLine($"{keyToSave} removed.");
}
else
{
    Console.WriteLine($"{keyToSave} was not removed.");
}