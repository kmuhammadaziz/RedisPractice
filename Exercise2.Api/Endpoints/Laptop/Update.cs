using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Domain.Common.Entities;
using Exercise2.Persistance.Servises;
using FastEndpoints;
using YamlDotNet.Core.Tokens;

namespace Exercise2.Api.Endpoints.Laptop2
{
    public class Update(ICacheService cacheService) : Endpoint<Laptop, MyResponse, MyRequestMapper>
    {
        public override void Configure()
        {
            Patch("/laptop/update");
            AllowAnonymous();
        }

        public override async Task<MyResponse> ExecuteAsync(Laptop req, CancellationToken ct)
        {
            var data = await cacheService.GetAsync(key: "laptop" + req.Id);

            if (data == null)
            {
                await SendErrorsAsync(404);
                return null!;
            }

            //await cacheService.DeleteAsync("laptop" + req.Id);
            await cacheService.SetAsync(key: "laptop" + req.Id,value: req);
            var result = new MyResponse() { Id = req.Id, Brand = req.Brand, Model = req.Model, DateOfIssue = req.DateOfIssue, Price = req.Price, UserName = req.UserName };

            await SendAsync(response: result);
            return Map.FromEntity(data);
        }
    }
}
