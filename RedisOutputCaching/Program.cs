using Microsoft.AspNetCore.OutputCaching;
using RedisOutputCaching;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    Ssl = false // false for development testing purposes
});
builder.Services.AddRedisOutputCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();
app.UseHttpsRedirection();

app.MapGet("/cache",
        async () =>
        {
            await Task.Delay(1000);
            return Task.FromResult("This is the endpoint that gets cached for 30 seconds. It also gets categorised under the 'cache' tag");
        })
    .CacheOutput(x => x
        .Expire(TimeSpan.FromSeconds(30))
        .Tag("cache"));

app.MapGet("/cache2",
        async () =>
        {
            await Task.Delay(1000);
            return Task.FromResult("This is the secondary endpoint that gets cached for 20 seconds. It also gets categorised under the 'cache' tag");
        })
    .CacheOutput(x => x
        .Expire(TimeSpan.FromSeconds(20))
        .Tag("cache"));

app.MapGet("/uncache",
    async (IOutputCacheStore outputCacheStore, CancellationToken token) =>
    {
        await outputCacheStore.EvictByTagAsync("cache", token);
        return Task.FromResult("This endpoint removes all the values for the cached endpoints that are categorised under the 'cache' tag"); 
    });

app.Run();