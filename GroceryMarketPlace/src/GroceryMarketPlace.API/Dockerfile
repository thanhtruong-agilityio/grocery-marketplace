FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GroceryMarketPlace.API/*.csproj", "GroceryMarketPlace.API/"]
COPY ["src/GroceryMarketPlace.DataAccess/*.csproj", "GroceryMarketPlace.DataAccess/"]
COPY ["src/GroceryMarketPlace.Domain/*.csproj", "GroceryMarketPlace.Domain/"]
COPY ["src/GroceryMarketPlace.Services/*.csproj", "GroceryMarketPlace.Services/"]
RUN dotnet restore "GroceryMarketPlace.API/GroceryMarketPlace.API.csproj"
COPY . .

WORKDIR "/src/."
RUN dotnet build "src/GroceryMarketPlace.API/GroceryMarketPlace.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "src/GroceryMarketPlace.API/GroceryMarketPlace.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:8080
ENTRYPOINT ["dotnet", "GroceryMarketPlace.API.dll"]
