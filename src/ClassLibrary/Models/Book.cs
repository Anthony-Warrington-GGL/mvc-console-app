namespace mvc_console_app.Models;

public class Book
{
    public Guid Id {get; set;}
    public string Author {get; set;}
    public string Title {get; set;}    

    public Book()
    {
        
    }

    public Book(Guid id, string title, string author)
    {
        if (author is null || title is null)
        {
            throw new ArgumentNullException();
        }

        this.Id = id;
        this.Title = title;
        this.Author = author;
    }

    public override string ToString()
    {
        return $"Title: \"{Title}\", Author: {Author}";
    }
}