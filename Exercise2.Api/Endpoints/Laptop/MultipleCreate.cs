namespace Exercise2.Api.Endpoints.Laptop2;

using Exercise2.Domain.Common.Entities;
using FastEndpoints;
using global::Exercise2.Api.Dtos;
using global::Exercise2.Api.Mappers;
using global::Exercise2.Persistance.Servises;
using Newtonsoft.Json;


public class MultipleCreate(ICacheService cacheService) : Endpoint<MultipleRequestByBrand, MultipleFullResponse, MyRequestMapper>
{
    public override void Configure()
    {
        Post("/laptop/multiplecreate");
        AllowAnonymous();
    }

    public override async Task<MultipleFullResponse> ExecuteAsync(MultipleRequestByBrand req, CancellationToken ct)
    {
        var list = new List<MyResponse>();
        foreach (MyRequest laptop in req.requests)
        {
            var data = Map.FromEntity(Map.ToEntity(laptop));

            await cacheService.SetAsync(key: "laptop" + data.Id, value: data, null);
            list.Add(data);
        }

        var result = new MultipleFullResponse() { result = list };
        await SendAsync(response: result, statusCode: 200, cancellation: ct);
        return result;
    }
}
