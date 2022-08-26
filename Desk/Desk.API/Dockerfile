FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Desk.API/Desk.API.csproj", "Desk.API/"]
COPY ["Desk.Core/Desk.Core.csproj", "Desk.Core/"]
COPY ["Desk.Domain/Desk.Domain.csproj", "Desk.Domain/"]
COPY ["Desk.Migrations/Desk.Migrations.csproj", "Desk.Migrations/"]
RUN dotnet restore "Desk.API/Desk.API.csproj"
COPY . .
WORKDIR "/src/Desk.API"
RUN dotnet build "Desk.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desk.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desk.API.dll"]
