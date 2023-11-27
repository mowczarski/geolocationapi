FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 

COPY *.sln .
COPY Geolocation.Application/*.csproj ./Geolocation.Application/
COPY Geolocation.Domain/*.csproj ./Geolocation.Domain/
COPY Geolocation.API/*.csproj ./Geolocation.API/
COPY Geolocation.Infrastructure/*.csproj ./Geolocation.Infrastructure/ 

COPY Tests/Geolocation.Unit/*.csproj ./Tests/Geolocation.Unit/
COPY Tests/Geolocation.Integration/*.csproj ./Tests/Geolocation.Integration/
COPY Tests/Geolocation.E2E/*.csproj ./Tests/Geolocation.E2E/

RUN dotnet restore

COPY Geolocation.Application/. ./Geolocation.Application/
COPY Geolocation.Domain/. ./Geolocation.Domain/
COPY Geolocation.API/. ./Geolocation.API/
COPY Geolocation.Infrastructure/. ./Geolocation.Infrastructure/ 

COPY Tests/Geolocation.Unit/. ./Tests/Geolocation.Unit/
COPY Tests/Geolocation.Integration/. ./Tests/Geolocation.Integration/
COPY Tests/Geolocation.E2E/. ./Tests/Geolocation.E2E/

WORKDIR /app/Geolocation.API
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app 

COPY --from=build /app/Geolocation.API/out ./
EXPOSE 5123 5124
ENTRYPOINT ["dotnet", "Geolocation.API.dll"]