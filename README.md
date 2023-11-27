# GeolocationAPI
http://172.104.139.191:5123/swagger/index.html

## Introduction to Project
API that allows to convert IP or URL to geolocation and store it in database. Application is deveoped on .NET with clean architecture principles. The project uses libraries  such as EF, MediatR, FluentValidation, Serilog, Swagger, NUnit, Moq and FluentAssertions.

## Healthchecks
``` enpoint /healthchecks ```

## Authorization

``` add 'x-api-key' header to request with api key value ```

## swagger

``` endpoint /swagger/index.html ```

## docker

``` docker build -f Geolocation.API/Dockerfile -f geolocation.latest . ```
