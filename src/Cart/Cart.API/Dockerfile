#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Cart/Cart.API/Cart.API.csproj", "src/Cart/Cart.API/"]
COPY ["src/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj", "src/Cart/Cart.Infrastructure/"]
COPY ["src/Cart/Cart.Application/Cart.Application.csproj", "src/Cart/Cart.Application/"]
COPY ["src/Cart/Cart.Domain/Cart.Domain.csproj", "src/Cart/Cart.Domain/"]
RUN dotnet restore "src/Cart/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/src/Cart/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]