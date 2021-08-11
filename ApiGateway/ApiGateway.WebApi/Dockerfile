#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiGateway.WebApi/ApiGateway.WebApi.csproj", "ApiGateway.WebApi/"]
COPY ["ApiGateway.BusinessLayer/ApiGateway.BusinessLayer.csproj", "ApiGateway.BusinessLayer/"]
RUN dotnet restore "ApiGateway.WebApi/ApiGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/ApiGateway.WebApi"
RUN dotnet build "ApiGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.WebApi.dll"]
