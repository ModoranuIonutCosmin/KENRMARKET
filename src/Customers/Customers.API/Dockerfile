﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR "/src"
COPY . .
WORKDIR "/src/src/Customers/Customers.API"
RUN dotnet restore "Customers.API.csproj"
RUN dotnet publish "Customers.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]
