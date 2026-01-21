// See https://aka.ms/new-console-template for more information
using mvc_console_app;
using mvc_console_app.Controllers;
using mvc_console_app.Models;
using mvc_console_app.UI;
using mvc_console_app.Views;
using MvcLibrary.UserInterfaces.Abstractions;

// new library
var guidManager = new GuidManager();
var library = new LibraryModel(guidManager);
LibraryInitializer.Initialize(library);
var controller = new LibraryController(library);
IUserInterface ui = new ConsoleUi();
var mainView = new MainMenuView(controller, ui);

mainView.PresentNew();