﻿namespace Geolocation.Domain.Abstrations
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);

        public static Error NullValue = new("Error.NullValue", "Null value was provided");
    }

    public static class GeolocationErrors
    {
        public static Error NotFound = new(
            "Geolocation.NotFound",
            "Geolocation with the specified identifier was not found");

        public static Error DuplicateError = new(
           "Geolocation.Duplicate",
           "Cannot insert duplicated geolocation");

        public static Error ApiStackError = new(
           "ApiStack.NoFound",
           "ApiStack did not give proper response");

        public static Error CannotRemove = new(
           "Geolocation.CannotRemove",
           "Cannot remove not existing geolocation");
    }
}
