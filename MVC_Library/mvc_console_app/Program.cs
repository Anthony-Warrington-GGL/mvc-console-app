// See https://aka.ms/new-console-template for more information
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using mvc_console_app.Views;

// new library
var library = new LibraryModel();
LibraryInitializer.Initialize(library);
var view = new ConsoleView();
var controller = new LibraryController(library, view);

controller.Start();