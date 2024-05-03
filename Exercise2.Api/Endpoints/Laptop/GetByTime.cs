using Exercise2.Api.Dtos;
using Exercise2.Api.Mappers;
using Exercise2.Domain.Common.Entities;
using Exercise2.Persistance.Servises;
using FastEndpoints;

namespace Exercise2.Api.Endpoints.Laptop2
{
    public class GetByTime(ICacheService cacheService) : Endpoint<RequestTime, MultipleFullResponse, MyResponseMapper>
    {
        public override void Configure()
        {
            Get("/laptop/getbytime");
            AllowAnonymous();
        }

        public override async Task<MultipleFullResponse> ExecuteAsync(RequestTime req, CancellationToken ct)
        {
            if (!await AnalyzeAsync(req.from, req.to))
            {
                await SendErrorsAsync(400, ct);
                return null!;
            }

            var data = cacheService.GetAll();

            List<MyResponse> laptops = new();

            foreach (Laptop laptop in data)
            {
                var IsValidValue = await AnalyzeAsync(req.from, req.to, laptop.DateOfIssue);
                if (IsValidValue)
                    laptops.Add(Map.FromEntity(laptop));
            }

            return new() { result = laptops };
        }


        Task<bool> AnalyzeAsync(DateOnly from, DateOnly to)
        {
            return Task.Run(() =>
            {
                if (from > to)
                    return true;
                return false;
            });
        }

        Task<bool> AnalyzeAsync(DateOnly from, DateOnly to, DateOnly productTime)
        {
            return Task.Run(() =>
            {
                if (productTime <= to && productTime >= from)
                    return true;
                return false;
            });
        }
    }
}
