using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

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

    public static IEnumerable<Member> InitialMembers { get; } =
    [
    // new (1, "James", "Smith"),
    // new (2, "Mary", "Johnson"),
    // new (3, "Robert", "Williams"),
    // new (4, "Patricia", "Brown"),
    // new (5, "Michael", "Jones"),
    // new (6, "Jennifer", "Garcia"),
    // new (7, "William", "Miller"),
    // new (8, "Linda", "Davis"),
    // new (9, "David", "Rodriguez"),
    // new (10, "Elizabeth", "Martinez"),
    // new (11, "Richard", "Hernandez"),
    // new (12, "Susan", "Lopez"),
    // new (13, "Joseph", "Gonzalez"),
    // new (14, "Jessica", "Wilson"),
    // new (15, "Thomas", "Anderson"),
    // new (16, "Sarah", "Thomas"),
    // new (17, "Charles", "Taylor"),
    // new (18, "Karen", "Moore"),
    // new (19, "Christopher", "Jackson"),
    // new (20, "Nancy", "Martin")
    ];

    public static void Initialize(ILibrary library)
    {
        library.CreateBooks(InitialBooks);
        // foreach(Member member in InitialMembers)
        // {
        //     library.AddMember(member);   
        // }        
    }
}