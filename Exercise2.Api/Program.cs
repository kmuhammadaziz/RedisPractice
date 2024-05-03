using Exercise2.Api.Mappers;
using Exercise2.Persistance.Servises;
using FastEndpoints;
using FastEndpoints.Swagger;

var bld = WebApplication.CreateBuilder();
bld.Services.AddSingleton<ICacheService, CacheService>();
bld.Services.AddAuthentication();
bld.Services.AddAuthorization();
bld.Services.AddFastEndpoints();
bld.Services.AddSingleton<MyRequestMapper>();
bld.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "My API";
        s.Version = "v1";
    };
});

bld.Services.AddStackExchangeRedisCache(
    options =>
    {
        options.Configuration = bld.Configuration.GetConnectionString("RedisConnectionString");
        options.InstanceName = "Caching.Example";
 });

bld.Services.AddDistributedMemoryCache();

var app = bld.Build();
app.UseFastEndpoints();
app.UseOpenApi(config =>
{
    config.Path = "/api/swagger/{documentName}/swagger.json";
});

app.UseSwaggerUi(s =>
{
    s.ConfigureDefaults(setting =>
    {
        setting.Path = "/api/swagger";
        setting.DocumentPath = "/api/swagger/{documentName}/swagger.json";
    });
});

app.UseAuthorization();
app.UseAuthentication();
app.Run();