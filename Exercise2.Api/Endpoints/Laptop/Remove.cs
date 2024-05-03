using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Persistance.Servises;
using FastEndpoints;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using YamlDotNet.Core.Tokens;

namespace Exercise2.Api.Endpoints.Laptop2
{
    public class Remove(ICacheService cacheService) : Endpoint<ShortRequest, MyResponse, MyRequestMapper>
    {
        public override void Configure()
        {
            Delete("/laptop/remove/{id}");
            AllowAnonymous();
        }

        public override async Task<MyResponse> ExecuteAsync(ShortRequest r, CancellationToken ct)
        {
            var data = await cacheService.GetAsync(key: "laptop" + r.Id.ToString());

            if (data is null)
            {
                await SendNotFoundAsync(default);
                return new();
            }
            var result = new MyResponse() { Id = data.Id, Brand = data.Brand, Model = data.Model, DateOfIssue = data.DateOfIssue, Price = data.Price, UserName = data.UserName };

            //var result = await Map.FromEntityAsync(data, ct);

            await cacheService.DeleteAsync(key: "laptop" + data.Id);
            await SendAsync(result, cancellation: ct);
            return result;
        }

    }
}
