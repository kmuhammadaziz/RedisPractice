using Exercise2.Api.Dtos;
using Exercise2.Persistance.Servises;
using FastEndpoints;

namespace Exercise2.Api.Endpoints.Laptop2;

public class GetAll(ICacheService cacheService) : EndpointWithoutRequest<MultipleFullResponse>
{
    public override void Configure()
    {
        Get("/laptop/getall");
        AllowAnonymous();
    }

    public override async Task<MultipleFullResponse> ExecuteAsync(CancellationToken ct)
    {
        var data = cacheService.GetAll();
        List<MyResponse> responses = new();

        if (data == null)
        {
            await SendNotFoundAsync(ct);
            return null!;
        }

        foreach(var item in data) { responses.Add(new() { Id = item.Id, Brand = item.Brand, Model = item.Model, Price = item.Price, UserName = item.UserName, DateOfIssue = item.DateOfIssue }); }
        var result = new MultipleFullResponse() { result = responses };

        await SendAsync(result, 200);
        return result;
    }
}
