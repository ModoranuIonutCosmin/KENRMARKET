#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Products/Products.API/Products.API.csproj", "src/Products/Products.API/"]
COPY ["src/Products/Product.Infrastructure/Products.Infrastructure.csproj", "src/Products/Product.Infrastructure/"]
COPY ["src/Products/Products.Application/Products.Application.csproj", "src/Products/Products.Application/"]
COPY ["src/Products/Products.Domain/Products.Domain.csproj", "src/Products/Products.Domain/"]
RUN dotnet restore "src/Products/Products.API/Products.API.csproj"
COPY . .
WORKDIR "/src/src/Products/Products.API"
RUN dotnet build "Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.API.dll"]