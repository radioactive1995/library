namespace library.application.Books.Dtos;

public class BookDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public required DateTime PublishedDate { get; set; }
    public required int Status { get; set; }
    public string? BorrowedByUserId { get; set; }
}
