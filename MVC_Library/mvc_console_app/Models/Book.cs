namespace mvc_console_app.Models;

public class Book
{
    public Guid Id;
    public string Author;
    public string Title;
    
    // Nullable because a book is null when its created
    public DateTime? CheckedOutDate { get; set; }

    public Book(Guid id, string title, string author)
    {
        if (author is null || title is null)
        {
            throw new ArgumentNullException();
        }

        this.Id = id;
        this.Title = title;
        this.Author = author;
        // Assuming the book is available when created...
        CheckedOutDate = null;
    }

    public bool IsOverdue()
    {
        if (CheckedOutDate == null)
            return false;

        return DateTime.Now > CheckedOutDate.Value.AddDays(14);
    }

    public override string ToString()
    {
        return $"Title: \"{Title}\", Author: {Author}";
    }
}