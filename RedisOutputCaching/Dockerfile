FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RedisOutputCaching/RedisOutputCaching.csproj", "RedisOutputCaching/"]
RUN dotnet restore "RedisOutputCaching/RedisOutputCaching.csproj"
COPY . .
WORKDIR "/src/RedisOutputCaching"
RUN dotnet build "RedisOutputCaching.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisOutputCaching.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisOutputCaching.dll"]
