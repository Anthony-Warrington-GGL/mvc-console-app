namespace mvc_console_app.Models;

public class Member
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Book> BorrowedBooks { get; private set; } = new HashSet<Book>();

    public Member(){}

    public Member(Guid id, string firstName, string lastName)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public override string ToString()
    {
        return $"{LastName}, {FirstName}";
    }
}