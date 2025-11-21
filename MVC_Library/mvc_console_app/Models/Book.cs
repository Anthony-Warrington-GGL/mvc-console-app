namespace mvc_console_app.Models;

public class Book
{
    public int Id;
    public string Author;
    public string Title;
    public bool IsAvailable;
    // Nullable because a book is null when its created
    public DateTime? CheckedOutDate { get; set; }

    public Book(int id, string title, string author)
    {
        this.Id = id;
        this.Title = title;
        this.Author = author;
        // Assuming the book is available when created...
        this.IsAvailable = true;
        CheckedOutDate = null;
    }

    public bool IsOverdue()
    {
        if (IsAvailable || CheckedOutDate == null)
            return false;

        return DateTime.Now > CheckedOutDate.Value.AddDays(14);
    }

    public override string ToString()
    {
        return $"Title: \"{Title}\", Author: {Author}";
    }
}