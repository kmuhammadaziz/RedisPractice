using Exercise2.Domain.Common.Entities;

namespace Exercise2.Api.Dtos
{
    public class MultipleRequestByBrand
    {
        public List<MyRequest> requests { get; set; } = new();
    }
}
