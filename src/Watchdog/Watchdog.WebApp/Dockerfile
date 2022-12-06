FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR "/src"
COPY . .
WORKDIR "/src/src/Watchdog/Watchdog.WebApp"
RUN dotnet restore "Watchdog.WebApp.csproj"
RUN dotnet publish "Watchdog.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Watchdog.WebApp.dll"]
