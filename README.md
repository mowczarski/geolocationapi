# GeolocationAPI
http://172.104.139.191:5123/swagger/index.html

## Introduction to Project
API that allows to convert IP or URL to geolocation and store it in database. Application is deveoped on .NET with clean architecture principles. The project uses libraries  such as EF, MediatR, FluentValidation, Serilog, Swagger, NUnit, Moq and FluentAssertions.

## Authorization

``` add 'x-api-key' header with api key value ```

## Healthchecks
``` enpoint /healthchecks ```

## Swagger

``` endpoint /swagger/index.html ```

## Docker

``` docker build -f Geolocation.API/Dockerfile -f geolocation.latest . ```

# Usage

There are 3 endpoints for managing geolocations, getting, adding and removing.

### GET 
address in parameter

 ``` api/geolocation?Address=www.google.pl ```  

### POST
JSON in request body

``` api/geolocation ```  

example
```
json
{
    "address": "www.google.pl"
}
```

### DELETE
JSON in request body
``` api/geolocation ```

example
```
json
{
    "address": "www.google.pl"
}
```

