using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

public static class LibraryInitializer
{
    public static IEnumerable<Book> InitialBooks { get; } =
    [
    // new (1, "To Kill a Mockingbird", "Harper Lee"),
    // new (2, "1984", "George Orwell"),
    // new (3, "Pride and Prejudice", "Jane Austen"),
    // new (4, "The Great Gatsby", "F. Scott Fitzgerald"),
    // new (5, "The Catcher in the Rye", "J.D. Salinger"),
    // new (6, "The Lord of the Rings", "J.R.R. Tolkien"),
    // new (7, "Animal Farm", "George Orwell"),
    // new (8, "Jane Eyre", "Charlotte Brontë"),
    // new (9, "Wuthering Heights", "Emily Brontë"),
    // new (10, "The Hobbit", "J.R.R. Tolkien"),
    // new (11, "Brave New World", "Aldous Huxley"),
    // new (12, "The Chronicles of Narnia", "C.S. Lewis"),
    // new (13, "Crime and Punishment", "Fyodor Dostoevsky"),
    // new (14, "The Picture of Dorian Gray", "Oscar Wilde"),
    // new (15, "Frankenstein", "Mary Shelley"),
    // new (16, "Dracula", "Bram Stoker"),
    // new (17, "Moby Dick", "Herman Melville"),
    // new (18, "War and Peace", "Leo Tolstoy"),
    // new (19, "The Odyssey", "Homer"),
    // new (20, "The Iliad", "Homer"),
    // new (21, "Don Quixote", "Miguel de Cervantes"),
    // new (22, "The Divine Comedy", "Dante Alighieri"),
    // new (23, "Great Expectations", "Charles Dickens"),
    // new (24, "A Tale of Two Cities", "Charles Dickens"),
    // new (25, "Oliver Twist", "Charles Dickens"),
    // new (26, "The Adventures of Huckleberry Finn", "Mark Twain"),
    // new (27, "The Adventures of Tom Sawyer", "Mark Twain"),
    // new (28, "Alice's Adventures in Wonderland", "Lewis Carroll"),
    // new (29, "The Little Prince", "Antoine de Saint-Exupéry"),
    // new (30, "One Hundred Years of Solitude", "Gabriel García Márquez"),
    // new (31, "The Grapes of Wrath", "John Steinbeck"),
    // new (32, "Of Mice and Men", "John Steinbeck"),
    // new (33, "The Old Man and the Sea", "Ernest Hemingway"),
    // new (34, "For Whom the Bell Tolls", "Ernest Hemingway"),
    // new (35, "The Sun Also Rises", "Ernest Hemingway"),
    // new (36, "Fahrenheit 451", "Ray Bradbury"),
    // new (37, "The Handmaid's Tale", "Margaret Atwood"),
    // new (38, "Slaughterhouse-Five", "Kurt Vonnegut"),
    // new (39, "Catch-22", "Joseph Heller"),
    // new (40, "Lord of the Flies", "William Golding"),
    // new (41, "The Road", "Cormac McCarthy"),
    // new (42, "Beloved", "Toni Morrison"),
    // new (43, "The Color Purple", "Alice Walker"),
    // new (44, "Invisible Man", "Ralph Ellison"),
    // new (45, "The Bell Jar", "Sylvia Plath"),
    // new (46, "On the Road", "Jack Kerouac"),
    // new (47, "A Clockwork Orange", "Anthony Burgess"),
    // new (48, "The Shining", "Stephen King"),
    // new (49, "Dune", "Frank Herbert"),
    // new (50, "Foundation", "Isaac Asimov")
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
        // library.AddBooks(InitialBooks);
        // foreach(Member member in InitialMembers)
        // {
        //     library.AddMember(member);   
        // }        
    }
}