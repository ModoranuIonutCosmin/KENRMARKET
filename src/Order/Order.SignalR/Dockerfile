FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Order/Order.SignalR/Order.SignalR.csproj", "Order.SignalR/"]
RUN dotnet restore "src/Order/Order.SignalR/Order.SignalR.csproj"
COPY . .
WORKDIR "/src/Order.SignalR"
RUN dotnet build "Order.SignalR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.SignalR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.SignalR.dll"]
