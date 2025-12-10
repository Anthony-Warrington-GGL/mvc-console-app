# Solution Structure

This document provides a possible project structure for the MVC application.

## Diagram

The following diagram categorizes different classes into separate projects: a class library, a test project (for the class library), and an application project.

```mermaid
    classDiagram

    namespace ClassLibrary {
        class Book {
            +Guid Id
            +string Author
            +string Title
            +IsOverdue() bool

        }

        class ILibrary {
            <<interface>>
            +CheckOutBook(Guid memberId, Guid bookId) void
            +CreateBook(string author, string title) Book
            +CreateBooks(List~tuple~ books) IEnumerable~Book~
            +CreateMember(string firstName, string lastName) Member
            +GetAllBooks() IEnumerable~Book~
            +GetAllMembers() IEnumerable~Member~
            +GetBookById(Guid bookId) Book?
            +GetBooksByTitle(string title) IEnumerable~Book~
            +GetBooksByAuthor(string author) IEnumerable~Book~
            +GetBooksCheckedOutByMember(Guid memberId) IEnumerable~Book~
            +GetMemberById(Guid id) Member?
            +ReturnBook(Guid memberId, Guid bookId) void

        }
        class Member {
            +Guid Id
            +string FirstName
            +string LastName
            +ICollection~Book~ BorrowedBooks
            +ToString() string
        }
    }

    namespace ClassLibrary.Tests {
        class BookTests {

        }
        class LibraryTests {

        }
        class MemberTests {

        }
    }

    namespace Application {
        class Program {

        }

        class IUI {
            <<interface>>
            +GetIntInput(string prompt) int
            +GetStringInput(string prompt) string
            +PresentCustomItems~T~(...) T
            +PresentItems(string title, IEnumerable~T~ items) void
            +PresentMenu(string title, List~string~ options) int

        }

        class LibraryController {

        }

        class MainMenuView {

        }
    }

```

## Folder Strcuture

The following is one possible way the folders and files could be organized, with a solution file at the root level, `doc`, `src`, and `test` folders, and then the separate projects within those folders. Each project is in its own folder and has its own projcet file. Arrows (`->`) indicate project references.

```text
doc\
    Design.md
src\
    mvc_console_app\
        mvc_console_app.csproj
            -> mvc_library
        Program.cs
        Controllers
            LibraryController.cs
        UIs\
            ConsoleUi.cs
        Views\
            MainMenuView.cs
            SearchView.cs
    mvc_library\
        mvc_library.csproj
        Interfaces\
            ILibrary.cs
        Models\
            Book.cs
            Member.cs
test\
    mvc_library.Tests\
        mvc_library.Tests.csproj
            -> mvc_library
        Models\
            BookTests.cs
            MemberTests.cs
mvc.sln
    -> mvc_console_app
    -> mvc_library
    -> mvc_library.Tests
```
