using Exercise2.Domain.Common.Entities;

namespace Exercise2.Api.Dtos;

public class CreateResponse
{
    public List<Laptop> laptops { get; set; } = default!;
}
