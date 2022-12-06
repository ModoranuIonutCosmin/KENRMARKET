FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR "/src"
COPY . .
WORKDIR "/src/src/Order/Order.SignalR"
RUN dotnet restore "Order.SignalR.csproj"
RUN dotnet publish "Order.SignalR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.SignalR.dll"]
