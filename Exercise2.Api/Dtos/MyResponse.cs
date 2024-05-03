namespace Exercise2.Api.Dtos;

public class MyResponse
{
    public Guid Id { get; set; } 
    public string UserName { get; set; } = default!;
    
    public string Model { get; set; } = default!;

    public string Brand { get; set; } = default!;

    public double Price { get; set; } = default!;

    public DateOnly DateOfIssue { get; set; } = default!;
}
