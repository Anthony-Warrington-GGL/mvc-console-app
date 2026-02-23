// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
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
var repository = new JsonRepository<Guid, Book>(repoDirectoryPath);


List<Book> booksList =
[
    new Book(Guid.NewGuid(), "1984", "George Orwell"),
    new Book(Guid.NewGuid(), "Animal Farm", "George Orwell"),
    new Book(Guid.NewGuid(), "Cat", "Dog")
];


Guid keyToSave = booksList[0].Id;
foreach (var book in booksList)
{
    repository.StoreOrUpdateItem(book.Id, book);
}


var allItems = repository.GetAllItems();

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
Console.ReadKey();
if (repository.RemoveItem(keyToSave))
{
    Console.WriteLine($"{keyToSave} removed.");
}
else
{
    Console.WriteLine($"{keyToSave} was not removed.");
}
Console.ReadKey();
if (repository.RemoveItem(keyToSave))
{
    Console.WriteLine($"{keyToSave} removed.");
}
else
{
    Console.WriteLine($"{keyToSave} was not removed.");
}

// Currently there is no way for the repo to get the key - once it's assigned, its lost
// so, there should be a way to retrieve all KVPs in the repo