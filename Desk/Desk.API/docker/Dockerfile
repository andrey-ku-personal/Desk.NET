FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

#COPY . .
#RUN dotnet restore

COPY *.sln ./
COPY Desk.API/Desk.API.csproj Desk.API/
RUN dotnet restore Desk.API/Desk.API.csproj

COPY . .
WORKDIR "/src/Desk.API"
RUN dotnet publish "Desk.API.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Desk.API.dll"]