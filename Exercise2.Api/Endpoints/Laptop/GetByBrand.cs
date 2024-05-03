using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Persistance.Servises;
using FastEndpoints;

namespace Exercise2.Api.Endpoints.Laptop2;

public class GetByBrand(ICacheService cacheService) : Endpoint<SearchBrandRequest, MultipleRequestByBrand, MyRequestMapper>
{
    public override void Configure()
    {
        Get("/laptop/getByBrand/{brand}");
        AllowAnonymous();
    }

    public override async Task<MultipleRequestByBrand> ExecuteAsync(SearchBrandRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Brand))
        {
            await SendErrorsAsync(404);
            return null!;
        }

        var found = cacheService.GetAll();


        if (found == null) { await SendErrorsAsync(400); return null!; }
        var exist = found.Where(laptop => laptop.Brand.ToLower() == req.Brand.ToLower()).ToList();
        var result = new List<MyRequest>();

        foreach (var item in exist)
        {
            result.Add(new() { Brand = item.Brand, DateOfIssue = item.DateOfIssue, Model = item.Model, Price = item.Price, UserName = item.UserName});
        }

        return new() { requests = result };
    }
}
