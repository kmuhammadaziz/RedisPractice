using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Domain.Common.Entities;
using Exercise2.Persistance.Servises;
using FastEndpoints;
using Newtonsoft.Json;

namespace Exercise2.Api.Endpoints.Laptop2;

public class Create(ICacheService cacheService) : Endpoint<MyRequest, ShortResponse, MyRequestMapper>
{
    public override void Configure()
    {
        Post("/laptop/create");
        //Description(x => x.Accepts<MyRequest>());
        AllowAnonymous();
    }

    public override async Task<ShortResponse> ExecuteAsync(MyRequest req, CancellationToken ct)
    {

        var data = Map.ToEntity(req);

        var result = new ShortResponse { Id = data.Id };
        await cacheService.SetAsync(key: "laptop" + data.Id, value: data, null);

        await SendAsync(result);
        return result;
    }
}
