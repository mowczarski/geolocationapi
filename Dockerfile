FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./GeolocationAPI.csproj" --disable-parallel
RUN dotnet publish "./GeolocationAPI.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5123 5124
ENTRYPOINT ["dotnet", "GeolocationAPI.dll"]