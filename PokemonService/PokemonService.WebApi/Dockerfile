#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PokemonService.WebApi/PokemonService.WebApi.csproj", "PokemonService.WebApi/"]
COPY ["PokemonService.BusinessLayer/PokemonService.BusinessLayer.csproj", "PokemonService.BusinessLayer/"]
COPY ["PokemonService.DataAccess/PokemonService.DataAccess.csproj", "PokemonService.DataAccess/"]
RUN dotnet restore "PokemonService.WebApi/PokemonService.WebApi.csproj"
COPY . .
WORKDIR "/src/PokemonService.WebApi"
RUN dotnet build "PokemonService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonService.WebApi.dll"]
