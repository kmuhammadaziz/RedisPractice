using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Persistance.Servises;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exercise2.Api.Endpoints.Laptop2;

public class GetByUserName(ICacheService cacheService) : Endpoint<ShortRequest, MyResponse, MyRequestMapper>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Get("/laptop/getbyid/{id}");
        AllowAnonymous();
    }

    public override async Task<MyResponse> ExecuteAsync(ShortRequest r, CancellationToken ct)
    {   
        var data = await cacheService.GetAsync("laptop" + r.Id.ToString());

        if (data is null)
        {
            await SendNotFoundAsync(default);
            return null!;
        }

        //var result = await Map.FromEntityAsync(data);
        var result =  new MyResponse() { Id = data.Id, Brand = data.Brand, Model = data.Model, DateOfIssue = data.DateOfIssue, Price = data.Price, UserName = data.UserName };

        await SendAsync(result, cancellation: ct);

        return result;
    }

}
