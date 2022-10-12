namespace Library.Book.Application.Commands.ResponseModels;

public record ValidateBookCommandResponse
{
    public bool isValid { get; set; }
}
