#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Customers/Customers.API/Customers.API/Customers.API.csproj", "src/Customers/Customers.API/Customers.API/"]
COPY ["src/Customers/Customers.API/Customers.Infrastructure/Customers.Infrastructure.csproj", "src/Customers/Customers.API/Customers.Infrastructure/"]
COPY ["src/Customers/Customers.API/Customers.Application/Customers.Application.csproj", "src/Customers/Customers.API/Customers.Application/"]
COPY ["src/Customers/Customers.API/Customers.Domain/Customers.Domain.csproj", "src/Customers/Customers.API/Customers.Domain/"]
RUN dotnet restore "src/Customers/Customers.API/Customers.API/Customers.API.csproj"
COPY . .
WORKDIR "/src/src/Customers/Customers.API/Customers.API"
RUN dotnet build "Customers.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customers.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]