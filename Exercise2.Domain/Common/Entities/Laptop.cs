namespace Exercise2.Domain.Common.Entities;

public class Laptop
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = default!;

    public string Model { get; set; } = default!;

    public string Brand { get; set; } = default!;

    public double Price { get; set; } = default!;

    public DateOnly DateOfIssue { get; set; } = default!;
}