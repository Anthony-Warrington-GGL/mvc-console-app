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

var repoDirectoryPath = "./repoFiles";
// var repository = new JsonRepository<Guid, Book>(repoDirectoryPath);

// // create db before library
// using var db = new LibraryContext();
// db.Database.EnsureDeleted();
// db.Database.EnsureCreated();

var bookRepository = new JsonRepository<Guid, Book>(repoDirectoryPath);
var memberRepository = new JsonRepository<Guid, Member>(repoDirectoryPath);
var ledgerRepository = new JsonRepository<Guid, LedgerEntry>(repoDirectoryPath);

var guidManager = new GuidManager();
//var library = new LibraryModel(guidManager); // old library
//var library = new EfLibraryModel(db); // new library
var library = new LibraryModel(guidManager, bookRepository, memberRepository, ledgerRepository);

LibraryInitializer.Initialize(library);
var controller = new LibraryController(library);
IUserInterface ui = new ConsoleUi();
var mainView = new MainMenuView(controller, ui);


//Console.WriteLine($"Database path: {db.DbPath}.");
// var book = new Book(Guid.NewGuid(), "1984", "George Orwell");
// var member = new Member(Guid.NewGuid(), "John", "Smith");
//member.BorrowedBooks.Add(book);

// Add it to the DbContext
// db.Books.Add(book);
// db.Members.Add(member);

// Save changes to the database
// db.SaveChanges();

// Console.WriteLine($"Added book: {book}");
// Console.ReadKey();

mainView.Present();
