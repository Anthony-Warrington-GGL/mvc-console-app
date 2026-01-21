namespace mvc_console_app.Views;

public record class DescribedAction
{
    public required string Description {get; init;}

    public required Action Action {get; init;}
}