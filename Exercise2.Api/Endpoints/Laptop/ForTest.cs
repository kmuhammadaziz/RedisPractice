using Exercise2.Api.Dtos;
using FastEndpoints;

namespace Exercise2.Api.Endpoints.Laptop2;

public class ForTest : Endpoint<ForTest.Request, Response>
{
    public class Request
    {
        public string? Text { get; set; }
    }
    public override void Configure()
    {
        Get("/laptop/test/{text}");
        AllowAnonymous();
    }

    public override Task<Response> ExecuteAsync(Request req, CancellationToken ct)
    {
        return Task.FromResult(new Response { name = req.Text! });
    }

}

public class Response
{
    public string name { get; set; } = default!;
}