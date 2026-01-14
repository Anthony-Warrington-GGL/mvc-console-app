using mvc_console_app.Interfaces;

namespace mvc_console_app;

public static class LibraryInitializer
{
    public static IEnumerable<(string title, string author)> InitialBooks { get; } =
    [
        new ("To Kill a Mockingbird", "Harper Lee"),
        new ("1984", "George Orwell"),
        new ("Pride and Prejudice", "Jane Austen"),
        new ("The Great Gatsby", "F. Scott Fitzgerald"),
        new ("The Catcher in the Rye", "J.D. Salinger"),
        new ("The Lord of the Rings", "J.R.R. Tolkien"),
        new ("Animal Farm", "George Orwell"),
        new ("Jane Eyre", "Charlotte Brontë"),
        new ("Wuthering Heights", "Emily Brontë"),
        new ("The Hobbit", "J.R.R. Tolkien"),
        new ("Brave New World", "Aldous Huxley"),
        new ("The Chronicles of Narnia", "C.S. Lewis"),
        new ("Crime and Punishment", "Fyodor Dostoevsky"),
        new ("The Picture of Dorian Gray", "Oscar Wilde"),
        new ("Frankenstein", "Mary Shelley"),
        new ("Dracula", "Bram Stoker"),
        new ("Moby Dick", "Herman Melville"),
        new ("War and Peace", "Leo Tolstoy"),
        new ("The Odyssey", "Homer"),
        new ("The Iliad", "Homer"),
        new ("Don Quixote", "Miguel de Cervantes"),
        new ("The Divine Comedy", "Dante Alighieri"),
        new ("Great Expectations", "Charles Dickens"),
        new ("A Tale of Two Cities", "Charles Dickens"),
        new ("Oliver Twist", "Charles Dickens"),
        new ("The Adventures of Huckleberry Finn", "Mark Twain"),
        new ("The Adventures of Tom Sawyer", "Mark Twain"),
        new ("Alice's Adventures in Wonderland", "Lewis Carroll"),
        new ("The Little Prince", "Antoine de Saint-Exupéry"),
        new ("One Hundred Years of Solitude", "Gabriel García Márquez"),
        new ("The Grapes of Wrath", "John Steinbeck"),
        new ("Of Mice and Men", "John Steinbeck"),
        new ("The Old Man and the Sea", "Ernest Hemingway"),
        new ("For Whom the Bell Tolls", "Ernest Hemingway"),
        new ("The Sun Also Rises", "Ernest Hemingway"),
        new ("Fahrenheit 451", "Ray Bradbury"),
        new ("The Handmaid's Tale", "Margaret Atwood"),
        new ("Slaughterhouse-Five", "Kurt Vonnegut"),
        new ("Catch-22", "Joseph Heller"),
        new ("Lord of the Flies", "William Golding"),
        new ("The Road", "Cormac McCarthy"),
        new ("Beloved", "Toni Morrison"),
        new ("The Color Purple", "Alice Walker"),
        new ("Invisible Man", "Ralph Ellison"),
        new ("The Bell Jar", "Sylvia Plath"),
        new ("On the Road", "Jack Kerouac"),
        new ("A Clockwork Orange", "Anthony Burgess"),
        new ("The Shining", "Stephen King"),
        new ("Dune", "Frank Herbert"),
        new ("Foundation", "Isaac Asimov")
    ];

    public static IEnumerable<(string firstName, string lastName)> InitialMembers { get; } =
    [
        new ("James", "Smith"),
        new ("Mary", "Johnson"),
        new ("Robert", "Williams"),
        new ("Patricia", "Brown"),
        new ("Michael", "Jones"),
        new ("Jennifer", "Garcia"),
        new ("William", "Miller"),
        new ("Linda", "Davis"),
        new ("David", "Rodriguez"),
        new ("Elizabeth", "Martinez"),
        new ("Richard", "Hernandez"),
        new ("Susan", "Lopez"),
        new ("Joseph", "Gonzalez"),
        new ("Jessica", "Wilson"),
        new ("Thomas", "Anderson"),
        new ("Sarah", "Thomas"),
        new ("Charles", "Taylor"),
        new ("Karen", "Moore"),
        new ("Christopher", "Jackson"),
        new ("Nancy", "Martin")
    ];

    public static void Initialize(ILibrary library)
    {
        library.CreateBooks(InitialBooks);
        library.CreateMembers(InitialMembers);     
    }
}