## .NET 7 OutputCaching implementation with Redis

See https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-6/ for more info on OutputCaching.

This repository contains a custom implementation of the IOutputCacheStore interface using Redis. This will allow users to use the OutputCaching feature in a distributed setting.


The following packages were used to integrate with Redis:

- Microsoft.Extensions.Caching.StackExchangeRedis
- StackExchange.Redis

The following custom classes were made to make it this all work:

- OutputCacheExtensions
- RedisOutputCacheStore

### Try it out yourself

- Clone this repository
- Be sure to get the latest .NET 7 version. This repository was created using the .NET 7 version 6 preview.
- Get a Redis client such as RedisInsight to monitor what kind of data is being persisted to your Redis instance
- Run `docker-compose up`
    - This will spin up a Redis server instance
- Run this project using `dotnet run`
- Call the`cache` and `cache2` endpoints to get the following:
    - A sliding cache entry per endpoint
        - A set for the tag that was used for both these endpoints. This set will allow for evicting by tag.
- Call the `nocache` endpoint which will evict both cache entries based on the what is in the 'tag set'.

