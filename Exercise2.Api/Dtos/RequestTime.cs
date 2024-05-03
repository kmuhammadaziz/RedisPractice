namespace Exercise2.Api.Dtos;

public class RequestTime
{
    public DateOnly from { get; set; } = default!;
    public DateOnly to { get; set; } = default!;
}
